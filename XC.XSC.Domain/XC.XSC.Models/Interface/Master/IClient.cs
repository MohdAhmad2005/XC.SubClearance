using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XC.CCMP.IAM.Keycloak.Connect;

namespace XC.XSC.Models.Interface.Master
{
    public interface IClient
    {
        public string ClientName { get; set; }
        public string ClientEmail { get; set; }
        public string Description { get; set; }

    }
}
