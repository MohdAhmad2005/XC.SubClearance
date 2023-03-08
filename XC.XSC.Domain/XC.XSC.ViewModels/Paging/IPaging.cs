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
    public interface IPaging
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
       
    }
}
