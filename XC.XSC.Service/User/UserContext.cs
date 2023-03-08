using Microsoft.AspNetCore.Http;
using XC.XSC.ViewModels.User;

namespace XC.XSC.Service.User
{
    public class UserContext : IUserContext
    {
        public readonly IUserInfo _userInfo;

        /// <summary>
        /// User context of the logged-in user.
        /// </summary>
        /// <param name="userInfo"></param>
        public UserContext(IUserInfo userInfo)
        {
            _userInfo = userInfo;
        }

        /// <summary>
        /// User's claims information.
        /// </summary>
        public IUserInfo UserInfo
        {
            get
            {
                return _userInfo;

            }
        }
    }    

    /// <summary>
    /// Test User context created to use in unit tests.
    /// </summary>
    public class TestUserContext : IUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Test user context constructructor.
        /// </summary>
        public TestUserContext()
        {
            var abc = this.UserInfo;
        }

        public IUserInfo UserInfo
        {
            get
            {
                return new UserInfo(_httpContextAccessor)
                {
                    UserId = "23q2w3e4r5t6y7sdf6tg",
                    FirstName = "Test",
                    MiddleName = "User",
                    Email = "testuser@test.com",
                    UserName = "testuser@test.com",
                    Issuer = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/xceedance"
                };

            }
        }
    }
}
