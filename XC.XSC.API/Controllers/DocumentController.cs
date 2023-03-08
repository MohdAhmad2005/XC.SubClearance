using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XC.CCMP.Logger;
using XC.XSC.Service.DataStorage;
using XC.XSC.Service.User;

namespace XC.XSC.API.Controllers
{
    /// <summary>
    /// This controller will handle all the document related methods.
    /// </summary>
    [Authorize]
    [Route("api/xsc/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IUserContext _userContext;
        private readonly ILoggerManager _logger;
        private readonly IDataStorageService _dataStorageService;

        /// <summary>
        /// this is the document controller constructor.
        /// </summary>
        /// <param name="userContext">user context instance.</param>
        /// <param name="logger">logger instance.</param>
        /// <param name="dataStorageService">data storage service interface instance.</param>
        public DocumentController(IUserContext userContext, ILoggerManager logger, IDataStorageService dataStorageService)
        {
            _userContext = userContext;
            _logger = logger;
            _dataStorageService = dataStorageService;
            _logger.LogInfo("Initialized - Document Controller");
        }

        /// <summary>
        /// this method will download the file based on document id.
        /// </summary>
        /// <param name="documentId">document id of the file.</param>
        /// <returns>file.</returns>
        [HttpGet]
        [Route("download/{documentId}")]
        [Authorize]
        public async Task<IActionResult> Download(string documentId)
        {
            if (!string.IsNullOrEmpty(documentId))
            {
                _logger.LogInfo("API called - Download Method");
                var data = await _dataStorageService.DownloadDocumentAsync(_userContext.UserInfo.TenantId, documentId);
                if (data.StreamData != null && data.StreamData.Length > 0)
                {
                    return File(data.StreamData, System.Net.Mime.MediaTypeNames.Application.Octet, data.FileName);
                }
                else
                    return BadRequest("Sorry, file not found");
            }
            else
                return BadRequest("document is a required parameter.");
        }
    }
}
