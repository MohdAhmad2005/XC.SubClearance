using MongoDB.Driver;
using System.Data;
using XC.CCMP.KeyVault;
using XC.CCMP.Queue;
using XC.CCMP.Queue.ASB;
using XC.CCMP.Queue.Enum;
using XC.XSC.Models.Entity.Prefrence;
using XC.XSC.Repositories.Submission;
using XC.XSC.Repositories.SubmissionExtraction;
using XC.XSC.Service.Preferences;
using XC.XSC.Service.User;
using XC.XSC.ValidateMail.Models;
using XC.XSC.ViewModels.Configuration;
using XC.XSC.ViewModels.Submission;
using XC.XSC.ViewModels.SubmissionExtraction;

namespace XC.XSC.Service.SubmissionExtraction
{
    public class SubmissionExtractionService : ISubmissionExtractionService
    {
        private readonly IUserContext _userContext;
        private readonly ISubmissionExtractionRepository _submissionExtractionRepository;
        private readonly IResponse _operationResponse;
        private readonly IPreferenceService _preferenceService;
        private readonly ISubmissionRepository _submissionRepository;
        private readonly IQueueConfiguration _queueConfiguration;
        private readonly IAzureServiceBus _azureServiceBus;
        private readonly IKeyVaultConfig _keyVaultConfig;

        public SubmissionExtractionService(ISubmissionExtractionRepository submissionExtractionRepository, IResponse operationResponse, IUserContext userContext, IPreferenceService preferenceService, ISubmissionRepository submissionRepository
            , IQueueConfiguration queueConfiguration, IAzureServiceBus azureServiceBus, IKeyVaultConfig keyVaultConfig)
        {
            _submissionExtractionRepository = submissionExtractionRepository;
            _operationResponse = operationResponse;
            _userContext = userContext;
            _preferenceService = preferenceService;
            _submissionRepository = submissionRepository;
            _queueConfiguration = queueConfiguration;
            _azureServiceBus = azureServiceBus;
            _keyVaultConfig = keyVaultConfig;
        }

        /// <summary>
        /// Add Submission Details.
        /// </summary>
        /// <returns>Returns the submission details as IResponse.</returns>
        public async Task<IResponse> AddSubmissionDetail()
        {
            Models.Mongo.Entity.SubmissionExtraction config = new Models.Mongo.Entity.SubmissionExtraction();
            SubmissionData data = new SubmissionData();

            SuggestionOption option = new SuggestionOption()
            {
                Id = "1",
                Name = "Accord 145",
                FinalEntry = "13-12-2022",
                Confidance = "100"
            };
            List<SuggestionOption> lst = new List<SuggestionOption>();
            lst.Add(option);
            Suggestions suggestion = new Suggestions()
            {
                Id = "1",
                SuggestionOptions = lst
            };
            SubmissionData submissionData = new SubmissionData()
            {
                Fields = "Effective Date",
                GroupName = "Policy Detail",
                FinalEntry = "15-12-2022",
                Confidance = "100",
                Suggestions = suggestion
            };
            config.SubmissionFormData = new SubmissionFormData();
            config.SubmissionFormData.SubmissionData = new List<SubmissionData>();
            config.SubmissionId = "12";
            config.SubmissionFormData.SubmissionData.Add(submissionData);

            var r = await _submissionExtractionRepository.Add(config);
            if (r != null)
            {
                _operationResponse.Result = r;
                _operationResponse.IsSuccess = true;
                _operationResponse.Message = "SUCCESS";
            }
            else
            {
                _operationResponse.Result = null;
                _operationResponse.IsSuccess = false;
                _operationResponse.Message = "No result found";
            }

            return _operationResponse;
        }


        public async Task<IResponse> GetSubmissionForm()
        {
            var builder = Builders<Models.Mongo.Entity.SubmissionForm>.Filter;
            FilterDefinition<Models.Mongo.Entity.SubmissionForm> filter = builder.Where(s => s.TenantId =="xceedance");
            var submissionForm = await _submissionExtractionRepository.GetSubmissionForm(filter);
            if(submissionForm != null)
            {
                _operationResponse.Result = submissionForm;
                _operationResponse.IsSuccess = true;
                _operationResponse.Message = "SUCCESS";
            }
            else
            {
                _operationResponse.Result = null;
                _operationResponse.IsSuccess = false;
                _operationResponse.Message = "No result found";
            }
            return _operationResponse;
        }

