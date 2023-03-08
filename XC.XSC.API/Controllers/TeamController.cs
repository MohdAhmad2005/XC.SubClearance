using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XC.CCMP.Logger;
using XC.XSC.Service.User;
using XC.XSC.UAM.UAM;
using XC.XSC.ViewModels.Configuration;

namespace XC.XSC.API.Controllers
{
    /// <summary>
    /// This controller will handle all the team related methods.
    /// </summary>
    [Authorize]
    [Route("api/xsc/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly IUserContext _userContext;
        private readonly ILoggerManager _logger;
        private readonly IUamService _uamService;
        private readonly IResponse _response;

        /// <summary>
        /// This is the team controller constructor.
        /// </summary>
        /// <param name="userContext">user context instance.</param>
        /// <param name="logger">logger instance.</param>
        /// <param name="uamService">uam servie instance.</param>
        /// <param name="response">IResponse instance.</param>
        public TeamController(IUserContext userContext, ILoggerManager logger, IUamService uamService, IResponse response)
        {
            _userContext = userContext;
            _logger = logger;
            _uamService = uamService;
            _response = response;
            _logger.LogInfo("Initialized - Team Controller");
        }

        /// <summary>
        /// Get team list.
        /// </summary>
        /// <returns>IResponse.</returns>
        [HttpGet("getTeamList")]
        [Authorize]
        public async Task<IResponse> GetTeamList()
        {
            _logger.LogInfo("API called - Get team list method");
            return await _uamService.GetTeamList();
        }
    }
}
