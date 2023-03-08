using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XC.XSC.ViewModels.Paging
{
    /// <summary>
    /// 
    /// </summary>
    public class Paging : IPaging
    {
        /// <summary>
        /// Page Size
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Page Limt
        /// </summary>
        public int Limit { get; set; }

        /// <summary>
        /// Sort Field
        /// </summary>
        public string? SortField { get; set; }

        /// <summary>
        /// Sort Order
        /// </summary>
        public int SortOrder { get; set; }   
        
       /// <summary>
       /// total item
       /// </summary>
        public int? TotalItems { get; set; }

        /// <summary>
        /// total pages
        /// </summary>
        public int? TotalPages { get; set; }

    }
}
