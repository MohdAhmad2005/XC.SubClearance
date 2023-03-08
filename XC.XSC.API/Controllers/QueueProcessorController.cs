using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using XC.XSC.Service.Submission;
using XC.XSC.ViewModels.QueueProcessor;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace XC.XSC.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/xsc/[controller]")]
    [ApiController]
    [Authorize]
    public class QueueProcessorController : ControllerBase
    {
        private readonly IEmailProcessorService _emailProcessorService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="emailProcessorService"></param>
        public QueueProcessorController(IEmailProcessorService emailProcessorService)
        {
            _emailProcessorService = emailProcessorService;
        }

        /// <summary>
        /// Process email monitoring system request
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ProcessMessage")]        
        [Authorize]
        public async Task<bool> ProcessMessage(XC.XSC.ViewModels.QueueProcessor.QueueMessage request)
        {
            var emailProcessorRequest = JsonConvert.DeserializeObject<EmailProcessorRequest>(request.Message);
            return await _emailProcessorService.SaveEmailInfo(emailProcessorRequest);
        }
    }
}
