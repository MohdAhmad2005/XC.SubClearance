namespace XC.XSC.EMS
{
    public class EmsApiConfig: IEmsApiConfig
    {

        /// <summary>
        /// Base url to access the UAM application.
        /// </summary>
        public string BaseUrl { get; set; }

        /// <summary>
        /// Property to return the get region list url.
        /// </summary>
        public string EmailBoxListEndPoint
        {
            get
            {
                return $"{BaseUrl}api/ems/MailBox/getEmailBoxList";
            }
        }

    }
}