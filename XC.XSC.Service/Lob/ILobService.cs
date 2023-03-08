using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XC.XSC.Models.Entity.Lob;
using XC.XSC.Models.Entity.Prefrence;

namespace XC.XSC.Service.Lobs
{
    public interface ILobService
    {

        /// <summary>
        /// This method is used to retrive Lob data based  lobId and tenantId.
        /// </summary>
        /// <param name="tenantId">Tenant Id</param>
        /// <param name="lobId">Lob Id</param>
        Task<Lob?> GetLobByTenantAndIdAsync(string lobId,string tenantId);

        /// <summary>
        /// This method is used to retrive all Lob data based tenantId.
        /// </summary>
        /// <param name="tenantId">Tenant Id</param>
        Task<List<Lob>> GetLobsAsync(string tenantId);

        /// <summary>
        /// This method is used to retrive all Lob data of currently logged in user.
        /// </summary>
        Task<List<Lob>> GetUserLob();

        /// <summary>
        /// Get lob detail by Id.
        /// </summary>
        /// <param name="lobId">Lob Id: Int</param>
        /// <returns></returns>
        Task<Lob> GetLobById(int lobId);

    }
}
