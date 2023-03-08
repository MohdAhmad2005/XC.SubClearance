namespace XC.CCMP.IAM.Keycloak.Models
{
    public class IAMOperationResponse:IIAMResponse
    {
        /// <summary>
        /// Success status of operation.
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Message of operation exexuted.
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// Result of operation returned.
        /// </summary>
        public object? Result { get; set; }
    }
}
