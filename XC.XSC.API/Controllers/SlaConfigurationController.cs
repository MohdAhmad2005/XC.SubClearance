using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XC.XSC.Service.EMS;
using XC.XSC.Service.Sla;
using XC.XSC.Service.User;
using XC.XSC.ViewModels.Configuration;
using XC.XSC.ViewModels.Enum;
using XC.XSC.ViewModels.Paging;
using XC.XSC.ViewModels.Sla;

namespace XC.XSC.API.Controllers
{
    [Authorize]
    [Route("api/xsc/[controller]")]
    [ApiController]
    public class SlaConfigurationController : ControllerBase
    {
        private readonly IUserContext _userContext;
        private readonly ISlaConfigurationService _slaConfigService;
        private readonly IResponse _operationResponse;
        private readonly IEmsService _emsService;

        /// <summary>
        /// SlaController constructer intitialigation for depandency injection  
        /// </summary>
        /// <param name="Sla"></param>
        /// <param name="logger"></param>
        /// <param name="Usercontext"></param>
        public SlaConfigurationController(ISlaConfigurationService slaConfigService, IResponse response, IUserContext userContext, IEmsService emsService)
        {
            _slaConfigService = slaConfigService;
            _operationResponse = response;
            _userContext = userContext;
            _emsService= emsService;    
        }

        /// <summary>
        /// Endpoint to get the list of Get Sla with all details .
        /// </summary>
        /// <param name="RegionId">this parameter is use for RegionId </param>
        ///  <param name="TeamdId">this parameter is use for TeamdId </param>
        ///   <param name="LobId">this parameter is use for LobId </param>
        ///    <param name="slaType">this parameter is use for slaType </param>
        ///     <param name="mailBoxId">this parameter is use for mailBoxId </param>
        /// <returns> Get All Sla Details</returns>
        [HttpGet]
        [Route("getAllSlaConfigDetails/{RegionId}/{TeamdId}/{LobId}/{slaType}/{mailBoxId}")]
        public async Task<IResponse> GetAllSlaConfigDetails(int RegionId, int TeamdId, int LobId, SlaType slaType, Guid mailBoxId)
        {
            return await _slaConfigService.GetSlaConfiguration(RegionId, TeamdId, LobId, slaType, mailBoxId);
        }

        /// <summary>
        ///save Sla Configuration detail based on Scheduler fire the job
        /// </summary>
        /// <param>
        /// TeamId,
        /// RegionId,
        /// LobId,
        /// MailBoxId,
        /// Type,Day,Hours ,Min
        /// Percentage 
        /// Sample Percentage
        /// Task Type as 0,1,2
        /// </param>
        /// <returns>Success</returns>
        [HttpPost]
        [Route("saveSlaConfiguration")]
        public async Task<IResponse> SaveSlaConfiguration( SlaConfigurationRequest slaRequest)
        {
            return await _slaConfigService.SaveSlaConfiguration(slaRequest);
        }
        /// <summary>
        /// update sla based on request model 
        /// </summary>
        /// <param name="slaRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("updateSlaConfiguration")]
        public async Task<IResponse> UpdatSlaConfiguration([FromBody] SlaConfigurationRequest slaRequest)
        {
            return await _slaConfigService.UpdateSlaConfiguration(slaRequest);
        }

        /// <summary>
        /// get SlaConfiguration by Id
        /// </summary>
        /// <param name="SlaId"> sla id </param>
        /// <returns></returns>
        [HttpGet]
        [Route("getSlaConfigurationbyId/{SlaId}")]
        public async Task<IResponse> getSlaConfigurationbyId(long slaId)
        {
            return await _slaConfigService.getSlaConfigurationbyId(slaId);
        }

        /// <summary>
        /// Endpoint to get the list of Get Sla with all details .
        /// </summary>
        [HttpGet]
        [Route("getAllSlaDetail")]
        public async Task<IResponse> getAllSlaDetail()
        {
            return await _slaConfigService.getAllSlaDetail();
        }


    }
}