        /// <summary>
        /// Get Submission Edit screen details.
        /// </summary>
        /// <param name="submissionId">Submission id of case.</param>
        /// <returns>Returns the submission details on submission edit screen.</returns>
        public async Task<IResponse> GetSubmissionTransformedData(long submissionId)
        {
            var submissionResponse = await _submissionExtractionRepository.GetSingleAsync(x => x.SubmissionId == submissionId.ToString());

            if (submissionResponse != null)
            {
                _operationResponse.Result = submissionResponse;
                _operationResponse.IsSuccess = true;
                _operationResponse.Message = "SUCCESS";
            }
            else
            {
                _operationResponse.Result = null;
                _operationResponse.IsSuccess = false;
                _operationResponse.Message = "No result found";
            }

            return _operationResponse;
        }


        /// <summary>
        /// update Submission Edit screen details on mongo.
        /// </summary>
        /// <param name="submissionExtraction">Object of SubmissionExtraction.</param>
        /// <returns>Returns the submission details on submission edit screen.</returns>
        public async Task<IResponse> UpdateSubmissionTransformedData(Models.Mongo.Entity.SubmissionExtraction submissionExtraction)
        {
            bool isValid = true;
            var requestBuilder = Builders<Models.Mongo.Entity.SubmissionForm>.Filter;
            FilterDefinition<Models.Mongo.Entity.SubmissionForm> filter = requestBuilder.Where(s => s.TenantId == _userContext.UserInfo.TenantId);
            var submissionFormResponse = await _submissionExtractionRepository.GetSubmissionForm(filter);
            if (submissionFormResponse != null)
            {
                var submissionFormResponseList = submissionFormResponse?.SubmissionEditForm?.SubmissionForm?.ToList();
                var submissionEditData = submissionExtraction?.SubmissionFormData?.SubmissionData?.Where(x => submissionFormResponseList.Any(y => x.Fields.Contains(y.Fields)))?.ToList();
                if(submissionEditData != null && submissionFormResponseList != null)
                {
                    foreach (var submissionForm in submissionFormResponseList)
                    {
                        foreach (var controls in submissionForm.Controls)
                        {
                            if (controls.Required)
                            {
                                var key = char.ToUpper(controls.Key[0]) + controls.Key.Substring(1);
                                var filteredSubmissionData = submissionEditData.Where(x => x.Fields == submissionForm.Fields).FirstOrDefault();
                                string keyData = filteredSubmissionData.GetType().GetProperty(key).GetValue(filteredSubmissionData).ToString();
                                if (string.IsNullOrEmpty(keyData))
                                {
                                    isValid = false;
                                    break;
                                }
                            }
                        }
                        if (!isValid)
                        {
                            break;
                        }
                    }
                }
            }

            if (isValid)
            {
                var submissionResponse = await _submissionExtractionRepository.GetSingleAsync(x => x.Id == submissionExtraction.Id);
                if (submissionResponse != null)
                {
                    Models.Mongo.Entity.SubmissionExtraction submissionExtractionRequest = new Models.Mongo.Entity.SubmissionExtraction()
                    {
                        Id = submissionExtraction.Id,
                        SubmissionId = submissionExtraction.SubmissionId,
                        SubmissionFormData = submissionExtraction.SubmissionFormData,
                        CreatedBy = submissionExtraction.CreatedBy,
                        CreatedOn = submissionExtraction.CreatedOn,
                        ModifiedBy = _userContext.UserInfo.UserId,
                        ModifiedOn = DateTime.Now,
                        IsActive = submissionExtraction.IsActive

                    };
                    var response = await _submissionExtractionRepository.UpdateAsync(submissionExtractionRequest);
                    if (response != null)
                    {
                        var submission = await _submissionRepository.GetSingleAsync(t => t.Id == Convert.ToDecimal(submissionExtractionRequest.SubmissionId) && t.TenantId == _userContext.UserInfo.TenantId
                             && t.IsActive);

                        if (submission != null)
                        {
                            submission.ModifiedBy = _userContext.UserInfo.UserId;
                            submission.ModifiedDate = DateTime.Now;
                            submission.isDataCompleted = true;
                            var submissionUpdated = await _submissionRepository.UpdateAsync(submission);
                            if (submissionUpdated != null)
                            {
                                _operationResponse.Result = submissionResponse;
                                _operationResponse.IsSuccess = true;
                                _operationResponse.Message = "SUCCESS";
                                return _operationResponse;
                            }
                        }
                    }
                    _operationResponse.Result = null;
                    _operationResponse.IsSuccess = false;
                    _operationResponse.Message = "Submission data not updated.";
                    return _operationResponse;
                }
                else
                {
                    _operationResponse.Result = null;
                    _operationResponse.IsSuccess = false;
                    _operationResponse.Message = "No result found for submission extraction.";
                }
            }
            else
            {
                _operationResponse.Result = null;
                _operationResponse.IsSuccess = false;
                _operationResponse.Message = "Please enter all mandatory fields.";
            }

            return _operationResponse;
        }

