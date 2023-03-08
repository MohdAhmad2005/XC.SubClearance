
using Microsoft.AspNetCore.Http;
using System.Data;

namespace XC.XSC.ViewModels.User
{
    public enum ExecutionType
    {
        UnitTests = 0,
        ActualExecution = 1
    }

    public class UserInfo : IUserInfo
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEnumerable<System.Security.Claims.Claim> _claims;
        private readonly ExecutionType _executionType;

        /// <summary>
        /// User's first name.
        /// </summary>
        private string _firstName;

        /// <summary>
        /// User's middle name.
        /// </summary>
        private string _middleName;

        /// <summary>
        /// User's user name.
        /// </summary>
        private string _userName;

        /// <summary>
        /// User's email.
        /// </summary>
        private string _email;

        /// <summary>
        /// User's user id.
        /// </summary>
        private string _userId;

        /// <summary>
        /// User's token issuer.
        /// </summary>
        private string _issuer;

        /// <summary>
        /// User's application/client id
        /// </summary>
        private string _clientId;

        /// <summary>
        /// User's regions.
        /// </summary>
        private int[] _region;

        /// <summary>
        /// User's lob's.
        /// </summary>
        private int[] _lob;

        /// <summary>
        /// User's holiday list id.
        /// </summary>
        private int _holidayListId;

        /// <summary>
        /// User's manager id.
        /// </summary>
        private string _managerId;

        /// <summary>
        /// If user is manager, it will be true.
        /// </summary>
        private bool _isTeamManager;

        /// <summary>
        /// User's team id.
        /// </summary>
        private int _teamId;

        /// <summary>
        /// User's business details in json format.
        /// </summary>
        private string _businessDetails;

        /// <summary>
        /// User's Role
        /// </summary>

        private string _role;

        /// <summary>
        /// Managing the user claims.
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <param name="executionType"></param>
        public UserInfo(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

            //if(!ExecutionType.Equals(ExecutionType.UnitTests))
            //{
            //_claims = ((System.Security.Claims.ClaimsIdentity)_httpContextAccessor.HttpContext.User.Identity).Claims;
            //}
            if (_httpContextAccessor != null)
            {
                this._executionType = ExecutionType.ActualExecution;
                _claims = ((System.Security.Claims.ClaimsIdentity)_httpContextAccessor.HttpContext.User.Identity).Claims;
            }

            setValues();
        }

