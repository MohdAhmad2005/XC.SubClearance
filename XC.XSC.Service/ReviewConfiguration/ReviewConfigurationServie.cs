
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using XC.XSC.Repositories.ReviewConfiguration;
using XC.XSC.Service.User;
using XC.XSC.UAM.Models;
using XC.XSC.UAM.UAM;
using XC.XSC.ViewModels.Configuration;
using XC.XSC.ViewModels.ReviewConfiguration;
using Attribute = XC.XSC.UAM.Models.Attribute;

namespace XC.XSC.Service.ReviewConfiguration
{
    /// <summary>
    /// Review configuration service class.
    /// </summary>
    public class ReviewConfigurationServie : IReviewConfigurationService
    {
        private readonly IUserContext _userContext;
        private readonly IResponse _response;
        private readonly IMapper _mapper;
        private readonly IReviewConfigurationRepository _reviewConfigurationRepository;
        private readonly IUamService _uamService;

        /// <summary>
        /// Review configuration service constructor.
        /// </summary>
        /// <param name="userContext">User context instance.</param>
        /// <param name="response">IResponse instance.</param>
        /// <param name="mapper">Mapper instance.</param>
        /// <param name="reviewConfigurationRepository">Review configuration repository instance.</param>
        public ReviewConfigurationServie(IUserContext userContext, IResponse response, IMapper mapper, IReviewConfigurationRepository reviewConfigurationRepository, IUamService uamService)
        {
            _userContext = userContext;
            _response = response;
            _mapper = mapper;
            _reviewConfigurationRepository = reviewConfigurationRepository;
            _uamService = uamService;
        }

        /// <summary>
        /// This method is used to add new review configuration to the table.
        /// </summary>
        /// <param name="reviewConfigurationRequest">review configuration request.</param>
        /// <returns>IResponse.</returns>
        public async Task<IResponse> SaveReviewConfigurationAsync(ReviewConfigurationRequest reviewConfigurationRequest)
        {
            var reviewAlreadyExists = _reviewConfigurationRepository.GetAll().Where(item=>item.TenantId == _userContext.UserInfo.TenantId)
                                      .Select(item => item.ProcessorId).ToList()
                                      .Intersect(reviewConfigurationRequest.ProcessorId);
            if (reviewAlreadyExists.Any())
            {
                _response.IsSuccess = false;
                _response.Message = "The Review Management for Selected Processor has already been created";
            }
            else
            {
                foreach (var item in reviewConfigurationRequest.ProcessorId)
                {
                    var reviewConfiguration = _mapper.Map<Models.Entity.ReviewConfiguration.ReviewConfiguration>(reviewConfigurationRequest);
                        reviewConfiguration.ProcessorId = item;
                        reviewConfiguration.TenantId = _userContext.UserInfo.TenantId;
                        reviewConfiguration.CreatedBy = _userContext.UserInfo.UserId;
                        reviewConfiguration.CreatedDate = DateTime.Now;
                        reviewConfiguration.IsActive = true;
                        await _reviewConfigurationRepository.AddAsync(reviewConfiguration);
                }
                _response.IsSuccess = true;
                _response.Message = "Successfully completed";
            }
            return _response;
        }

        /// <summary>
        /// This method is used to update the review configuration entry.
        /// </summary>
        /// <param name="reviewConfigurationRequest">review configuration request.</param>
        /// <returns>IResponse.</returns>
        public async Task<IResponse> UpdateReviewConfigurationAsync(ReviewConfigurationUpdate reviewConfigurationUpdate)
        {
                var reviewConfiguration = await _reviewConfigurationRepository.GetSingleAsync(a => a.Id == reviewConfigurationUpdate.Id
                              && a.TenantId == _userContext.UserInfo.TenantId);
                if (reviewConfiguration != null)
                {
                        reviewConfiguration.ProcessorId = reviewConfigurationUpdate.ProcessorId;
                        reviewConfiguration.LobId = reviewConfigurationUpdate.LobId;
                        reviewConfiguration.ReviewerId = reviewConfigurationUpdate.ReviewerId;
                        reviewConfiguration.IsActive = reviewConfigurationUpdate.IsActive;
                        reviewConfiguration.RegionId = reviewConfigurationUpdate.RegionId;
                        reviewConfiguration.ReviewType = reviewConfigurationUpdate.ReviewTypeId;
                        reviewConfiguration.TeamId = reviewConfigurationUpdate.TeamId;
                        reviewConfiguration.ModifiedBy = _userContext.UserInfo.UserId;
                        reviewConfiguration.ModifiedDate = DateTime.Now;
                        await _reviewConfigurationRepository.UpdateAsync(reviewConfiguration);                  
                    _response.IsSuccess = true;
                    _response.Message = "Successfully completed";
                }
                else
                {
                _response.Result = null;
                _response.IsSuccess = false;
                _response.Message = "Invalid review configuration id.";
                }
            return _response;
        }

