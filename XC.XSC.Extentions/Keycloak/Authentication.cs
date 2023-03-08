using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using XC.CCMP.IAM.Keycloak;
using XC.CCMP.IAM.Keycloak.Connect;

namespace XC.XSC.Utilities.Keycloak
{
    public static class Authentication
    {
        public static void AddKeycloakAuthentication(this IServiceCollection services)
        {
            IApiConfig apiConfig = null;
            IIAMClient iamClient = null;

            var scopeFactory = services
                    .BuildServiceProvider()
                    .GetRequiredService<IServiceScopeFactory>();

            using (var scope = scopeFactory.CreateScope())
            {
                var provider = scope.ServiceProvider;                                
                {
                    apiConfig = provider.GetRequiredService<IApiConfig>();
                    iamClient = provider.GetRequiredService<IIAMClient>();
                }
            }

            #region 
            bool IsDevelopment = false; //dev

            var AuthenticationBuilder = services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            });

            AuthenticationBuilder.AddJwtBearer(o =>
            {

                #region == JWT Token Validation ===

                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = true,
                    ValidIssuers = new[] { apiConfig.ValidIssuers },
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = BuildRSAKey(apiConfig.PublicKey),
                    ValidateLifetime = true
                };

                #endregion

                #region === Event Authentification Handlers ===

                o.Events = new JwtBearerEvents()
                {
                    OnTokenValidated = context =>
                    {
                        string path = ((DefaultHttpContext)context.HttpContext).Request.Path.Value;
                        string payloadToken = ((JwtSecurityToken)context.SecurityToken).RawPayload;
                        
                        iamClient.ValidateResourcePermission(payloadToken, "/api/ccmp/uam/getCurrentUserDetails");

                        Console.WriteLine("User successfully authenticated");
                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = c =>
                    {
                        c.NoResult();

                        c.Response.StatusCode = 500;
                        c.Response.ContentType = "text/plain";

                        if (IsDevelopment)
                        {
                            return c.Response.WriteAsync(c.Exception.ToString());
                        }
                        return c.Response.WriteAsync("An error occured processing your authentication.");
                    }
                };

                #endregion

            });
            #endregion
        }
        private static RsaSecurityKey BuildRSAKey(string publicKeyJWT)
        {
            
            RSA rsa = RSA.Create();

            rsa.ImportSubjectPublicKeyInfo(

                source: Convert.FromBase64String(publicKeyJWT),
                bytesRead: out _
            );

            var IssuerSigningKey = new RsaSecurityKey(rsa);

            return IssuerSigningKey;
        }
    }
}
