using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XC.CCMP.Logger;
using XC.XSC.Service.IAM;
using XC.XSC.ViewModels.Configuration;

namespace XC.XSC.API.Controllers
{
    /// <summary>
    /// This is the Controller class containing the methods that interacts with UAM.
    /// </summary>
    [Authorize]
    [Route("api/xsc/[controller]")]
    [ApiController]
    public class EnumController : ControllerBase
    {
        private readonly IResponse _operationResponse;
        private readonly IEnumService _enumService;
        private readonly ILoggerManager _logger;

        /// <summary>
        /// user construtor.
        /// </summary>     
        /// <param name="operationResponse">Response of IResponse type.</param>
        /// <param name="logger">logger instance.</param>
        public EnumController(IResponse operationResponse, IEnumService enumService, ILoggerManager logger)
        {
            _operationResponse = operationResponse;
            _enumService = enumService;
            _logger = logger;
            _logger.LogInfo("Initialized - Enum Controller");
        }

        /// <summary>
        /// Get the list of enum key and value pair.
        /// </summary>
        /// <param name="enumName">Enum Name.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("getEnum/{enumName}")]
        [Authorize]
        public async Task<IResponse> GetEnum(string enumName)
        {
            _logger.LogInfo("API called - Get enum method");

            var enumData = _enumService.EnumNamedValues(enumName).Result;

            if (enumData != null)
            {
                _operationResponse.IsSuccess = true;
                _operationResponse.Message = $"Enum: {enumName} result";
                _operationResponse.Result = enumData;
            }
            else
            {

                _operationResponse.IsSuccess = false;
                _operationResponse.Message = "No records found.";
                _operationResponse.Result = null;
            }
            return _operationResponse;
        }

    }

}
