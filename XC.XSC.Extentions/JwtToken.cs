using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace XC.XSC.Utilities
{
    public class JwtToken : IDisposable
    {
        private JwtSecurityTokenHandler _jwtSecurityTokenHandler;

        public JwtToken() {
            _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        }

        public JwtSecurityToken GetDecodedToken(string jwtToken)
        {            
            return _jwtSecurityTokenHandler.ReadJwtToken(jwtToken);
        }

        void IDisposable.Dispose()
        {
            if(_jwtSecurityTokenHandler != null)
            {
                _jwtSecurityTokenHandler = null;
            }
        }
    }
}
