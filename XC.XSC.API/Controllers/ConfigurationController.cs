using Microsoft.AspNetCore.Mvc;
using XC.CCMP.Logger;

namespace XC.XSC.API.Controllers
{
    [ApiController]
    [Route("api/DataStorage/[controller]")]
    public class ConfigurationController : ControllerBase
    {        
        private readonly ILoggerManager _logger;

        public ConfigurationController(ILoggerManager logger)
        {
            _logger = logger;

            _logger.LogInfo("Initialized - ConfigurationController");
        }

    }
}
