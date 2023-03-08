using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using XC.CCMP.IAM.Keycloak.Models;
using XC.CCMP.IAM.Keycloak.Models.Permission;

namespace XC.CCMP.IAM.Keycloak.Connect
{
    public class Client: IIAMClient
    {
        private IApiConfig _apiConfig;
        private IIAMResponse _response;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="apiConfig"></param>
        public Client(IApiConfig apiConfig, IIAMResponse response)
        {
            _apiConfig = apiConfig;
            _response = response;
        }

        /// <summary>
        /// Try to login with provided username and password. If username or password are invalide, this method will return new empty KeycloakToken object.
        /// </summary>
        /// <param name="username">Username of user that needs to be authenticated.</param>
        /// <param name="password">Password of user that needs to be authenticated.</param>
        /// <returns></returns>
        public async Task<TokenResponse> Login(string userName, string password)
        {
            StringBuilder data = new StringBuilder();

            data.Append($"client_id={_apiConfig.ClientId}&");
            data.Append($"client_secret={_apiConfig.ClientSecret}&");
            data.Append($"username={userName}&");
            data.Append($"password={password}&");
            data.Append($"grant_type=password&");

            using (HttpClient httpClient = new HttpClient())
            {
                using (HttpContent httpContent = new StringContent(data.ToString()))
                {
                    httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

                    var response = await httpClient.PostAsync(_apiConfig.LoginEndpoint, httpContent);
                    string content = await response.Content.ReadAsStringAsync();

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        return new TokenResponse
                        {
                            Token = JsonConvert.DeserializeObject<KeycloakToken>(content),
                            StatusCode = response.StatusCode
                        };
                    }
                    else
                    {
                        var resp = JsonConvert.DeserializeObject<Models.Response>(content);
                        return new TokenResponse
                        {
                            StatusCode = response.StatusCode,
                            Error = resp.Error,
                            ErrorDescription = resp.ErrorDescription
                        };
                    }
                }
            }
        }

