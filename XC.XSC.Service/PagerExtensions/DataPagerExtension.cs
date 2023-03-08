using MailKit.Search;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using XC.XSC.ViewModels.PagedModel;

namespace XC.XSC.Service.PagerExtensions
{
    public static class DataPagerExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="query">IQueryable Context model</param>
        /// <param name="page"> </param>
        /// <param name="limit"></param>
        /// <param name="SortDirection"></param>
        /// <param name="SortField"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<PagedModel<TModel>> PaginateAsync<TModel>(
            this IQueryable<TModel> query,
            int page,
            int limit,
            int SortDirection,
            string? SortField,
        CancellationToken cancellationToken)
            where TModel : class
        {

            var paged = new PagedModel<TModel>();

            page = (page < 0) ? 1 : page;

            paged.CurrentPage = page;
            paged.PageSize = limit;

            paged.TotalItems = await query.CountAsync(cancellationToken);

            var startRow = (page - 1) * limit;
            var direction = SortDirection == 0 ? " ASC" : " DESC";
            if (SortField != null)
            {
                query = query.OrderBy(SortField + direction);

            }
            else if (query.ToList().Count > 0)
            {
                query = query.OrderBy("CreatedDate " + "DESC");
            }

            paged.Items = await query.Skip(startRow).Take(limit).ToListAsync(cancellationToken);
            if (paged.Items.Count == 0 && page > 1)
            {
                paged.CurrentPage = page - 1;
                startRow = (paged.CurrentPage - 1) * limit;
                paged.Items = await query.Skip(startRow).Take(limit).ToListAsync(cancellationToken);

            }
            paged.TotalPages = (int)Math.Ceiling(paged.TotalItems / (double)limit);

            return paged;
        }

    }
}
