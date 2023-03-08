using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XC.XSC.Service.User;
using XC.XSC.UAM.UAM;
using XC.XSC.ViewModels.Configuration;
using XC.XSC.ViewModels;
using XC.XSC.Workflow.Workflow.Connect;
using XC.XSC.UAM.Models;
using XC.XSC.Workflow.Workflow.Models;

namespace XC.XSC.API.Controllers
{
    [Authorize]
    [Route("api/xsc/[controller]")]
    [ApiController]
    public class WorkFlowController : Controller
    {
        private readonly IWorkflowClient _workflowClient;
        private readonly IUserContext _userContext;
        private readonly IResponse _operationResponse;

        public WorkFlowController(IUserContext userContext, IResponse operationResponse, IWorkflowClient workflowClient)
        {
            _userContext = userContext;
            _operationResponse = operationResponse;
            _workflowClient= workflowClient;
        }

        [HttpPost]
        [Route("executeProcess")]
        [Authorize]
        public async Task<IResponse> ExecuteProcess(string teamId)
        {
            return null;
        }

    }
}
