using System.Data;
using System.Text;
using XC.CCMP.KeyVault;
using XC.CCMP.Queue;
using XC.CCMP.Queue.ASB;
using XC.CCMP.Queue.Enum;
using XC.XSC.Models.Entity.Prefrence;
using XC.XSC.Repositories.Comment;
using XC.XSC.Repositories.Submission;
using XC.XSC.Service.Preferences;
using XC.XSC.Service.User;
using XC.XSC.ValidateMail.Connect;
using XC.XSC.ValidateMail.Models;
using XC.XSC.ValidateMail.Models.Request;
using XC.XSC.ValidateMail.Models.Response;
using XC.XSC.ViewModels.Configuration;
using XC.XSC.ViewModels.Enum;

namespace XC.XSC.Service.ValidateMail
{
    public class ValidateMailService : IValidateMailService
    {
        private readonly IClient _client;
        private readonly ISubmissionRepository _submissionRepository;
        private readonly IPreferenceService _preferenceService;
        private readonly IAzureServiceBus _azureServiceBus;
        private readonly IQueueConfiguration _queueConfiguration;
        private readonly IKeyVaultConfig _keyVaultConfig;
        private readonly ICommentRepository _commentRepository;
        private readonly IUserContext _userContext;
        private readonly IResponse _operationResponse;

        public ValidateMailService(IClient client, ISubmissionRepository submissionRepository, IPreferenceService preferenceService, IAzureServiceBus azureServiceBus, IQueueConfiguration queueConfiguration
            , IKeyVaultConfig keyVaultConfig, ICommentRepository commentRepository, IUserContext userContext, IResponse operationResponse)
        {
            _client = client;
            _submissionRepository = submissionRepository;
            _preferenceService = preferenceService;
            _azureServiceBus = azureServiceBus;
            _queueConfiguration = queueConfiguration;
            _keyVaultConfig = keyVaultConfig;
            _commentRepository = commentRepository;
            _userContext = userContext;
            _operationResponse = operationResponse;
        }

        /// <summary>
        /// Validate e-mail is in scope or out of scope on the basis of predefined rules.
        /// </summary>
        /// <param name="validateMailScopeRequest">Accept Camunda TaskId, SubmissionId, Stage, TenantId</param>
        /// <returns>return TaskId, SubmissionId, Stage, TenantId, Scope</returns>
        public async Task<IResponse> ValidateMailScopeAsync(ValidateMailScopeRequest validateMailScopeRequest)
        {
            ValidateMailScopeResponse validateMailScopeResponse = new ValidateMailScopeResponse();

            var submission = await _submissionRepository.GetSingleAsync(a => a.Id == validateMailScopeRequest.SubmissionId);

            if (submission != null)
            {
                EmailQueueMessageResquest emailQueueMessageResquest = SetEmailQueueMessageResquest(validateMailScopeRequest, submission);

                RuleExecutionRequest ruleExecutionRequest = await SetPreferenceValue(validateMailScopeRequest);
                ruleExecutionRequest.SourceData = CreateDataTable<EmailQueueMessageResquest>(emailQueueMessageResquest);

                RuleExecutionResult ruleExecutionResult = await _client.ExecuteRuleByContextAsync(ruleExecutionRequest);

                if (ruleExecutionResult != null)
                {
                    validateMailScopeResponse.TaskId = submission.TaskId;
                    validateMailScopeResponse.SubmissionId = validateMailScopeRequest.SubmissionId;
                    validateMailScopeResponse.Stage = validateMailScopeRequest.Stage;
                    validateMailScopeResponse.TenantId = validateMailScopeRequest.TenantId;

                    if (ruleExecutionResult.ResultantData.Rows.Count > 0)
                    {
                        bool scope = false;
                        bool.TryParse(Convert.ToString(ruleExecutionResult.ResultantData.Rows[0]["Scope"]), out scope);

                        submission.IsInScope = scope;
                        validateMailScopeResponse.Scope = scope;

                        await _submissionRepository.UpdateAsync(submission);

                        if (!scope)
                        {
                            #region Only out scope case Add Comment

                            StringBuilder reasonForOutScope = new StringBuilder();

                            foreach (var executedRule in ruleExecutionResult.RulesExecutedDetail)
                            {
                                reasonForOutScope.Append($"{executedRule.ErrorMessage}, ");
                            }
                            if (reasonForOutScope.Length > 0)
                            {
                                Models.Entity.Comment.Comment comments = new Models.Entity.Comment.Comment()
                                {
                                    CommentText = reasonForOutScope.Remove(reasonForOutScope.Length - 2, 2).ToString(),
                                    CommentTypeId = (int)Enum.Parse(typeof(CommentType), CommentType.OutOfScope.ToString()),
                                    TenantId = _userContext.UserInfo.TenantId,
                                    CreatedBy = _userContext.UserInfo.UserId,
                                    CreatedDate = DateTime.Now,
                                    SubmissionId = validateMailScopeRequest.SubmissionId,
                                    IsActive = true
                                };
                                await _commentRepository.AddAsync(comments);
                            }

                            #endregion
                            bool outOfScopeQueue = false;
                            List<Preference> preferences = await _preferenceService.GetPreferenceByTenantAsync(validateMailScopeRequest.TenantId);

                            if (preferences.Count() > 0)
                            {
                                Preference? preference = preferences.Find(a => a.Key == PreferenceConstants.OutOfScopeQueue);
                                if (preference != null)
                                {
                                    bool.TryParse(preference.Value, out outOfScopeQueue);
                                }
                            }
                            if (outOfScopeQueue)
                            {
                                //If OutScope then push message to ASB
                                await SendOutScopeMessage(submission, validateMailScopeRequest, scope);
                            }
                        }

                        _operationResponse.IsSuccess = true;
                        _operationResponse.Result = validateMailScopeResponse;
                        _operationResponse.Message = "SUCCESS";
                    }
                }
            }
            else
            {
                _operationResponse.IsSuccess = false;
                _operationResponse.Message = "Submission data not found.";
            }
            return _operationResponse;
        }

