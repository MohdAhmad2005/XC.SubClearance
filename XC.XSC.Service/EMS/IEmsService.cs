using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using XC.XSC.UAM.Models;
using XC.XSC.ViewModels.Configuration;

namespace XC.XSC.Service.EMS
{
    /// <summary>
    /// Ems interface.
    /// </summary>
  public interface IEmsService
    {
        /// <summary>
        /// Get mail box details based on the following parameter.
        /// </summary>
        /// <param name="regionId">region id.</param>
        /// <param name="lobId">lob id.</param>
        /// <param name="teamId">team id.</param>
        /// <returns>IResponse.</returns>
        Task<IResponse> GetMailBoxList(int regionId, int lobId, int teamId);
    }
}