        public async Task<TokenResponse> GetAdminLoginToken(string username, string password)
        {
            StringBuilder data = new StringBuilder();

            data.Append($"client_id=admin-cli&");
            data.Append($"client_secret={_apiConfig.ClientSecret}&");
            data.Append($"username={username}&");
            data.Append($"password={password}&");
            data.Append($"grant_type=password&");

            using (HttpClient httpClient = new HttpClient())
            {
                using (HttpContent httpContent = new StringContent(data.ToString()))
                {
                    httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

                    var response = await httpClient.PostAsync(_apiConfig.AdminLoginEndpoint, httpContent);
                    string content = await response.Content.ReadAsStringAsync();

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        return new TokenResponse
                        {
                            Token = JsonConvert.DeserializeObject<KeycloakToken>(content),
                            StatusCode = response.StatusCode
                        };
                    }
                    else
                    {
                        var resp = JsonConvert.DeserializeObject<Models.Response>(content);
                        return new TokenResponse
                        {
                            StatusCode = response.StatusCode,
                            Error = resp.Error,
                            ErrorDescription = resp.ErrorDescription
                        };
                    }
                }
            }
        }

        /// <summary>
        /// Register new user with filled Registration object.
        /// </summary>
        /// <param name="userRegistration">Instance of Registration object with filled data.</param>
        /// <returns></returns>
        public async Task<KeycloakToken> Registration(Registration userRegistration)
        {
            TokenResponse tokenResponse = await GetAdminLoginToken(_apiConfig.AdminUsername, _apiConfig.AdminPassword);
            if (tokenResponse.StatusCode != HttpStatusCode.OK)
            {
                return null;
            }

            //KeycloakToken token = tokenResponse.Token;

            StringBuilder data = new StringBuilder();

            data.Append("{");
            data.Append($"\"email\": \"{userRegistration.Email}\",");
            data.Append($"\"username\": \"{userRegistration.Username}\",");
            data.Append($"\"firstName\": \"{userRegistration.FirstName}\",");
            data.Append($"\"lastName\": \"{userRegistration.LastName}\",");
            data.Append($"\"enabled\": {userRegistration.Enabled},");
            data.Append($"\"emailVerified\": {userRegistration.EmailVerified}");
            data.Append("}");

            using (HttpClient httpClient = new HttpClient())
            {
                using (HttpContent httpContent = new StringContent(data.ToString()))
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenResponse.Token.AccessToken);
                    httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var response = await httpClient.PostAsync(_apiConfig.UserEndpoint, httpContent);

                    string[] locationSegments = response.Headers.Location.AbsoluteUri.Split('/');

                    string userGuid = locationSegments[locationSegments.Length - 1];

                    StringBuilder passwordData = new StringBuilder();

                    passwordData.Append("{");
                    passwordData.Append($"\"temporary\": {userRegistration.Temporary},");
                    passwordData.Append($"\"type\": \"password\",");
                    passwordData.Append($"\"value\": \"{userRegistration.Password}\"");
                    passwordData.Append("}");

                    using (HttpClient resetPasswordClient = new HttpClient())
                    {
                        using (HttpContent resetPasswordContent = new StringContent(passwordData.ToString()))
                        {
                            resetPasswordClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenResponse.Token.AccessToken);
                            resetPasswordContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                            var resetUrl = _apiConfig.UserEndpoint + userGuid + "/reset-password";

                            var response2 = await resetPasswordClient.PutAsync(resetUrl, resetPasswordContent);

                            var login = await Login(userRegistration.Username, userRegistration.Password);

                            return (response2.StatusCode.Equals(HttpStatusCode.NoContent)) ? tokenResponse.Token : new KeycloakToken();
                        }
                    }
                }
            }
        }


        /// <summary>
        /// Get user details
        /// </summary>
        /// <returns> returns all users associated in keycloak</returns>
        public async Task<UserDetail> GetUsers()
        {
            TokenResponse tokenResponse = await GetAdminLoginToken(_apiConfig.AdminUsername, _apiConfig.AdminPassword);

            using (HttpClient httpClient = new HttpClient())
            {
                using (HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(tokenResponse.Token.AccessToken)))
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenResponse.Token.AccessToken);
                    httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var response = await httpClient.GetAsync(_apiConfig.UserEndpoint);
                    if (response.StatusCode.Equals(HttpStatusCode.OK))
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        List<KeycloakUser> userList = JsonConvert.DeserializeObject<List<KeycloakUser>>(content.ToString());
                        return new UserDetail
                        {
                            KeycloakUser=userList
                        };
                    }

                    return new UserDetail();

                }
            }
        }
        /// <summary>
        /// Logout user which information is filled in Logout object.
        /// </summary>
        /// <param name="logout">Instance of Logout object with filled data.</param>
        /// <returns></returns>
        public async Task<bool> Logout(string accessToken, string refreshToken)
        {
            StringBuilder data = new StringBuilder();

            data.Append($"client_id={_apiConfig.ClientId}&");
            data.Append($"client_secret={_apiConfig.ClientSecret}&");
            data.Append($"refresh_token={refreshToken}");

            using (HttpClient httpClient = new HttpClient())
            {
                using (HttpContent httpContent = new StringContent(data.ToString()))
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                    httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

                    var response = await httpClient.PostAsync(_apiConfig.LogoutEndpoint, httpContent);

                    return response.StatusCode.Equals(HttpStatusCode.NoContent);
                }
            }
        }

        /// <summary>
        /// Delete user which information is filled in DeleteUser object.
        /// </summary>
        /// <param name="logout">Instance of DeleteUser object with filled data.</param>
        /// <returns></returns>
        public async Task<bool> DeleteUser(string AccessToken, string UserGuid)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);

                var response = await httpClient.DeleteAsync(_apiConfig.UserEndpoint + UserGuid);

                return response.StatusCode.Equals(HttpStatusCode.NoContent);
            }
        }

        /// <summary>
        /// Create new Keycloak client with filled KeycloakClient information and with access token of account that have rights to create new client.
        /// </summary>
        /// <param name="keycloakClient">Instance of KeycloakClient object with filled data.</param>
        /// <param name="accessToken">Access token of account that have rights to create new client.</param>
        /// <returns></returns>
        public async Task<bool> CreateClient(KeycloakClient keycloakClient, string accessToken)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                using (HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(keycloakClient)))
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                    httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var response = await httpClient.PostAsync(_apiConfig.ClientEndpoint, httpContent);
                    return response.StatusCode.Equals(HttpStatusCode.Created);
                }
            }
        }

        /// <summary>
        /// Create new Keycloak client with filled KeycloakClient information and with access token of account that have rights to create new client.
        /// </summary>
        /// <param name="keycloakClient">Instance of KeycloakClient object with filled data.</param>
        /// <param name="accessToken">Access token of account that have rights to create new client.</param>
        /// <returns></returns>
        public async Task<ICollection<KeycloakClient>> GetAllClients(string accessToken)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                using (HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(accessToken)))
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                    httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var response = await httpClient.GetAsync(_apiConfig.ClientEndpoint);

                    if (response.StatusCode.Equals(HttpStatusCode.OK))
                    {
                        return JsonConvert.DeserializeObject<ICollection<KeycloakClient>>(await response.Content.ReadAsStringAsync());
                    }

                    return new List<KeycloakClient>();
                }
            }
        }

        /// <summary>
        /// Delete existing client with provided guid of client and access token of account that have rights to delete client.
        /// </summary>
        /// <param name="clientGuid">Validi client guid.</param>
        /// <param name="accessToken">Access token of account that have rights to delete client.</param>
        /// <returns></returns>
        public async Task<bool> DeleteClient(string clientGuid, string accessToken)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var response = await httpClient.DeleteAsync(_apiConfig.ClientEndpoint + "/" + clientGuid);

                return response.StatusCode.Equals(HttpStatusCode.NoContent);
            }
        }

        /// <summary>
        /// Update existing client with filled instance of KeycloakClient object and valid client guid. 
        /// </summary>
        /// <param name="keycloakClient">Filled instance of KeycloakClient object.</param>
        /// <param name="clientGuid">Valid client guid.</param>
        /// <returns></returns>
        public async Task<bool> UpdateClient(KeycloakClient keycloakClient, string accessToken, string clientGuid)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                using (HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(keycloakClient)))
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                    httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var response = await httpClient.PutAsync(_apiConfig.ClientEndpoint + "/" + clientGuid, httpContent);

                    return response.StatusCode.Equals(HttpStatusCode.NoContent);
                }
            }
        }

        /// <summary>
        /// Reset existing user's password by userId and admin access token.
        /// </summary>
        /// <param name="accessToken">Access token</param>
        /// <param name="userId">User Id to whom we are going to reset password</param>
        /// <param name="newPassword">New password</param>
        /// <returns></returns>
        public async Task<bool> ResetPassword(string userId, string newPassword)
        {
            string _temporary = "false";

            StringBuilder passwordChangeData = new StringBuilder();

            passwordChangeData.Append("{");
            passwordChangeData.Append($"\"type\":\"password\",");
            passwordChangeData.Append($"\"value\": \"{newPassword}\",");
            passwordChangeData.Append($"\"temporary\": {_temporary}");
            passwordChangeData.Append("}");

            TokenResponse adminAccessToken = await GetAdminLoginToken(_apiConfig.AdminUsername, _apiConfig.AdminPassword);

            using (HttpClient httpClient = new HttpClient())
            {
                using (HttpContent httpContent = new StringContent(passwordChangeData.ToString()))
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", adminAccessToken.Token.AccessToken);
                    httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var response = await httpClient.PutAsync($"{_apiConfig.ResetPasswordEndpoint}/{userId}/reset-password", httpContent);
                    string content = await response.Content.ReadAsStringAsync();

                    return (response.StatusCode == HttpStatusCode.NoContent) ? true : false;
                }
            }
        }

        /// <summary>
        /// Try to login with provided client_id and client_secret. If client_id and client_secret are in-valide, 
        /// this method will return new empty KeycloakToken object.
        /// </summary>
        /// <returns></returns>
        public async Task<TokenResponse> GetResourceServerToken()
        {
            StringBuilder data = new StringBuilder();

            data.Append($"client_id={_apiConfig.ClientId}&");
            data.Append($"client_secret={_apiConfig.ClientSecret}&");
            data.Append($"grant_type=client_credentials&");

            using (HttpClient httpClient = new HttpClient())
            {
                using (HttpContent httpContent = new StringContent(data.ToString()))
                {
                    httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

                    var response = await httpClient.PostAsync(_apiConfig.LoginEndpoint, httpContent);
                    string content = await response.Content.ReadAsStringAsync();

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        return new TokenResponse
                        {
                            Token = JsonConvert.DeserializeObject<KeycloakToken>(content),
                            StatusCode = response.StatusCode
                        };
                    }
                    else
                    {
                        var resp = JsonConvert.DeserializeObject<Models.Response>(content);
                        return new TokenResponse
                        {
                            StatusCode = response.StatusCode,
                            Error = resp.Error,
                            ErrorDescription = resp.ErrorDescription
                        };
                    }
                }
            }
        }

        /// <summary>
        /// Method to return the list of the matching resources by resource uri.
        /// </summary>
        /// <param name="resourceUri"></param>
        /// <returns></returns>
        public async Task<IIAMResponse> GetResourcesByUri(string resourceUri)
        {
            TokenResponse resourceToken = await GetResourceServerToken();

            using (HttpClient httpClient = new HttpClient())
            {
                using (HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(resourceToken.Token.AccessToken)))
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", resourceToken.Token.AccessToken);
                    httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var response = await httpClient.GetAsync($"{_apiConfig.GetResourcesByUriEndPoint}?matchingUri=true&deep=true&max=-1&exactName=false&uri={resourceUri}");

                    if (response.StatusCode.Equals(HttpStatusCode.OK))
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        List<Resource> resources = JsonConvert.DeserializeObject<List<Resource>>(content.ToString());
                        if (resources.Count > 0)
                        {
                            _response.IsSuccess = true;
                            _response.Message = "SUCCESS";
                            _response.Result = resources;
                        }
                        else
                        {
                            _response.IsSuccess = false;
                            _response.Message = "No records found.";
                            _response.Result = null;
                        }
                        return _response;
                    }
                    _response.IsSuccess = false;
                    _response.Message = response.ReasonPhrase;
                    _response.Result = null;
                    return _response;
                }
            }
        }

        /// <summary>
        /// Validate users accessed resource.
        /// </summary>
        /// <param name="userToken">userToken: Logged-in user access token.</param>
        /// <param name="resourceUri">resourceUri: Logged-in user trying to access api/resource.</param>
        /// <returns></returns>
        public async Task<bool> ValidateResourcePermission(string userToken, string resourceUri)
        {
            var resResponse = this.GetResourcesByUri(resourceUri).Result;

            if (!resResponse.IsSuccess)
            {
                return false;
            }

            Resource resource = (Resource)resResponse.Result;

            if (resource == null)
            {
                return false;
            }

            StringBuilder data = new StringBuilder();

            data.Append($"audience={_apiConfig.ClientId}&");
            data.Append($"subject_token={userToken}&");
            data.Append($"permission={resource.Id}&");
            data.Append($"grant_type=urn:ietf:params:oauth:grant-type:uma-ticket&");

            using (HttpClient httpClient = new HttpClient())
            {
                using (HttpContent httpContent = new StringContent(data.ToString()))
                {
                    var byteArray = Encoding.ASCII.GetBytes($"{_apiConfig.ClientId}:{_apiConfig.ClientSecret}");
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                    httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

                    var response = await httpClient.PostAsync(_apiConfig.LoginEndpoint, httpContent);
                    string content = await response.Content.ReadAsStringAsync();

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var permToken = new TokenResponse
                        {
                            Token = JsonConvert.DeserializeObject<KeycloakToken>(content),
                            StatusCode = response.StatusCode
                        };

                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
    }
}
