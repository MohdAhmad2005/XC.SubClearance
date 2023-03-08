using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XC.XSC.ViewModels.Configuration;
using XC.XSC.Workflow.Workflow.Models;

namespace XC.XSC.Service.Workflow
{
    /// <summary>
    /// Implementation of IWorkflowService interface.
    /// </summary>
    public interface IWorkflowService
    {
        /// <summary>
        /// Method to run the camunda workflow.
        /// </summary>
        /// <param name="request">Model class property of type StartWorkflowRequest.</param>
        /// <returns>Camunda workflow process.</returns>
        Task<IResponse> RunWorkflow(StartWorkflowRequest request);
    }
}
