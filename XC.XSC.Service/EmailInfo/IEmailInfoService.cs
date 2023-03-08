using XC.XSC.ViewModels.Configuration;
using XC.XSC.ViewModels.EmailInfo;

namespace XC.XSC.Service.EmailInfo
{
    /// <summary>
    /// This interface is responsible for operations happens on Email Info.
    /// </summary>
    public interface IEmailInfoService
    {
        Task<IResponse> SaveEmailInfo(AddEmailInfoRequest emailInfoRequest);

        ///<summary>
        ///Get email info details based on the email info id.
        /// </summary>
        /// <param name="emailInfoId">email info id.</param>
        /// <returns>return the single email info response as common IResponse.</returns>
        Task<IResponse> GetEmailInfoDetailByIdAsync(long emailInfoId);
    }
}
