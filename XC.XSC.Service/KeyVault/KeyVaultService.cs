using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XC.CCMP.KeyVault.Manager;

namespace XC.XSC.Service.KeyVault
{
    public class KeyVaultService: IKeyVaultService
    {
        private readonly IKeyVaultManager _secretManager;

        public KeyVaultService(IKeyVaultManager secretManager)
        {
            _secretManager = secretManager;
        }


    }
}
