using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XC.XSC.Models.Entity.Lob;
using XC.XSC.Models.Entity.Prefrence;
using XC.XSC.Repositories.Lobs;
using XC.XSC.Repositories.Preferences;
using XC.XSC.Service.User;

namespace XC.XSC.Service.Lobs
{
    public class LobService:ILobService
    {
        private readonly ILobRepository _lobRepository;
        private readonly IUserContext _userContext;

        public LobService(ILobRepository lobRepository, IUserContext userContext)
        {
            _lobRepository = lobRepository;
            _userContext = userContext;
        }

        /// <summary>
        /// This method is used to retrive Lob data based  lobId and tenantId.
        /// </summary>
        /// <param name="tenantId">Tenant Id</param>
        /// <param name="lobId">Lob Id</param>
        public async Task<Lob?> GetLobByTenantAndIdAsync(string lobId,string tenantId)
        {
            return await _lobRepository.GetAll().Where(x => x.LOBID == lobId && x.TenantId==tenantId && x.IsActive == true).FirstOrDefaultAsync();
        }

        /// <summary>
        /// This method is used to retrive all Lob data based tenantId.
        /// </summary>
        /// <param name="tenantId">Tenant Id</param>
        public async Task<List<Lob>> GetLobsAsync(string tenantId)
        {
            return await _lobRepository.GetAll().Where(x => x.TenantId == tenantId && x.IsActive == true)
                                              .ToListAsync();
        }

        /// <summary>
        /// This method is used to retrive all Lob data of currently logged in user.
        /// </summary>
        public async Task<List<Lob>> GetUserLob()
        {
            var lobList= await _lobRepository.GetAll().Where(x => x.TenantId == _userContext.UserInfo.TenantId && x.IsActive == true)
                                              .ToListAsync();

            var filteredLoblist= lobList.Where(c => _userContext.UserInfo.Lob.Any(c2 => c.Id == c2)).ToList();

            return filteredLoblist;
        }

        /// <summary>
        /// Get lob detail by Id.
        /// </summary>
        /// <param name="lobId">Lob Id: Int</param>
        /// <returns></returns>
        public async Task<Lob> GetLobById(int lobId)
        {
            return await _lobRepository.GetAll()
                .Where(x => x.Id == lobId && x.IsActive == true)
                .FirstOrDefaultAsync();
        }
    }
}
