using Microsoft.AspNetCore.Mvc;
using XC.CCMP.KeyVault;
using XC.XSC.EMS;
using XC.XSC.EMS.Connector;
using XC.XSC.EMS.Model;
using XC.XSC.Service.Lobs;
using XC.XSC.Service.User;
using XC.XSC.ViewModels;
using XC.XSC.ViewModels.Configuration;

namespace XC.XSC.Service.EMS
{
    public class EmsService: IEmsService
    {
        /// <summary>
        /// Service class which is used to invoke the uam web api.
        /// </summary>

        private readonly IEMSClient _emsClient;
        private readonly IKeyVaultConfig _keyVaultConfig;
        private readonly IEmsApiConfig _emsApiConfig;
        private readonly IUserContext _userContext;
        private readonly ILobService _lobService;
        private readonly IResponse _operationResponse;

        /// <summary>
        /// Constructor of service which initialize the dependencies. 
        /// </summary>
        /// <param name="keyVaultConfig">key vault config.</param>
        /// <param name="emsClient">ems client.</param>
        /// <param name="emsApiConfig">ems apiconfig.</param>
        /// <param name="userContext">user context.</param>
        /// <param name="lobService">lob service.</param>
        /// <param name="operationResponse">operation response.</param>
        public EmsService(IKeyVaultConfig keyVaultConfig, IEMSClient emsClient, IEmsApiConfig emsApiConfig, IUserContext userContext, ILobService lobService, IResponse operationResponse)
        {
            _keyVaultConfig = keyVaultConfig;
            _emsApiConfig = emsApiConfig;
            _emsApiConfig.BaseUrl = keyVaultConfig.EmsApiBaseUrl;
            _emsClient = emsClient;
            _userContext = userContext;
            _lobService = lobService;
            _operationResponse = operationResponse;
        }

        /// <summary>
        /// Get mail box details based on the following parameter.
        /// </summary>
        /// <param name="regionId">region id.</param>
        /// <param name="lobId">lob id.</param>
        /// <param name="teamId">team id.</param>
        /// <returns>IResponse.</returns>
        public async Task<IResponse> GetMailBoxList(int regionId, int lobId, int teamId)
        {
            var lobDetails = await _lobService.GetLobById(lobId);
            if(lobDetails != null && !string.IsNullOrEmpty(lobDetails.LOBID))
            {
                var emsDetails = await _emsClient.GetMailBoxList(regionId, lobDetails.LOBID, teamId, _userContext.UserInfo.TenantId);
                if (_operationResponse.Result != null)
                {
                    List<EmailBoxResponse> emsList = (List<EmailBoxResponse>)emsDetails.Result;
                    if(emsList != null && emsList.Count > 0)
                    {
                        _operationResponse.IsSuccess = true;
                        _operationResponse.Result = emsList;
                        _operationResponse.Message = "Success";
                    }
                }
            }
            else
            {
                _operationResponse.Result = null;
                _operationResponse.IsSuccess = false;
                _operationResponse.Message = "Lob details not found";
            }
            return _operationResponse;
        }
    }
}