        /// <summary>
        /// This method is used to delete a review configuration entry from the table.
        /// </summary>
        /// <param name="reviewConfigurationId">review configuration id.</param>
        /// <returns>IResponse.</returns>
        public async Task<IResponse> DeleteReviewConfigurationAsync(long reviewConfigurationId)
        {
            var reviewConfiguration = await _reviewConfigurationRepository.GetSingleAsync(a => a.Id == reviewConfigurationId
                                      && a.TenantId == _userContext.UserInfo.TenantId);
            if (reviewConfiguration != null)
            {
                await _reviewConfigurationRepository.DeleteAsync(item => item.Id == reviewConfigurationId);
                _response.IsSuccess = true;
                _response.Message = "Successfully completed";
            }
            else
            {
                _response.Result = null;
                _response.IsSuccess = false;
                _response.Message = "Invalid review configuration id.";
            }
            return _response;
        }

        /// <summary>
        /// This method is used to retrieve all the review configuration entrys from the table.
        /// </summary>
        /// <returns>IResponse.</returns>
        public async Task<IResponse> GetAllReviewConfigurationAsync()
        {
            var reviewConfiguration = await _reviewConfigurationRepository.GetAll().
                                      Where(item => item.TenantId == _userContext.UserInfo.TenantId).
                                      OrderByDescending(item => item.CreatedDate).ToListAsync();
            if (reviewConfiguration != null)
            {
                List<Region> regionLookup = new List<Region>();
                List<UserResponseResult> userListLookup = new List<UserResponseResult>();
                List<TeamModel> teamLookup= new List<TeamModel>();
                var regionList = await _uamService.GetUserRegions();
                if (regionList.Result != null)
                {
                    regionLookup = ((List<Region>)regionList.Result);
                }
                UserFilterRequest userFilterRequest = new UserFilterRequest();
                var usersList = await _uamService.GetUsersByFilters(userFilterRequest);
                if (usersList.Result != null)
                {
                    userListLookup = (List<UserResponseResult>)usersList.Result;
                }
                var teamList = await _uamService.GetTeamList();
                if(teamList.Result != null)
                {
                    teamLookup =(List<TeamModel>)teamList.Result;
                }
                var result = reviewConfiguration.AsEnumerable().Select(item => new ReviewConfigurationResponse()
                {
                    Id = item.Id,
                    IsActive = item.IsActive,
                    RegionName = regionLookup.Count > 0 ? regionLookup.FirstOrDefault(r => r.Id == item.RegionId).RegionName : string.Empty,
                    LobId = item.LobId,
                    LobName = item.Lob.Name,
                    ProcessorId = item.ProcessorId,
                    ProcessorName = userListLookup.Count > 0 ? userListLookup.Where(r => r.Id == item.ProcessorId).
                                    Select(s => string.Format("{0} {1}", s.FirstName, s.LastName)).FirstOrDefault() : string.Empty,
                    ReviewerName = userListLookup.Count > 0 ? userListLookup.Where(r => r.Id == item.ReviewerId).
                                    Select(s => string.Format("{0} {1}", s.FirstName, s.LastName)).FirstOrDefault() : string.Empty,
                    RegionId = item.RegionId,
                    ReviewerId = item.ReviewerId,
                    ReviewTypeId = (int)item.ReviewType,
                    ReviewType = item.ReviewType.GetType().GetMember(item.ReviewType.ToString()).First()
                                .GetCustomAttribute<DisplayAttribute>().Name,
                    TeamId = item.TeamId,
                    TeamName = teamLookup.Count > 0 ? teamLookup.FirstOrDefault(r => r.Id == item.TeamId).Name : string.Empty
                }).ToList();
                _response.Result = result;
                _response.IsSuccess = true;
                _response.Message = "Success";
            }
            else
            {
                _response.Result = null;
                _response.IsSuccess = false;
                _response.Message = "No result found.";
            }
            return await Task.FromResult(_response);
        }

