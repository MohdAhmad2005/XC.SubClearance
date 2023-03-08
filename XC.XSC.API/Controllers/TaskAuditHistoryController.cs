using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XC.CCMP.Logger;
using XC.XSC.Service.Submission;
using XC.XSC.Service.TaskAuditHistory;
using XC.XSC.ViewModels.Configuration;
using XC.XSC.ViewModels.TaskAuditHistory;

namespace XC.XSC.API.Controllers
{
    /// <summary>
    /// This is the Controller class containing the methods that interacts with UI directly.
    /// </summary>
    [Route("api/xsc/[controller]")]
    [ApiController]
    public class TaskAuditHistoryController : ControllerBase
    {
        private readonly ITaskAuditHistoryService _taskAuditHistoryService;
        private readonly ILoggerManager _logger;

        /// <summary>
        /// TaskAuditHistoryController constructer intitialigation for depandency injection  
        /// </summary>
        /// <param name="taskAuditHistoryService"></param>
        /// <param name="logger"></param>
        public TaskAuditHistoryController(ITaskAuditHistoryService taskAuditHistoryService, ILoggerManager logger)
        {
            this._taskAuditHistoryService = taskAuditHistoryService;
            _logger = logger;
            _logger.LogInfo("Initialized - TaskAuditHistoryController");
        }

        /// <summary>
        /// Endpoint to get the list of TaskAuditHistory.
        /// </summary>
        /// <param name="submissionId">this parameter used for submission id </param>
        /// <returns>List of TaskAuditHistory</returns>
        [HttpGet]
        [Route("getTaskAduditHistory/{submissionId}")]
        public async Task<IResponse> GetTaskAduditHistory(long submissionId)
        {
            _logger.LogInfo("Api call to get list of TaskAuditHistory");
            return await _taskAuditHistoryService.GetTaskAuditHistoryDetailAsync(submissionId);
        }

        /// <summary>
        /// Endpoint to get the list of Get Task Adudit History Duration.
        /// </summary>
        /// <param name="submissionId">this parameter is use for submission id </param>
        /// <returns> GetTask AduditHistor yDuration</returns>
        [HttpGet]
        [Route("getTaskAduditHistoryDuration/{submissionId}")]
        public async Task<IResponse> getTaskAduditHistoryDuration(long submissionId)
        {
            _logger.LogInfo("Api call to get Total Time taken in Every Process like Query,Under Review,Total Time");
            return await _taskAuditHistoryService.GetTaskAuditHistoryDurationAsync(submissionId);

        }
    }
}
