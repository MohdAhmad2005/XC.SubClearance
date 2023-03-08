namespace XC.XSC.ViewModels.User
{
    public interface IUserInfo
    {     

        /// <summary>
        /// First Name of logged-in user
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Middle Name of logged-in user.
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// User Name of logged-in user.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Email Id of logged-in user.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Uniqueue user id of logged-in user.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// TenantId/RealmId/OrganizationId of logged-in user.
        /// </summary>
        public string TenantId { get;}

        /// <summary>
        /// Realm/Organization of logged-in user.
        /// </summary>
        public string Realm { get; }

        /// <summary>
        /// Token issuer
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// Client Application Id.
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// User's region.
        /// </summary>
        public int[] Region { get; set; }

        /// <summary>
        /// User's lob.
        /// </summary>
        public int[] Lob { get; set; }

        /// <summary>
        /// User's holiday list id.
        /// </summary>
        public int HolidayListId { get; set; }

        /// <summary>
        /// User's manager id.
        /// </summary>
        public string ManagerId { get; set; }

        /// <summary>
        /// If user is manager, it will be true.
        /// </summary>
        public bool IsTeamManager { get; set; }

        /// <summary>
        /// User's Team Id
        /// </summary>
        public int TeamId { get; set; }

        /// <summary>
        /// User's business details in json format.
        /// </summary>
        public string BusinessDetails { get; set; }

        /// <summary>
        /// User's Role
        /// </summary>
        public string Role { get; set; }
    }
}
