using Newtonsoft.Json;
using System.Net;

namespace XC.CCMP.IAM
{
    public class Response
    {
        public HttpStatusCode StatusCode { get; set; }
        
        [JsonProperty("error")]
        public string Error { get; set; }

        [JsonProperty("error_description")]
        public string ErrorDescription { get; set; }
        
        public object Message { get; set; }
        
    }
}