        #region Validate MailDuplicity Check

        /// <summary>
        /// Check mail is duplicate or not based on submissionId
        /// </summary>
        /// <param name="submissionId">Submission id of case.</param>
        /// <returns>Returns true if mail is duplicate otherwise false.</returns>
        public async Task<IResponse> ValidateMailDuplicityAsync(long submissionId)
        {
            _operationResponse.IsSuccess = false;
            _operationResponse.Message = "No result found";
            var submissionResponse = await _submissionExtractionRepository.GetSingleAsync(x => x.SubmissionId == submissionId.ToString());
            if (submissionResponse != null)
            {
                var builder = Builders<Models.Mongo.Entity.SubmissionExtraction>.Filter;
                FilterDefinition<Models.Mongo.Entity.SubmissionExtraction> filter = builder.Where(s => s.SubmissionId != submissionResponse.SubmissionId);
                List<Preference> preferences = await _preferenceService.GetPreferenceByTenantAsync(_userContext.UserInfo.TenantId);
                if (preferences.Count() > 0)
                {
                    Preference? preference = preferences.Find(a => a.Key == PreferenceConstants.MailDuplicityField);
                    if (preference != null)
                    {
                        foreach (string field in preference.Value.Split(','))
                        {
                            var finalEntry = submissionResponse.SubmissionFormData.SubmissionData.Where(x => x.Fields == field).FirstOrDefault();
                            if (finalEntry != null)
                            {
                                var findFluent = Builders<Models.Mongo.Entity.SubmissionExtraction>.Filter.ElemMatch(
                                            a => a.SubmissionFormData.SubmissionData,
                                            b => b.Fields == field && b.FinalEntry == finalEntry.FinalEntry);
                                filter &= findFluent;
                            }
                        }
                        var duplicateMail = await _submissionExtractionRepository.GetSingleAsync(filter);
                        if (duplicateMail != null)
                        {
                            var submission = await _submissionRepository.GetSingleAsync(a => a.Id == Convert.ToInt64(submissionResponse.SubmissionId));
                            submission.IsInScope = false;
                            await _submissionRepository.UpdateAsync(submission);
                            bool outOfScopeQueue = false;
                            Preference? preferenceOutScopeQueue = preferences.Find(a => a.Key == PreferenceConstants.OutOfScopeQueue);
                            if (preferenceOutScopeQueue != null)
                            {
                                bool.TryParse(preferenceOutScopeQueue.Value, out outOfScopeQueue);
                            }
                            if (outOfScopeQueue)
                            {
                                await SendOutScopeMessage(submission);
                            }
                            _operationResponse.IsSuccess = true;
                            _operationResponse.Message = "SUCCESS";
                        }
                    }
                }
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
        private async Task SendOutScopeMessage(Models.Entity.Submission.Submission submission)
        {
            OutScopeMessage outScopeMessage = new OutScopeMessage();
            outScopeMessage.ConfigurationId = submission.EmailInfo.ConfigurationId;
            outScopeMessage.MailboxEmailId = submission.EmailInfo.MailboxName;
            outScopeMessage.MessageId = submission.EmailInfo.MessageId;
            outScopeMessage.SubmissionId = submission.Id;
            outScopeMessage.TenantId = submission.TenantId;
            outScopeMessage.Scope = submission.IsInScope;

            _queueConfiguration.QueueType = (QueueType)System.Enum.Parse(typeof(QueueType), _keyVaultConfig.QueueType);
            _queueConfiguration.ASBConfiguration = new AzureServiceConfiguration
            {
                ConnectionString = _keyVaultConfig.QueueConnectionString,
                QueueName = _keyVaultConfig.QueueName,
            };
            await _azureServiceBus.SendMessageAsync(outScopeMessage);
        }

        #endregion
    }
}