        /// <summary>
        /// Set the properties value from claims.
        /// </summary>
        private void setValues()
        {
            if (!this._executionType.Equals(ExecutionType.UnitTests))
            {
                if (_claims.Select(c => c).Where(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname").Any())
                {
                    this._firstName = _claims.Select(c => c).Where(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname").FirstOrDefault().Value;
                }

                if (_claims.Select(c => c).Where(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname").Any())
                {
                    this._middleName = _claims.Select(c => c).Where(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname").FirstOrDefault().Value;
                }

                if (_claims.Select(c => c).Where(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Any())
                {
                    this._userName = _claims.Select(c => c).Where(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").FirstOrDefault().Value;
                }

                if (_claims.Select(c => c).Where(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Any())
                {
                    this._email = _claims.Select(c => c).Where(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").FirstOrDefault().Value;
                }

                if (_claims.Select(c => c).Where(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Any())
                {
                    this._userId = _claims.Select(c => c).Where(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").FirstOrDefault().Value;
                }

                if (_claims.Select(c => c).Where(c => c.Type == "iss").Any())
                {
                    this._issuer = _claims.Select(c => c).Where(c => c.Type == "iss").FirstOrDefault().Value;
                }

                if (_claims.Select(c => c).Where(c => c.Type == "azp").Any())
                {
                    this._clientId = _claims.Select(c => c).Where(c => c.Type == "azp").FirstOrDefault().Value;
                }

                if (_claims.Select(c => c).Where(c => c.Type == "RegionId").Any())
                {
                    this._region = _claims.Select(c => c).Where(c => c.Type == "RegionId").Select(r => Convert.ToInt32(r.Value)).ToArray();
                }

                if (_claims.Select(c => c).Where(c => c.Type == "LobId").Any())
                {
                    this._lob = _claims.Select(c => c).Where(c => c.Type == "LobId").Select(r => Convert.ToInt32(r.Value)).ToArray();
                }

                if (_claims.Select(c => c).Where(c => c.Type == "HolidayListId").Any())
                {
                    this._holidayListId = _claims.Select(c => c).Where(c => c.Type == "HolidayListId").Select(r => Convert.ToInt32(r.Value)).FirstOrDefault();
                }

                if (_claims.Select(c => c).Where(c => c.Type == "ManagerId").Any())
                {
                    this._managerId = _claims.Select(c => c).Where(c => c.Type == "ManagerId").Select(r => r.Value).FirstOrDefault();
                }

                if (_claims.Select(c => c).Where(c => c.Type == "IsTeamManager").Any())
                {
                    this._isTeamManager = _claims.Select(c => c).Where(c => c.Type == "IsTeamManager").Select(r => Convert.ToBoolean(r.Value)).FirstOrDefault();
                }

                if (_claims.Select(c => c).Where(c => c.Type == "TeamId").Any())
                {
                    this._teamId = _claims.Select(c => c).Where(c => c.Type == "TeamId").Select(r => Convert.ToInt32(r.Value)).FirstOrDefault();
                }

                if (_claims.Select(c => c).Where(c => c.Type == "BusinessDetails").Any())
                {
                    this._businessDetails = _claims.Select(c => c).Where(c => c.Type == "BusinessDetails").Select(r => r.Value).FirstOrDefault();
                }

                if (_claims.Select(c => c).Where(c => c.Type == "Role").Any())
                {
                    this._role = _claims.Select(c => c).Where(c => c.Type == "Role").Select(r => r.Value).FirstOrDefault();
                }
            }

        }

        /// <summary>
        /// First Name of logged-in user
        /// </summary>
        public string FirstName
        {
            get => this._firstName;
            set
            {
                if (string.IsNullOrEmpty(_firstName))
                {
                    _firstName = value;
                }
            }
        }

        /// <summary>
        /// Middle Name of logged-in user.
        /// </summary>
        public string MiddleName
        {
            get => this._middleName;
            set
            {
                if (string.IsNullOrEmpty(_middleName))
                {
                    _middleName = value;
                }
            }
        }

        /// <summary>
        /// User Name of logged-in user.
        /// </summary>
        public string UserName
        {
            get => this._userName;
            set
            {
                if (string.IsNullOrEmpty(_userName))
                {
                    _userName = value;
                }
            }
        }

        /// <summary>
        /// Email Id of logged-in user.
        /// </summary>
        public string Email
        {
            get => this._email;
            set
            {
                if (string.IsNullOrEmpty(_email))
                {
                    _email = value;
                }
            }
        }

        /// <summary>
        /// Uniqueue user id of logged-in user.
        /// </summary>
        public string UserId
        {
            get => this._userId;
            set
            {
                if (string.IsNullOrEmpty(_userId))
                {
                    _userId = value;
                }
            }
        }

        /// <summary>
        /// TenantId/RealmId/OrganizationId of logged-in user.
        /// </summary>
        public string TenantId
        {
            get
            {
                return this.Realm;
            }
        }

        /// <summary>
        /// Realm/Organization of logged-in user.
        /// </summary>
        public string Realm
        {
            get
            {
                int lastIndexOfSlash = this.Issuer.LastIndexOf('/');
                return this.Issuer.Substring(lastIndexOfSlash + 1);
            }
        }

        /// <summary>
        /// Token issuer
        /// </summary>
        public string Issuer
        {
            get => this._issuer;
            set
            {
                if (string.IsNullOrEmpty(_issuer))
                {
                    _issuer = value;
                }
            }
        }

        /// <summary>
        /// Client Application Id.
        /// </summary>
        public string ClientId
        {
            get => this._clientId;
            set
            {
                if (string.IsNullOrEmpty(_clientId))
                {
                    _clientId = value;
                }
            }
        }

        /// <summary>
        /// User's region.
        /// </summary>
        public int[] Region
        {
            get => this._region;
            set
            {
                _region = value;
            }
        }

        /// <summary>
        /// User's lob.
        /// </summary>
        public int[] Lob
        {
            get => this._lob;
            set
            {
                _lob = value;
            }
        }

        /// <summary>
        /// User's holiday list id.
        /// </summary>
        public int HolidayListId
        {
            get => this._holidayListId;
            set
            {
                _holidayListId = value;
            }
        }

        /// <summary>
        /// User's manager id.
        /// </summary>
        public string ManagerId
        {
            get => this._managerId;
            set
            {
                if (string.IsNullOrEmpty(_managerId))
                {
                    _managerId = value;
                }
            }
        }

        /// <summary>
        /// If user is manager, it will be true.
        /// </summary>
        public bool IsTeamManager
        {
            get => this._isTeamManager;
            set
            {
                _isTeamManager = value;
            }
        }

        /// <summary>
        /// User's Team Id
        /// </summary>
        public int TeamId
        {
            get => this._teamId;
            set
            {
                _teamId = value;
            }
        }

        /// <summary>
        /// User's business details in json format.
        /// </summary>
        public string BusinessDetails
        {
            get => this._businessDetails;
            set
            {
                _businessDetails = value;
            }
        }

        /// <summary>
        /// User's Role
        /// </summary>
        public string Role
        {
            get => this._role;
            set
            {
                _role = value;
            }
        }
    }
}
