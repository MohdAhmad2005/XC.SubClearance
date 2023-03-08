using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XC.XSC.ViewModels.Configuration;

namespace XC.XSC.EMS.Connector
{
    public interface IEMSClient
    {
        /// <summary>
        /// Method to invoke the list of Mail from uam.
        /// </summary>
        /// <returns>List of Regions.</returns>
        Task<IResponse> GetMailBoxList(int regionId, string lobId, int teamId, string tenantId);
    }
}
