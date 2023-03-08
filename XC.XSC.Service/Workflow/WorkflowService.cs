using XC.XSC.ViewModels.Configuration;
using XC.XSC.Workflow;
using XC.XSC.Workflow.Workflow.Connect;
using XC.XSC.Workflow.Workflow.Models;

namespace XC.XSC.Service.Workflow
{
    /// <summary>
    /// Implementation of WorkflowService Service.
    /// </summary>
    public class WorkflowService : IWorkflowService
    {
        private readonly WorkflowType _workflowType;
        private readonly IWorkflowClient _workflowClient;
        private readonly IResponse _operationResponse;

        public WorkflowService(IWorkflowClient workflowClient, IResponse operationResponse)
        {
            _workflowType = WorkflowType.Camunda;
            _workflowClient = workflowClient;
            _operationResponse = operationResponse;
        }

        /// <summary>
        /// Method to run the camunda workflow.
        /// </summary>
        /// <param name="request">Model class property of type StartWorkflowRequest.</param>
        /// <returns>Camunda workflow process.</returns>
        public async Task<IResponse> RunWorkflow(StartWorkflowRequest request)
        {
            switch (_workflowType)
            {
                case WorkflowType.Camunda:
                    {
                        var response = await _workflowClient.RunWorkflow(request);

                        return response;
                    }
                default:
                    {
                        _operationResponse.IsSuccess = false;
                        _operationResponse.Message = "Workflow not found.";
                        _operationResponse.Result = null;
                        return _operationResponse;
                    }

            }
        }
    }
}