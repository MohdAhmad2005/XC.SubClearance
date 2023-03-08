using Microsoft.EntityFrameworkCore;
using XC.XSC.Repositories.TaskAuditHistory;
using XC.XSC.ViewModels;
using XC.XSC.ViewModels.Configuration;
using XC.XSC.ViewModels.TaskAuditHistory;

namespace XC.XSC.Service.TaskAuditHistory
{
    /// <summary>
    /// This is an implementation class of ITaskAuditHistoryService. In this class main buisness logics are written.
    /// </summary>
    public class TaskAuditHistoryService : ITaskAuditHistoryService
    {
        private readonly ITaskAuditHistoryRepository _taskAuditHistoryRepository;
        private readonly IResponse _operationResponse;

        /// <summary>
        ///This is an interface containing one method "TaskAuditHistoryService.
        /// </summary>
        ///
        public TaskAuditHistoryService(ITaskAuditHistoryRepository taskAuditHistoryRepository,IResponse response)
        {
            _taskAuditHistoryRepository = taskAuditHistoryRepository;
            this._operationResponse = response; 
        } 

        /// <summary>
        /// This method is implemented to retrieve TaskAuditHistoryDetails.
        /// </summary>
        /// <param name="submissionId"></param>
        /// <returns>It returns the object of TaskAuditHistory</returns>
        public async Task<IResponse> GetTaskAuditHistoryDetailAsync(long submissionId)
        {
            TaskAuditHistoryResponse ObjResponse = new TaskAuditHistoryResponse();
            List<TaskSatgeData> taskSatgeDataOutput = new List<TaskSatgeData>();
            var taskAuditHistoryQuery = await _taskAuditHistoryRepository.GetAll().Where(k => k.SubmissionId == submissionId).ToListAsync();
            foreach (var e in taskAuditHistoryQuery)
            {
                List<TaskSatgeData> taskStageData = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TaskSatgeData>>(e.StageData);
                if(taskStageData.Count>0)
                {
                    taskSatgeDataOutput.Add(taskStageData[0]);
                }
            }
            _operationResponse.Result = taskSatgeDataOutput;
            ObjResponse.StageData = taskSatgeDataOutput;
            _operationResponse.IsSuccess = true;
            _operationResponse.Message = "SUCCESS";
            return _operationResponse;
        }

        /// <summary>
        /// This method is implemented to retrieve TaskAuditHistory Duration.
        /// </summary>
        /// <param name="submissionId"></param>
        /// <returns>It returns the object of TaskAuditHistory Duration</returns>
        public async Task<IResponse> GetTaskAuditHistoryDurationAsync(long submissionId)
        {
            TaskAuditHistoryWithDurationResponse ObjResponse = new TaskAuditHistoryWithDurationResponse();
            var taskAuditHistoryQuery = await _taskAuditHistoryRepository.GetAll().Where(k => k.SubmissionId == submissionId).ToListAsync();
            var distinctStageQuery= taskAuditHistoryQuery.GroupBy(x => x.SubmissionStageId).Select(x => x.FirstOrDefault());
            var taskAuditHistoryListQuery = taskAuditHistoryQuery;
            TimeSpan processingtime = new TimeSpan();
            TimeSpan reviewTime =new TimeSpan();
            TimeSpan queryTime = new TimeSpan();
            TimeSpan total = new TimeSpan();
          
            foreach (var e in distinctStageQuery)
            {
                int submissionStage=e.SubmissionStage.Id;
                var responsex = taskAuditHistoryListQuery.Where(x => x.SubmissionStage.Id == submissionStage).ToList();
                if(responsex.Any())
                {
                    var filterdeSubmission = taskAuditHistoryQuery.Where(x => x.SubmissionStage.Id == submissionStage).ToList();
                    TimeSpan timer = new TimeSpan();
                    foreach (var filter in filterdeSubmission)
                    {
                        var prevDate = filter.StartTime;
                        var LastDate = filter.EndTime;
                        var DateDiff = filter.EndTime - filter.StartTime;
                        timer = timer + DateDiff;
                    }
                    if (e.SubmissionStatus.Name == "UnderReview" || e.SubmissionStatus.Name == "ReviewFail" || e.SubmissionStatus.Name == "ReviewPass" || e.SubmissionStatus.Name == "ReviewRejected")
                    {
                        reviewTime = timer;
                        total = total + timer;
                    }
                    if (e.SubmissionStatus.Name == "UnderQuery")
                    {
                        queryTime = timer;
                        total = total + timer;
                    }
                    if (e.SubmissionStatus.Name == "In Progress (Play)")
                    {
                        processingtime = timer;
                        total = total + timer;
                    }
                }
            }
            _operationResponse.Result = new TaskAuditHistoryWithDurationResponse()
            {
                ReviewDays = reviewTime.Days,
               
                ReviewHour = reviewTime.Hours,
                ReviewMins = reviewTime.Minutes,
                ReviewSecs= reviewTime.Seconds,

                ProcessingDays= processingtime.Days,
                ProcessingHour= processingtime.Hours,   
                ProcessingMins= processingtime.Minutes,
                ProcessingSecs= processingtime.Seconds,

                QueryDays=queryTime.Days,
                QueryHour=queryTime.Hours,  
                QueryMins=queryTime.Minutes,
                QuerySecs= queryTime.Seconds,

                TotalDay=total.Days,
                TotalHour=total.Hours,
                TotalMins=total.Minutes,
                TotalSecs=total.Seconds,

            };
            _operationResponse.IsSuccess = true;
            _operationResponse.Message = "SUCCESS";
            return _operationResponse;
            
        }
    }
}


