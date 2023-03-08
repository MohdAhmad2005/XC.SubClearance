using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XC.XSC.EMS
{
    public interface IEmsApiConfig
    {

        /// <summary>
        /// Base url to access the Ems application.
        /// </summary>
        public string BaseUrl { get; set; }

        /// <summary>
        /// Property to return the get Email Box list.
        /// </summary>
        public string EmailBoxListEndPoint { get; }
    }
}
