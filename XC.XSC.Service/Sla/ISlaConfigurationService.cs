using XC.XSC.ViewModels.Configuration;
using XC.XSC.ViewModels.EmailInfo;
using XC.XSC.ViewModels.Enum;
using XC.XSC.ViewModels.Paging;
using XC.XSC.ViewModels.Sla;
using XC.XSC.ViewModels.Submission;
namespace XC.XSC.Service.Sla
{
    public interface ISlaConfigurationService
    {
        /// <summary>
        /// To Get the Sla Configuration detail based on Sla fire the job
        /// </summary>
        /// <param name="request">
        /// TeamId,
        /// RegionId,
        /// LobId,
        /// MailBoxId,
        /// slaType
        /// </param>
        /// <returns>Success</returns>
        Task<IResponse> GetSlaConfiguration(int regionId, int teamdId, int lobId, SlaType slaType, Guid mailBoxId);

        /// <summary>
        /// To Save the Sla Configuration detail based on Sla fire the job
        /// </summary>
        /// <param name="request">
        /// TeamId,
        /// RegionId,
        /// LobId,
        /// MailBoxId,
        /// slaType
        /// </param>
        /// <returns>Success</returns>
        Task<IResponse> SaveSlaConfiguration(SlaConfigurationRequest request);

        /// <summary>
        /// To Save the Sla Configuration detail based on Sla fire the job
        /// </summary>
        /// <param name="RegionId">Region Id</param>
        /// <param name="TeamdId">Team Id</param>
        /// <param name="LobId"> Lob Id</param>
        /// <param name="slaType">SLA Type</param>
        /// <param name="mailBoxId">Mail Box Guid</param>
        /// <param name="tenantId">Tenant Id</param>
        /// <returns></returns>
        Task<IResponse> GetSlaConfiguration(int RegionId, int TeamdId, int LobId, SlaType slaType, Guid mailBoxId, string tenantId);

        /// <summary>
        /// To Get the Update Sla Configuration detail based on Sla fire the job
        /// </summary>
        /// <param name="request">
        /// TeamId,
        /// RegionId,
        /// LobId,
        /// MailBoxId,
        /// slaType
        /// </param>
        /// <returns>Success</returns>
        Task<IResponse> UpdateSlaConfiguration(SlaConfigurationRequest request);

        /// <summary>
        /// To Get the All Sla Configuration detail based on Sla fire the job
        /// </summary>
        /// <param name="request">
        /// TeamId,
        /// RegionId,
        /// LobId,
        /// MailBoxId,
        /// slaType
        /// </param>
        /// <returns>Success</returns>
        Task<IResponse> getAllSlas(GetSlaConfigurationRequest getSlaRequest);
        /// <summary>
        /// Calculate DueDate based on submission created date,region,team,lob and mailBoxId
        /// </summary>
        /// <param name="submission"></param>
        /// <param name="emailInfoRequest"></param>
        /// <returns>dueDate</returns>
        Task<DateTime> CalculateDueDate(SubmissionRequest submission, AddEmailInfoRequest emailInfoRequest);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SlaId"></param>
        /// <returns></returns>
        Task<IResponse> getSlaConfigurationbyId(long SlaId);

        /// <summary>
        /// Get Al all the dtails 
        /// </summary>
        
        Task<IResponse> getAllSlaDetail();

    }
}
