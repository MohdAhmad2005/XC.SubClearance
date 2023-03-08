using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XC.XSC.ViewModels.TanentAction
{
    /// <summary>
    ///  Get Action Detail Reponse Model
    /// </summary>
    public class TenantActionDetail
    {
        /// <summary>
        /// initialization Constructor 
        /// </summary>
        public TenantActionDetail()
        {
            submissionStatusMappedAction = new List<SubmissionStatusMappedAction>();
            roleMappedWithAction = new List<RoleMappedWithAction>();
        }
        /// <summary>
        /// Tenant id
        /// </summary>
        public string id { get; set; } = string.Empty;

        /// <summary>
        /// Tenant Name
        /// </summary>
        public string tenantName { get; set; } = string.Empty;

        /// <summary>
        /// Submission status with mapped Action List Detail
        /// </summary>
        public List<SubmissionStatusMappedAction> submissionStatusMappedAction { get; set; }
        /// <summary>
        /// Action mapped wth Role
        /// </summary>
        public List<RoleMappedWithAction> roleMappedWithAction { get; set; }

        /// <summary>
        /// not Assigned action 
        /// </summary>
        public NotAssignedSubmissionActions notAssignedSubmissionActions { get; set; }

    }

    public class SubmissionStatusMappedAction
    {
        public SubmissionStatusMappedAction()
        {
            this.allowedActions = new List<AllowedAction>();
        }
        /// <summary>
        /// Submission Status Id
        /// </summary>
        public int statusId { get; set; }

        /// <summary>
        /// submission Status Name
        /// </summary>
        public string statusName { get; set; } = string.Empty;

        /// <summary>
        /// Submission Action List
        /// </summary>
        public List<AllowedAction> allowedActions { get; set; }

        /// <summary>
        /// Rale with mapped Action list
        /// </summary>


    }
    public class RoleMappedWithAction
    {
        RoleMappedWithAction()
        {
            this.allowedActions = new List<string>();
        }
        /// <summary>
        /// user role id
        /// </summary>
        public string roleId { get; set; } = string.Empty;
        /// <summary>
        /// user Role Name 
        /// </summary>
        public string roleName { get; set; } = string.Empty;

        /// <summary>
        /// List of Action 
        /// </summary>
        public List<string> allowedActions { get; set; }
    }

    public class AllowedAction
    {
        /// <summary>
        /// lableName 
        /// </summary>
        public string lableName { get; set; } = string.Empty;

        /// <summary>
        /// url
        /// </summary>
        public string url { get; set; } = string.Empty;

        /// <summary>
        /// className
        /// </summary>
        public string className { get; set; } = string.Empty;

        /// <summary>
        /// formName
        /// </summary>
        public string formName { get; set; } = string.Empty;

        /// <summary>
        /// actionType
        /// </summary>
        public string actionType { get; set; } = string.Empty;

    }

    public class NotAssignedSubmissionActions
    {
        /// <summary>
        /// Constractor
        /// </summary>
        public NotAssignedSubmissionActions()
        {
            this.allowedActions= new List<AllowedAction>();
        }
        /// <summary>
        /// list of action
        /// </summary>
        public List<AllowedAction> allowedActions { get; set; }
    }


}

