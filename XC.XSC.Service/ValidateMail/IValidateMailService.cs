using XC.XSC.ValidateMail.Models.Request;
using XC.XSC.ValidateMail.Models.Response;
using XC.XSC.ViewModels.Configuration;

namespace XC.XSC.Service.ValidateMail
{
    public interface IValidateMailService
    {
        /// <summary>
        /// Validate e-mail is in scope or out of scope on the basis of predefined rules.
        /// </summary>
        /// <param name="validateMailScopeRequest">Accept Camunda TaskId, SubmissionId, Stage, TenantId</param>
        /// <returns>return TaskId, SubmissionId, Stage, TenantId, Scope</returns>
        Task<IResponse> ValidateMailScopeAsync(ValidateMailScopeRequest validateMailScopeRequest);
    }
}
