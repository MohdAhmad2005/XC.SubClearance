
namespace XC.XSC.Workflow.Workflow
{
    public interface IWorkflowApiConfig
    {
        public WorkflowType WorkflowType { get { return WorkflowType.Camunda; } }
        public string AdminUsername { get; }
        public string AdminPassword { get; }
        public string BaseUrl { get; }
        public string WorkflowAdminLoginEndpoint { get;}
        public string WorkflowEndpoint { get;}
        
    }
}
