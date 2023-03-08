using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace XC.XSC.ViewModels.Sla
{
    public class SlaConfigurationList
    {

        /// <summary>
        /// this constructor is use for initalize the paging
        /// </summary>
        public SlaConfigurationList()
        {
            this.Paging= new Paging.Paging();
            this.SlaConfigurationResponse = new List<SlaConfigurationResponse>();
        }

        /// <summary>
        /// Tthis is use for Pagin and get the paging 
        /// </summary>
        public Paging.Paging Paging { get; set; }

        /// <summary>
        /// This View Model is crate the sla configuration response 
        /// </summary>
        public List<SlaConfigurationResponse> SlaConfigurationResponse { get; set; }
    }

}
