using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using XC.XSC.ViewModels.Enum;

namespace XC.XSC.Service.IAM
{
    public class EnumService : IEnumService
    {
        /// <summary>
        /// Generic Enum Service.
        /// </summary>
        public EnumService()
        {

        }

        /// <summary>
        /// Get the list of enum key and value pair.
        /// </summary>
        /// <typeparam name="T">Enum Name</typeparam>
        /// <returns>Return the list of the enum key and value pairs.</returns>
        public async Task<List<EnumResponse>?> EnumNamedValues(string enumName)
        {
            List<EnumResponse>? enumResponse = null;

            Array values = this.GetEnumArray(enumName);

            if (values != null)
            {
                enumResponse = new List<EnumResponse>();

                foreach (int item in values)
                {
                    enumResponse.Add(new EnumResponse { Id = item, Name = GetEnumName(enumName, item) });
                }
            }

            return enumResponse;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumName"></param>
        /// <returns></returns>
        private Array GetEnumArray(string enumName)
        {
            switch (enumName)
            {
                case "CommentType":
                    return Enum.GetValues(typeof(CommentType));                    

                case "LogType":
                    return Enum.GetValues(typeof(LogType));                    

                case "SchedulerFrequency":
                    return Enum.GetValues(typeof(SchedulerFrequency));                    

                case "SlaType":
                    return Enum.GetValues(typeof(SlaType));

                case "SubmissionStatusType":
                    return Enum.GetValues(typeof(SubmissionStatusType));

                case "TaskType":
                    return Enum.GetValues(typeof(TaskType));

                case "ReviewType":
                    return Enum.GetValues(typeof(ReviewType));

                case "GeneralInfoPasStatusType":
                    return Enum.GetValues(typeof(GeneralInfoPasStatusType));

                case "GeneralInfoReviewStatusType":
                    return Enum.GetValues(typeof(GeneralInfoReviewStatusType));

                default:
                    return null;

            }
        }

        private string GetEnumName(string enumName, int value)
        {
            string name = string.Empty;
            FieldInfo fieldInfo;
            switch (enumName)
            {
                case "CommentType":
                    name = Enum.GetName(typeof(CommentType), value);
                    fieldInfo = typeof(CommentType).GetField(name);
                    DisplayAttribute[] attributes = (DisplayAttribute[])fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), false);
                    if (attributes != null && attributes.Length > 0)
                    {
                        return attributes[0].Name;
                    }
                    else
                    {
                        return name;
                    }
                case "LogType":
                    name = Enum.GetName(typeof(LogType), value);
                    fieldInfo = typeof(LogType).GetField(name);
                    DisplayAttribute[] logAttributes = (DisplayAttribute[])fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), false);
                    if (logAttributes != null && logAttributes.Length > 0)
                    {
                        return logAttributes[0].Name;
                    }
                    else
                    {
                        return name;
                    }
                case "SchedulerFrequency":
                    name = Enum.GetName(typeof(SchedulerFrequency), value);
                    fieldInfo = typeof(SchedulerFrequency).GetField(name);
                    DisplayAttribute[] scheduler = (DisplayAttribute[])fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), false);
                    if (scheduler != null && scheduler.Length > 0)
                    {
                        return scheduler[0].Name;
                    }
                    else
                    {
                        return name;
                    }
                case "SlaType":
                    name = Enum.GetName(typeof(SlaType), value);
                    fieldInfo = typeof(SlaType).GetField(name);
                    DisplayAttribute[] sla = (DisplayAttribute[])fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), false);
                    if (sla != null && sla.Length > 0)
                    {
                        return sla[0].Name;
                    }
                    else
                    {
                        return name;
                    }
                case "SubmissionStatusType":
                    name = Enum.GetName(typeof(SubmissionStatusType), value);
                    fieldInfo = typeof(SubmissionStatusType).GetField(name);
                    DisplayAttribute[] submission = (DisplayAttribute[])fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), false);
                    if (submission != null && submission.Length > 0)
                    {
                        return submission[0].Name;
                    }
                    else
                    {
                        return name;
                    }
                case "TaskType":
                    name = Enum.GetName(typeof(TaskType), value);
                    fieldInfo = typeof(TaskType).GetField(name);
                    DisplayAttribute[] task = (DisplayAttribute[])fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), false);
                    if (task != null && task.Length > 0)
                    {
                        return task[0].Name;
                    }
                    else
                    {
                        return name;
                    }
                case "ReviewType":
                    name = Enum.GetName(typeof(ReviewType), value);
                    fieldInfo = typeof(ReviewType).GetField(name);
                    DisplayAttribute[] review = (DisplayAttribute[])fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), false);
                    if (review != null && review.Length > 0)
                    {
                        return review[0].Name;
                    }
                    else
                    {
                        return name;
                    }
                case "GeneralInfoPasStatusType":
                    name = Enum.GetName(typeof(GeneralInfoPasStatusType), value);
                    fieldInfo = typeof(GeneralInfoPasStatusType).GetField(name);
                    DisplayAttribute[] pasStatus = (DisplayAttribute[])fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), false);
                    if (pasStatus != null && pasStatus.Length > 0)
                    {
                        return pasStatus[0].Name;
                    }
                    else
                    {
                        return name;
                    }
                case "GeneralInfoReviewStatusType":
                    name = Enum.GetName(typeof(GeneralInfoReviewStatusType), value);
                    fieldInfo = typeof(GeneralInfoReviewStatusType).GetField(name);
                    DisplayAttribute[] reviewStatus = (DisplayAttribute[])fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), false);
                    if (reviewStatus != null && reviewStatus.Length > 0)
                    {
                        return reviewStatus[0].Name;
                    }
                    else
                    {
                        return name;
                    }

                default:
                    return string.Empty;

            }
        }
    }
}