        /// <summary>
        ///  This method is used to get the review configuration details by id or user id.
        /// </summary>
        /// <param name="id">review config id.</param>
        /// <param name="fetchFromUserId">user id.</param>
        /// <returns>IResponse.</returns>
        public async Task<IResponse> GetReviewConfig(long? id, bool? userId)
        {
            Models.Entity.ReviewConfiguration.ReviewConfiguration reviewConfiguration = null;
            if(userId.HasValue && id.HasValue)
            {
                reviewConfiguration = await _reviewConfigurationRepository.GetSingleAsync(item => item.Id == id
                          && item.TenantId == _userContext.UserInfo.TenantId && item.ProcessorId == _userContext.UserInfo.UserId && item.IsActive);
            }
            else if(id.HasValue && userId.HasValue == false) {
                reviewConfiguration = await _reviewConfigurationRepository.GetSingleAsync(item => item.Id == id
                           && item.TenantId == _userContext.UserInfo.TenantId && item.IsActive);
            }
            else if(id == null && userId.HasValue)
            {
                reviewConfiguration = await _reviewConfigurationRepository.GetSingleAsync(item => item.TenantId == _userContext.UserInfo.TenantId
                && item.ProcessorId == _userContext.UserInfo.UserId && item.IsActive);
            }
            if (reviewConfiguration != null && reviewConfiguration.Id > 0)
            {
                List<Region> regionLookup = new List<Region>();
                List<UserResponseResult> userListLookup = new List<UserResponseResult>();
                List<TeamModel> teamLookup = new List<TeamModel>();
                var regionList = await _uamService.GetUserRegions();
                if (regionList.Result != null)
                {
                    regionLookup = ((List<Region>)regionList.Result);
                }
                var usersList = await _uamService.GetUsersByFilters(new UserFilterRequest());
                if (usersList.Result != null)
                {
                    userListLookup = (List<UserResponseResult>)usersList.Result;
                }
                var teamList = await _uamService.GetTeamList();
                if (teamList.Result != null)
                {
                    teamLookup = (List<TeamModel>)teamList.Result;
                }
                _response.Result = new ReviewConfigurationResponse()
                {
                    Id = reviewConfiguration.Id,
                    IsActive = reviewConfiguration.IsActive,
                    LobId = reviewConfiguration.LobId,
                    LobName = reviewConfiguration.Lob.Name,
                    ProcessorId = reviewConfiguration.ProcessorId,
                    RegionId = reviewConfiguration.RegionId,
                    ReviewerId = reviewConfiguration.ReviewerId,
                    ReviewType = reviewConfiguration.ReviewType.GetType().GetMember(reviewConfiguration.ReviewType.ToString()).First()
                                .GetCustomAttribute<DisplayAttribute>().Name,
                    RegionName = regionLookup.Count > 0 ? regionLookup.FirstOrDefault(r => r.Id == reviewConfiguration.RegionId).RegionName : string.Empty,
                    ReviewTypeId = (int)reviewConfiguration.ReviewType,
                    TeamId = reviewConfiguration.TeamId,
                    ProcessorName = userListLookup.Count > 0 ? userListLookup.Where(r => r.Id == reviewConfiguration.ProcessorId).
                                    Select(s => string.Format("{0} {1}", s.FirstName, s.LastName)).FirstOrDefault() : string.Empty,
                    ReviewerName = userListLookup.Count > 0 ? userListLookup.Where(r => r.Id == reviewConfiguration.ReviewerId).
                                    Select(s => string.Format("{0} {1}", s.FirstName, s.LastName)).FirstOrDefault() : string.Empty,
                    TeamName = teamLookup.Count > 0 ? teamLookup.FirstOrDefault(r => r.Id == reviewConfiguration.TeamId).Name : string.Empty
                };
                _response.IsSuccess = true;
                _response.Message = "Success";
            }
            else
            {
                _response.Result = null;
                _response.IsSuccess = false;
                _response.Message = "Review details are not found";
            }
            return _response;
        }
    }
}
