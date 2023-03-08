using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XC.CCMP.IAM;

namespace XC.XSC.ViewModels.Authentication
{
    public class AuthResponse: Response
    {
        public bool IsAuthSuccessful { get; set; }
        public object UserInfo { get; set; }
    }
}
