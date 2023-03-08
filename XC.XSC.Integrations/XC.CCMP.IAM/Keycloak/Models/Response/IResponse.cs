using System.ComponentModel;

namespace XC.CCMP.IAM.Keycloak.Models
{
    /// <summary>
    /// Models to return the response.
    /// </summary>
    public interface IIAMResponse
    {
        /// <summary>
        /// Check the success of operations.
        /// </summary>
        [DefaultValue(false)]
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Message returned from operations.
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// Result of operations.
        /// </summary>
        public object? Result { get; set; }
    }
}
