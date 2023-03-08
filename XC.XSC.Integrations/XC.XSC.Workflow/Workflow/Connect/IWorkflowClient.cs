using XC.XSC.ViewModels.Configuration;
using XC.XSC.Workflow.Workflow.Models;

namespace XC.XSC.Workflow.Workflow.Connect
{
    /// <summary>
    /// Interface of IWorkflowClient.
    /// </summary>
    public interface IWorkflowClient
    {
        /// <summary>
        /// Method to run the camunda workflow.
        /// </summary>
        /// <param name="request">Model class property of type StartWorkflowRequest.</param>
        /// <returns>Camunda workflow process.</returns>
        Task<IResponse> RunWorkflow(StartWorkflowRequest request);
    }
}
