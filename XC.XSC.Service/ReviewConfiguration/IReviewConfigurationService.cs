
using XC.XSC.ViewModels.Configuration;
using XC.XSC.ViewModels.ReviewConfiguration;

namespace XC.XSC.Service.ReviewConfiguration
{
    /// <summary>
    /// Review configuration service interface.
    /// </summary>
    public interface IReviewConfigurationService
    {
        /// <summary>
        /// This method is used to add new review configuration to the table.
        /// </summary>
        /// <param name="reviewConfigurationRequest">review configuration request.</param>
        /// <returns>IResponse.</returns>
        Task<IResponse> SaveReviewConfigurationAsync(ReviewConfigurationRequest reviewConfigurationRequest);

        /// <summary>
        /// This method is used to update the review configuration entry.
        /// </summary>
        /// <param name="reviewConfigurationRequest">review configuration request.</param>
        /// <returns>IResponse.</returns>
        Task<IResponse> UpdateReviewConfigurationAsync(ReviewConfigurationUpdate reviewConfigurationUpdate);

        /// <summary>
        /// This method is used to delete a review configuration entry from the table.
        /// </summary>
        /// <param name="reviewConfigurationId">review configuration id.</param>
        /// <returns>IResponse.</returns>
        Task<IResponse> DeleteReviewConfigurationAsync(long reviewConfigurationId);

        /// <summary>
        /// This method is used to retrieve all the review configuration entrys from the table.
        /// </summary>
        /// <returns>IResponse.</returns>
        Task<IResponse> GetAllReviewConfigurationAsync();

        /// <summary>
        ///  This method is used to get the review configuration details by id or user id.
        /// </summary>
        /// <param name="id">review config id.</param>
        /// <param name="userId">user id.</param>
        /// <returns>IResponse.</returns>
        Task<IResponse> GetReviewConfig(long? id, bool? userId);
    }
}
