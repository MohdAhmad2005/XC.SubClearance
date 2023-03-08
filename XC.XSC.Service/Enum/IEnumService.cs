using XC.XSC.ViewModels.Enum;

namespace XC.XSC.Service.IAM
{
    public interface IEnumService
    {

        /// <summary>
        /// Get the list of enum key and value pair.
        /// </summary>
        /// <typeparam name="T">Enum Name</typeparam>
        /// <returns>Return the list of the enum key and value pairs.</returns>
        Task<List<EnumResponse>?> EnumNamedValues(string enumName);

    }
}
