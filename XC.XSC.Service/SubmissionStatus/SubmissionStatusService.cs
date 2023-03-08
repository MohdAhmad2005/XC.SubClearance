using AutoMapper;
using XC.XSC.Repositories.SubmissionStatus;
using XC.XSC.ViewModels.Configuration;
using XC.XSC.ViewModels.SubmissionStatus;

namespace XC.XSC.Service.SubmissionStatus
{
    /// <summary>
    /// This is the implementation class of ISubmissionStatusService interface.In this class main buisness logic is implemented.
    /// </summary>
    public class SubmissionStatusService : ISubmissionStatusService
    {
        private readonly ISubmissionStatusRepository _submissionStatusRepository;
        private readonly IMapper _mapper;
        private readonly IResponse _response;

        public SubmissionStatusService(ISubmissionStatusRepository submissionStatusRepository, IResponse response,IMapper mapper)
        {
            _submissionStatusRepository = submissionStatusRepository;
            _response = response;
            _mapper = mapper;
        }

        /// <summary>
        /// This is implemented to get all records from the database.
        /// </summary>
        /// <returns>IResponse (IsSuccess, Message, Result)</returns>
        public Task<IResponse> GetSubmissionStatusListAsync()
        {
            var submissionStatuslist = _submissionStatusRepository.GetAll();
            var submissionStatusReadList = _mapper.Map<List<SubmissionStatusResponse>>(submissionStatuslist);
            _response.IsSuccess = true;
            _response.Message = "SUCCESS";
            _response.Result = submissionStatusReadList;
            return Task.FromResult(_response);
        }

        /// <summary>
        /// This method is implemented to Add a new record in the database.It will add new record if and only if provide SubmissionStatus name in the request body is not exist in the database.
        /// </summary>
        /// <param name="addSubmissionStatusRequest"></param>
        /// <returns>IResponse (IsSuccess, Message, Result)</returns>

        public async Task<IResponse> AddSubmissionStatusAsync(AddSubmissionStatusRequest addSubmissionStatusRequest)
            {
            var getSingleSubmissionStatus = await _submissionStatusRepository.GetSingleAsync(x => x.Name == addSubmissionStatusRequest.Name);
            if(getSingleSubmissionStatus == null)
            {
                Models.Entity.SubmissionStatus.SubmissionStatus newSubmissionStatusAdd = _mapper.Map<Models.Entity.SubmissionStatus.SubmissionStatus>(addSubmissionStatusRequest);
                newSubmissionStatusAdd.CreatedDate = DateTime.Now;
                await _submissionStatusRepository.AddAsync(newSubmissionStatusAdd);
                _response.IsSuccess = true;
                _response.Message = "SUCCESS";
                _response.Result = newSubmissionStatusAdd;
                return _response;
        }
            else
            {
                _response.IsSuccess = false;
                _response.Message = "EXIST";
                _response.Result = null;
                return _response;
            }
        }

        /// <summary>
        /// This method is implemented to Update an existing record based on provide Id in the request body.It will update the record if and only if provide SubmissionStatus name in the request body is not exist in the database.
        /// </summary>
        /// <param name="updateSubmissionStatus"></param>
        /// <returns>IResponse (IsSuccess, Message, Result)</returns>

        public async Task<IResponse> UpdateSubmissionStatusAsync(UpdateSubmissionStatusRequest updateSubmissionStatus)
        {
            var submissionStatusList = _submissionStatusRepository.GetAll();
  

            if (submissionStatusList.Any(o => o.Name == updateSubmissionStatus.Name))
            {
                _response.IsSuccess = false;
                _response.Message = "EXIST";
                _response.Result = null;
                return _response; 
            }
            else
            {
                var submissionStatusToUpdate = await _submissionStatusRepository.GetSingleAsync(x => x.Id == updateSubmissionStatus.Id);
                if(submissionStatusToUpdate == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "NOT EXIST";
                    _response.Result = null;
                    return _response;
                }
                Models.Entity.SubmissionStatus.SubmissionStatus newSubmissionStatus = _mapper.Map<Models.Entity.SubmissionStatus.SubmissionStatus>(updateSubmissionStatus);
                submissionStatusToUpdate.Name = newSubmissionStatus.Name;
                submissionStatusToUpdate.ModifiedDate = DateTime.Now;
                await _submissionStatusRepository.UpdateAsync(submissionStatusToUpdate);
                _response.IsSuccess = true;
                _response.Message = "SUCCESS";
                _response.Result = submissionStatusToUpdate;
                return _response;
            }
        }

        /// <summary>
        /// This method is implemented to get a single submission status record based on provided Id.
        /// </summary>
        /// <param name="submissionStatusId"></param>
        /// <returns>IResponse (IsSuccess, Message, Result)</returns>

        public async Task<IResponse> GetSubmissionStatusByIdAsync(int submissionStatusId)
        {
            var submissionStatus = await _submissionStatusRepository.GetSingleAsync(x=>x.Id.Equals(submissionStatusId));
            if (submissionStatus == null)
            {
                _response.IsSuccess = true;
                _response.Message = "NOT EXISTS";
                _response.Result = null;
                return _response;
            }
            else
            {
                var singleSubmissionStatus = _mapper.Map<SubmissionStatusResponse>(submissionStatus);
                _response.IsSuccess = true;
                _response.Message = "SUCCESS";
                _response.Result = singleSubmissionStatus;
                return _response;
            }
        }

        ///<summary>
        ///Get all submission status from the table based on tenant id.
        /// </summary>
        /// <param name="tenantId">tenant Id of the corresponding user.</param>
        /// <returns>return the submission status response.</returns>
        public async Task<List<SubmissionStatusResponse>> GetAllSubmissionStatusAsync(string tenantId)
        {
            var submissionStatusList = _submissionStatusRepository.GetAll().Where(item=>item.TenantId == tenantId && item.IsActive == true).OrderBy(item=>item.OrderNo);
            if (submissionStatusList.Any())
            {
                var result = submissionStatusList.AsQueryable().Select(s => new SubmissionStatusResponse()
                {
                    Color = s.Color,
                    Id = s.Id,
                    Label = s.Label,
                    Name = s.Name,
                }).ToList();
                return await Task.FromResult(result);
            }
            else return await Task.FromResult(new List<SubmissionStatusResponse>());
        }
    }
}
