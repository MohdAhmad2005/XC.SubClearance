using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using XC.XSC.Data;
using XC.XSC.Models.Entity.Lob;

namespace XC.XSC.Repositories.Lobs
{
    /// <summary>
    /// This is the implementation class of ILobRepository interface.
    /// </summary>
    public class LobRepository:ILobRepository
    {
        private readonly MSSqlContext _msSqlContext;
        public LobRepository(MSSqlContext msSqlContext)
        {
            _msSqlContext = msSqlContext;
        }

        /// <summary>
        /// Not implemented.
        /// </summary>
        /// <param name="lob"> Lob modal object</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task AddAsync(Lob lob)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Not implemented.
        /// </summary>
        /// <param name="lob"> Lob modal object</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task DeleteAsync(Expression<Func<Lob, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This method is implemented to retrieve all record of Lob table.
        /// </summary>
        /// <returns> Returns all data from lob table</returns>
        public IQueryable<Lob> GetAll()
        {
            return _msSqlContext.Lob.AsQueryable();
        }

        /// <summary>
        /// This method is implemented to retrieve single record of Lob table.
        /// </summary>
        /// <param name="predicate">Predicate</param>
        /// <returns> Returns single data from lob table</returns>
        public async Task<Lob> GetSingleAsync(Expression<Func<Lob, bool>> predicate)
        {
            return await GetAll().SingleAsync<Lob>(predicate);
        }

        /// <summary>
        /// Not implemented.
        /// </summary>
        /// <param name="lob"> Lob modal object</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<Lob> UpdateAsync(Lob lob)
        {
            throw new NotImplementedException();
        }

    }
}
