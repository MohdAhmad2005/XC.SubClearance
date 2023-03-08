using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XC.XSC.ViewModels.Configuration;
using XC.XSC.ViewModels.TenantActionDetail;

namespace XC.XSC.Service.JsonFileService
{
    public interface IJsonFileService
    {
        /// <summary>
        /// Get Submission Action Detail
        /// </summary>
        /// <param name="statusId">this property used for Submission StatusId</param>     
        /// <param name="roleName">this is parameter used for roleName </param>
        /// <returns>Submission Action of List</returns>
        Task<IResponse> GetSubmissionActions(int? statusId, string jsonPath);
    }
}
