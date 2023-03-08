using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Data;
using XC.XSC.Repositories.Submission;
using XC.XSC.Repositories.SubmissionClearance;
using XC.XSC.Repositories.SubmissionExtraction;
using XC.XSC.Repositories.TenantContextMapping;
using XC.XSC.Service.Preferences;
using XC.XSC.Service.TenantContextMapping;
using XC.XSC.Service.User;
using XC.XSC.ValidateMail.Connect;
using XC.XSC.ValidateMail.Models.Request;
using XC.XSC.ValidateMail.Models.Response;
using XC.XSC.ViewModels.Configuration;
using XC.XSC.ViewModels.SubmissionExtraction;

namespace XC.XSC.Service.SubmissionClearance
{
    /// <summary>
    /// Used for Submission Clearance Service
    /// </summary>
    public class SubmissionClearanceService : ISubmissionClearanceService
    {
        private readonly IClient _client;
        private readonly ISubmissionRepository _submissionRepository;
        private readonly IPreferenceService _preferenceService;
        private readonly IUserContext _userContext;
        private readonly IResponse _operationResponse;
        private readonly ISubmissionClearanceRepository _submissionClearanceRepository;
        private readonly ITenantContextMappingService _tenantContextMappingService;
        private readonly ISubmissionExtractionRepository _submissionExtractionRepository;

        /// <summary>
        /// Initialize the submission clearance service
        /// </summary>
        /// <param name="client"></param>
        /// <param name="submissionRepository"></param>
        /// <param name="preferenceService"></param>
        /// <param name="userContext"></param>
        /// <param name="operationResponse"></param>
        /// <param name="submissionClearanceRepository"></param>
        /// <param name="tenantContextMappingService"></param>
        public SubmissionClearanceService(IClient client, ISubmissionRepository submissionRepository, IPreferenceService preferenceService
            , IUserContext userContext, IResponse operationResponse, ISubmissionClearanceRepository submissionClearanceRepository
            , ITenantContextMappingService tenantContextMappingService, ISubmissionExtractionRepository submissionExtractionRepository)
        {
            _client = client;
            _submissionRepository = submissionRepository;
            _preferenceService = preferenceService;
            _userContext = userContext;
            _operationResponse = operationResponse;
            _submissionClearanceRepository = submissionClearanceRepository;
            _tenantContextMappingService = tenantContextMappingService;
            _submissionExtractionRepository = submissionExtractionRepository;
        }

        /// <summary>
        /// Execute submission clearance check rule on the basis of predefined rules
        /// </summary>
        /// <param name="submissionId">Accept submissionId</param>
        /// <returns>SUCCESS</returns>
        public async Task<IResponse> SubmissionClearanceCheckAsync(long SubmissionId)
        {
            var submission = await _submissionRepository.GetSingleAsync(a => a.Id == SubmissionId);
            if (submission != null)
            {
                RuleExecutionRequest ruleExecutionRequest = await SetPreferenceValue(submission);
                RuleSetResponse ruleSetResponse = await _client.GetRuleSetList(GetRuleSetRequest(ruleExecutionRequest));
                ruleExecutionRequest.SourceData = await ConvertToDataTable(SubmissionId, ruleSetResponse);
                RuleExecutionResult ruleExecutionResult = await _client.ExecuteRuleByContextAsync(ruleExecutionRequest);

                if (ruleExecutionResult != null)
                {
                    if (ruleExecutionResult.ResultantData.Rows.Count > 0)
                    {
                        await SaveSubmissionClearance(SubmissionId, ruleExecutionRequest, ruleExecutionResult, ruleSetResponse);
                        _operationResponse.IsSuccess = true;
                        _operationResponse.Message = "SUCCESS";
                    }
                    else
                    {
                        _operationResponse.IsSuccess = false;
                        _operationResponse.Message = "TRUE";
                    }
                }
            }
            return _operationResponse;
        }

        /// <summary>
        /// Get rule set request object
        /// </summary>
        /// <param name="ruleExecutionRequest"></param>
        /// <returns></returns>
        private RuleSetRequest GetRuleSetRequest(RuleExecutionRequest ruleExecutionRequest)
        {
            RuleSetRequest ruleSetRequest = new RuleSetRequest();
            ruleSetRequest.UserName = "";
            ruleSetRequest.Take = 1000;
            ruleSetRequest.OrderBy = "";
            FilterConfig filterConfig = new FilterConfig();
            filterConfig.ColumnName = "ContextId";
            filterConfig.Operator = "=";
            filterConfig.Value = Convert.ToString(ruleExecutionRequest.RuleContextId);
            filterConfig.LogicalOperator = "";
            List<FilterConfig> filterConfigs = new List<FilterConfig>();
            filterConfigs.Add(filterConfig);
            ruleSetRequest.Filter = filterConfigs;
            return ruleSetRequest;
        }

