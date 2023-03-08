using AutoMapper;
using XC.XSC.Repositories.SubmissionStage;
using XC.XSC.ViewModels.Configuration;
using XC.XSC.ViewModels.SubmissionStage;

namespace XC.XSC.Service.SubmissionStage
{
    /// <summary>
    /// This is the implementation class of ISubmissionStageService interface.In this class main buisness logic is implemented.
    /// </summary>
    public class SubmissionStageService : ISubmissionStageService
    {
        private readonly ISubmissionStageRepository _submissionStageRepository;
        private readonly IMapper _mapper;
        private readonly IResponse _response;
        
        public SubmissionStageService(ISubmissionStageRepository submissionStageRepository, IMapper mapper, IResponse response)
        {
            _submissionStageRepository = submissionStageRepository;
            _mapper = mapper;
            _response = response;
        }

        /// <summary>
        /// This method is the implementation of GetAllSubmissionStageAsync().
        /// </summary>
        /// <returns>It returns a list of SubmissionStage</returns>
        public Task<IResponse> GetAllSubmissionStageAsync()
        {
            var submissionStageList = _submissionStageRepository.GetAll();
            var submissionStageReadList = _mapper.Map<List<SubmissionStageResponse>>(submissionStageList);
            _response.IsSuccess = true;
            _response.Message = "SUCCESS";
            _response.Result = submissionStageReadList;
            return Task.FromResult(_response);
        }

        /// <summary>
        /// This method is the implementation of GetSubmissionStageByIdAsync() method.
        /// </summary>
        /// <param name="submissionStageId"></param>
        /// <returns>It returns SubmissionStage Object if provided Id record is present in database.
        /// It returns a null object in IResponse if provided Id is not not mached.</returns>
        public async Task<IResponse> GetSubmissionStageByIdAsync(int submissionStageId)
        {
            var submissionStage = await _submissionStageRepository.GetSingleAsync(x =>x.Id.Equals(submissionStageId));           
            if(submissionStage == null)
            {
                _response.IsSuccess = true;
                _response.Message = "NOT EXISTS";
                _response.Result = null;
                return _response;
            }
            else
            {
                var singleSubmissionStage = _mapper.Map<SubmissionStageResponse>(submissionStage);
                _response.IsSuccess = true;
                _response.Message = "SUCCESS";
                _response.Result = singleSubmissionStage;
                return _response;
            }           
        }

        /// <summary>
        /// This is the implementation of AddSubmissionStageAsync() methods.This method first checks for duplicacy.
        /// </summary>
        /// <param name="addSubmissionStageRequest"></param>
        /// <returns>This is void return type method.</returns>
        public async Task<IResponse> AddSubmissionStageAsync(AddSubmissionStageRequest addSubmissionStageRequest)
        {
            var submissionStageObj = await _submissionStageRepository.GetSingleAsync(x => x.Name == addSubmissionStageRequest.Name);

            if (submissionStageObj == null)
            {
                Models.Entity.SubmissionStage.SubmissionStage newSubmissionStatageAdd = _mapper.Map<Models.Entity.SubmissionStage.SubmissionStage>(addSubmissionStageRequest);
                newSubmissionStatageAdd.CreatedDate = DateTime.Now;
                await _submissionStageRepository.AddAsync(newSubmissionStatageAdd);
                _response.IsSuccess = true;
                _response.Message = "SUCCESS";
                _response.Result = newSubmissionStatageAdd;
                return _response;
            }
            _response.IsSuccess = true;
            _response.Message = "EXIST";
            _response.Result = null;
            return _response;
        }

        /// <summary>
        /// This method is the implementation of UpdateSubmissionStageAsync() method. 
        /// It helps to update the SubmissionStage record after checking the duplicacy.
        /// </summary>
        /// <param name="updateSubmissionStageRequest"></param>
        /// <returns>It returns an updated object of SubmissionStage</returns>
        public async Task<IResponse> UpdateSubmissionStageAsync(UpdateSubmissionStageRequest updateSubmissionStageRequest)
        {
            var submissionStageReadList = _submissionStageRepository.GetAll();
            var submissionStageObj = await _submissionStageRepository.GetSingleAsync(x => x.Name == updateSubmissionStageRequest.Name);

            if (submissionStageReadList.Any(o => o.Name == updateSubmissionStageRequest.Name))
            {
                _response.IsSuccess = true;
                _response.Message = "EXIST";
                _response.Result = null;
                return _response;
            }
            else
            {
                var submissionStageToUpdate = await _submissionStageRepository.GetSingleAsync(x => x.Id == updateSubmissionStageRequest.Id);
                Models.Entity.SubmissionStage.SubmissionStage newSubmissionStage = _mapper.Map<Models.Entity.SubmissionStage.SubmissionStage>(updateSubmissionStageRequest);
                submissionStageToUpdate.Name = newSubmissionStage.Name;
                submissionStageToUpdate.ModifiedDate = DateTime.Now;
                await _submissionStageRepository.UpdateAsync(submissionStageToUpdate);
                _response.IsSuccess = true;
                _response.Message = "SUCCESS";
                _response.Result = submissionStageToUpdate;
                return _response;
            }            
        }
    }
}