        /// <summary>
        /// SendOutOfScopeMessage to Queue
        /// </summary>
        /// <param name="submission"></param>
        /// <param name="validateMailScopeRequest"></param>
        /// <param name="scope"></param>
        /// <returns></returns>
        private async Task SendOutScopeMessage(Models.Entity.Submission.Submission submission, ValidateMailScopeRequest validateMailScopeRequest, bool scope)
        {
            OutScopeMessage outScopeMessage = new OutScopeMessage();

            outScopeMessage.ConfigurationId = submission.EmailInfo.ConfigurationId;
            outScopeMessage.MailboxEmailId = submission.EmailInfo.MailboxName;
            outScopeMessage.MessageId = submission.EmailInfo.MessageId;
            outScopeMessage.SubmissionId = validateMailScopeRequest.SubmissionId;
            outScopeMessage.Stage = validateMailScopeRequest.Stage;
            outScopeMessage.TenantId = validateMailScopeRequest.TenantId;
            outScopeMessage.Scope = scope;

            _queueConfiguration.QueueType = (QueueType)System.Enum.Parse(typeof(QueueType), _keyVaultConfig.QueueType);
            _queueConfiguration.ASBConfiguration = new AzureServiceConfiguration
            {
                ConnectionString = _keyVaultConfig.QueueConnectionString,
                QueueName = _keyVaultConfig.QueueName,
            };

            await _azureServiceBus.SendMessageAsync(outScopeMessage);
        }

        /// <summary>
        /// SetEmailQueueMessageResquest is used to set object to send Rule Engine
        /// </summary>
        /// <param name="validateMailScopeRequest"></param>
        /// <param name="emailQueueMessageResquest"></param>
        /// <param name="submission"></param>
        private EmailQueueMessageResquest SetEmailQueueMessageResquest(ValidateMailScopeRequest validateMailScopeRequest, Models.Entity.Submission.Submission submission)
        {
            EmailQueueMessageResquest emailQueueMessageResquest = new EmailQueueMessageResquest();
            emailQueueMessageResquest.TaskId = validateMailScopeRequest.TaskId;
            emailQueueMessageResquest.SubmissionId = validateMailScopeRequest.SubmissionId;
            emailQueueMessageResquest.FromEmail = submission.EmailInfo.FromEmail.Trim();
            emailQueueMessageResquest.ToEmail = submission.EmailInfo.ToEmail;
            if (!string.IsNullOrEmpty(submission.EmailInfo.Subject))
            {
                emailQueueMessageResquest.EmailSubject = submission.EmailInfo.Subject.Trim();
                if (emailQueueMessageResquest.EmailSubject.Length > 0)
                {
                    emailQueueMessageResquest.EmailSubjectPrefix = emailQueueMessageResquest.EmailSubject.Substring(0, 2);
                }
            }
            emailQueueMessageResquest.BodyLength = submission.EmailInfo.BodyLength;
            emailQueueMessageResquest.AttachmentCount = submission.EmailInfo.TotalDocuments;
            return emailQueueMessageResquest;
        }

        /// <summary>
        /// SetPreferenceValue is used to set PreferenceValue from Database 
        /// </summary>
        /// <param name="ruleExecutionRequest"></param>
        /// <param name="validateMailScopeRequest"></param>
        /// <returns></returns>
        private async Task<RuleExecutionRequest> SetPreferenceValue(ValidateMailScopeRequest validateMailScopeRequest)
        {
            RuleExecutionRequest ruleExecutionRequest = new RuleExecutionRequest();
            ruleExecutionRequest.SourceDataRowIdentifier = "";
            int ruleContextId = 0;
            int sourceEntityId = 0;
            List<Preference> preferences = await _preferenceService.GetPreferenceByTenantAsync(validateMailScopeRequest.TenantId);
            if (preferences.Count() > 0)
            {
                Preference? preference = preferences.Find(a => a.Key == PreferenceConstants.RuleContextIdScope);
                if (preference != null)
                {
                    int.TryParse(preference.Value, out ruleContextId);
                    preference = null;
                }
                preference = preferences.Find(a => a.Key == PreferenceConstants.SourceContextIdScope);
                if (preference != null)
                {
                    int.TryParse(preference.Value, out sourceEntityId);
                }
            }
            ruleExecutionRequest.RuleContextId = ruleContextId;
            ruleExecutionRequest.SourceEntityId = sourceEntityId;
            return ruleExecutionRequest;
        }

        /// <summary>
        /// CreateDataTable is used to create datatable from model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        private DataTable CreateDataTable<T>(T entity)
        {
            DataTable dt = new DataTable();
            //creating columns
            foreach (var prop in typeof(T).GetProperties())
            {
                dt.Columns.Add(prop.Name, prop.PropertyType);
            }
            //creating rows
            var values = GetObjectValues(entity);
            dt.Rows.Add(values);
            return dt;
        }

        /// <summary>
        /// GetObjectValues is used to get values from model
        /// </summary>
        /// <typeparam name="T">model object</typeparam>
        /// <param name="entity"></param>
        /// <returns>Array of values</returns>
        private object[] GetObjectValues<T>(T entity)
        {
            var values = new List<object>();
            foreach (var prop in typeof(T).GetProperties())
            {
                values.Add(prop.GetValue(entity));
            }
            return values.ToArray();
        }
    }
}