        /// <summary>
        /// Save data in SubmissionClearances table if exist then delete based on submissionId and contextId
        /// </summary>
        /// <param name="SubmissionId"></param>
        /// <param name="ruleExecutionRequest"></param>
        /// <param name="executedRule"></param>
        /// <returns></returns>
        private async Task SaveSubmissionClearance(long SubmissionId, RuleExecutionRequest ruleExecutionRequest, RuleExecutionResult ruleExecutionResult, RuleSetResponse ruleSetResponse)
        {
            bool ruleStatus = false;
            List<Models.Entity.SubmissionClearance.SubmissionClearance> submissionClearances = _submissionClearanceRepository.GetAll()
                .Where(a => a.SubmissionId == SubmissionId && a.ContextId == ruleExecutionRequest.RuleContextId
                 && a.EntityId == ruleExecutionRequest.SourceEntityId).ToList();
            if (submissionClearances.Count > 0)
            {
                await _submissionClearanceRepository.RemoveRange(submissionClearances);
            }

            List<Models.Entity.SubmissionClearance.SubmissionClearance> submissionClearancesAdd = new List<Models.Entity.SubmissionClearance.SubmissionClearance>();
            foreach (var ruleSet in ruleSetResponse.Entity)
            {
                ruleStatus = Convert.ToBoolean(ruleExecutionResult.ResultantData.Rows[0][$"{ruleSet.RuleSetName}_RESULT"]);
                Models.Entity.SubmissionClearance.SubmissionClearance submissionClearance = new Models.Entity.SubmissionClearance.SubmissionClearance()
                {
                    SubmissionId = SubmissionId,
                    RuleName = ruleSet.RuleSetName,
                    RuleStatus = ruleStatus,
                    Description = ruleSet.Message,
                    Remark = ruleSet.Message,
                    ContextId = ruleExecutionRequest.RuleContextId,
                    EntityId = ruleExecutionRequest.SourceEntityId,
                    TenantId = _userContext.UserInfo.TenantId,
                    CreatedBy = "System",
                    IsActive = true
                };
                submissionClearancesAdd.Add(submissionClearance);
            }
            await _submissionClearanceRepository.AddRangeAsync(submissionClearancesAdd);
        }

        /// <summary>
        /// Set ContextId and TenantId from TenantContextMappings table
        /// </summary>
        /// <param name="submission"></param>
        /// <returns>ruleExecutionRequest</returns>
        private async Task<RuleExecutionRequest> SetPreferenceValue(Models.Entity.Submission.Submission submission)
        {
            RuleExecutionRequest ruleExecutionRequest = new RuleExecutionRequest();
            ruleExecutionRequest.SourceDataRowIdentifier = "";
            Models.Entity.TenantContextMapping.TenantContextMapping tenantContextMapping = await _tenantContextMappingService.GetTenantContextMapping(submission);
            if (tenantContextMapping != null)
            {
                ruleExecutionRequest.RuleContextId = tenantContextMapping.ContextId;
                ruleExecutionRequest.SourceEntityId = tenantContextMapping.EntityId;
            }
            return ruleExecutionRequest;
        }

        /// <summary>
        /// Create datatable and add result field
        /// </summary>
        /// <param name="submissionId"></param>
        /// <param name="ruleSetResponse"></param>
        /// <returns>table</returns>
        public async Task<DataTable> ConvertToDataTable(long submissionId, RuleSetResponse ruleSetResponse)
        {
            DataTable table = new DataTable();
            var submissionResponse = await _submissionExtractionRepository.GetSingleAsync(x => x.SubmissionId == submissionId.ToString());
            if (submissionResponse != null)
            {
                DataRow dr = table.NewRow();
                foreach (SubmissionData submissionData in submissionResponse.SubmissionFormData.SubmissionData)
                {
                    var fields = submissionResponse.SubmissionFormData.SubmissionData.Where(x => x.Fields == submissionData.Fields).FirstOrDefault();
                    if (fields != null)
                    {
                        table.Columns.Add(new DataColumn(fields.Fields, typeof(string)));
                        dr[fields.Fields] = fields.FinalEntry;
                    }
                }
                table.Rows.Add(dr); 
            }
            foreach (var ruleSet in ruleSetResponse.Entity)
            {
                DataColumn dataColumn = new DataColumn($"{ruleSet.RuleSetName}_RESULT", typeof(bool));
                dataColumn.DefaultValue = false;
                table.Columns.Add(dataColumn);
            }
            return table;
        }


        /// <summary>
        /// Get all submission clearances by submissionId
        /// </summary>
        /// <param name="submissionId">Accept submissionId</param>
        /// <returns>SUCCESS</returns>
        public async Task<IResponse> GetSubmissionClearancesAsync(long SubmissionId)
        {
            _operationResponse.IsSuccess = true;
            _operationResponse.Message = "SUCCESS";

            _operationResponse.Result = await _submissionClearanceRepository.GetAll()
                      .Where(k => k.SubmissionId == SubmissionId
                       && k.TenantId == _userContext.UserInfo.TenantId
                       && k.IsActive == true)
                .ToListAsync<Models.Entity.SubmissionClearance.SubmissionClearance>();

            return _operationResponse;
        }
    }
}
