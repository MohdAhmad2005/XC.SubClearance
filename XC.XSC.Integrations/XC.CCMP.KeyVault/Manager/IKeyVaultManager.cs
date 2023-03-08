using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XC.CCMP.KeyVault.Manager
{
    public interface IKeyVaultManager: IDisposable
    {
        /// <summary>
        /// Get the vault secret value by key.
        /// </summary>
        /// <param name="secretName"></param>
        /// <returns></returns>
        Task<string> GetSecret(string secretName);

        //public KeyVaultConfig keyVaultConfig { get; }
    }
}
