using MongoDB.Driver.Linq;
using Newtonsoft.Json;
using System.Linq;
using System.Security.Cryptography;
using XC.XSC.Service.User;
using XC.XSC.ViewModels.Configuration;
using XC.XSC.ViewModels.TanentAction;
using XC.XSC.ViewModels.TenantActionDetail;

namespace XC.XSC.Service.JsonFileService
{
    public class JsonFileService : IJsonFileService
    {
        private readonly IUserContext _userContext;
        private readonly IResponse _operationResponse;

        public JsonFileService(IResponse operationResponse, IUserContext userContext)
        {
            _operationResponse = operationResponse;
            _userContext = userContext;
        }
        /// <summary>
        /// Get Submission Action Detail
        /// </summary>
        /// <param name="statusId">this property used for Submission StatusId</param>        
        /// <param name="roleName">this is parameter used for roleName </param>
        /// <param name="jsonData"> jsondata  read from JsonData folder  file name SubmissonStatusMappedWithAction </param>
        /// <returns>Submission Action of List</returns>
        public async Task<IResponse> GetSubmissionActions(int? statusId, string jsonPath)
        {

            TenantActionDetail? tenantActions = JsonConvert.DeserializeObject<List<TenantActionDetail>>(System.IO.File.ReadAllText(Path.Combine(jsonPath))).Where(t => t.id == _userContext.UserInfo.TenantId)
                                                 .FirstOrDefault();

            TenantActionResponse intersectedActions = new TenantActionResponse();

            if (tenantActions != null)
            {


                var actionsByStatus = statusId > 0 ? tenantActions.submissionStatusMappedAction.Where(s => s.statusId == statusId).ToList() : tenantActions.submissionStatusMappedAction.ToList();

                if (!string.IsNullOrWhiteSpace(_userContext.UserInfo.Role))
                {
                    var roleMappedWithAction = tenantActions.roleMappedWithAction
                                                      .Where(s => s.roleName.Trim().ToLower() == _userContext.UserInfo.Role.Trim().ToLower())
                                                      .SelectMany(f => f.allowedActions).OrderBy(s=>s).ToList();

                    int count = 0;
                    foreach (var actiondetails in actionsByStatus)
                    {
                        intersectedActions.submissionStatusMappedAction.Add(new SubmissionStatusMappedAction
                        {
                            statusId = actiondetails.statusId,
                            statusName = actiondetails.statusName,
                            allowedActions = new List<AllowedAction>()
                        });
                        foreach (var action in actiondetails.allowedActions.OrderBy(t=>t.lableName).ToList())
                        {

                            if (roleMappedWithAction.Contains(action.lableName))
                            {
                                intersectedActions.submissionStatusMappedAction[count].allowedActions.Add(action);
                            }
                        }
                        count++;
                    }
                    intersectedActions.notAssignedSubmissionActions = tenantActions.notAssignedSubmissionActions;
                    _operationResponse.IsSuccess = true;
                    _operationResponse.Result = intersectedActions;
                }
                else
                {
                    _operationResponse.Result = actionsByStatus;
                    _operationResponse.Message = "Success";
                    _operationResponse.IsSuccess = true;
                }
            }


            return _operationResponse;


        }
    }
}
