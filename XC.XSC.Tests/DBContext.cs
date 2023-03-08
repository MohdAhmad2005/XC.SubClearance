using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using XC.XSC.Data;
using XC.XSC.Models.Entity.EmailInfo;
using XC.XSC.Models.Entity.Lob;
using XC.XSC.Models.Entity.Master;
using XC.XSC.Models.Entity.Prefrence;
using XC.XSC.Models.Entity.SubmissionClearance;
using XC.XSC.Models.Entity.SubmissionStage;
using XC.XSC.Models.Entity.SubmissionStatus;
using XC.XSC.Models.Entity.TenantContextMapping;
using XC.XSC.Service.User;
using XC.XSC.ViewModels.Enum;

namespace XC.XSC.Tests
{
    public class XSCContext : IDisposable
    {
        public MSSqlContext dbContext { get; private set; }

        DbContextOptions<MSSqlContext> dbContextOptions = new DbContextOptionsBuilder<MSSqlContext>()
                    .UseInMemoryDatabase(databaseName: "InMemoryDb")
                    .Options;


        public XSCContext(IUserContext userContext)
        {
            dbContext = new MSSqlContext(dbContextOptions);
            dbContext.Database.EnsureCreated();

            var stage = new Models.Entity.SubmissionStage.SubmissionStage()
            {

                Name = "EmailReceived",
                Color = "Red",
                Label = "Email Received",
                OrderNo = 1,

                CreatedDate = System.DateTime.Now,
                ModifiedDate = System.DateTime.Now,
                CreatedBy = userContext.UserInfo.UserId,
                ModifiedBy = userContext.UserInfo.UserId,
                TenantId = userContext.UserInfo.TenantId,
                IsActive = true,
            };
            dbContext.Set<Models.Entity.SubmissionStage.SubmissionStage>().Add(stage);
            dbContext.SaveChanges();

            var submissionStatus = new Models.Entity.SubmissionStatus.SubmissionStatus()
            {

                Name = "NotAssignedYet",
                Color = "badge bg-info",
                Label = "Not Assigned Yet",
                OrderNo = 1,

                CreatedDate = System.DateTime.Now,
                ModifiedDate = System.DateTime.Now,
                CreatedBy = userContext.UserInfo.UserId,
                ModifiedBy = userContext.UserInfo.UserId,
                TenantId = userContext.UserInfo.TenantId,
                IsActive = true

            };
            dbContext.Set<Models.Entity.SubmissionStatus.SubmissionStatus>().Add(submissionStatus);
            dbContext.SaveChanges();

            var lob = new Models.Entity.Lob.Lob()
            {
                Name = "Lob-1",
                LOBID = "9F2A9503-6C72-4B56-A460-B8EEC75CDDF2",
                Description = "Lob-Desc",

                CreatedDate = System.DateTime.Now,
                ModifiedDate = System.DateTime.Now,
                CreatedBy = userContext.UserInfo.UserId,
                ModifiedBy = userContext.UserInfo.UserId,
                TenantId = userContext.UserInfo.TenantId,
                IsActive = true
            };

            dbContext.Set<Models.Entity.Lob.Lob>().Add(lob);
            dbContext.SaveChanges();

            var emailInfo = new Models.Entity.EmailInfo.EmailInfo()
            {
                Body = "Email Body",
                EmailId = "96c6012d-cbb1-4dd8-b310-dd89cc8168af",
                FromName = "XCDSC DEV",
                FromEmail = "XCDSCDEV@itssxc.com",
                ToEmail = "XCDSCDEV@itssxc.com",
                CCEmail = "Test@itssxc.com",
                Subject = "test Subject3",
                LobId = lob.LOBID,
                MessageId = "PN2PR01MB8685C970571955B1B44E8B46AB009",
                ParentMessageId = "214d68e1-73d8-42e5",
                TotalDocuments = 1,
                ReceivedDate = DateTime.Now,
                DocumentId = "a4e24d9e-89e3-4e66-a723-b92b73e4b394",
                IsDuplicate = false,
                MailboxName = "XCDCCMPDEV@xceedance.com",
                ConfigurationId = "43903a33-8ce5-49b9-84e7-472516c67e95",

                CreatedDate = System.DateTime.Now,
                ModifiedDate = System.DateTime.Now,
                CreatedBy = userContext.UserInfo.UserId,
                ModifiedBy = userContext.UserInfo.UserId,
                TenantId = userContext.UserInfo.TenantId,
                IsActive = true
            };

            dbContext.Set<Models.Entity.EmailInfo.EmailInfo>().Add(emailInfo);
            dbContext.SaveChanges();


            var attachment = new Models.Entity.EMailInfoAttachment.EmailInfoAttachment()
            {
                EmailInfoId = emailInfo.Id,
                FileName = "name.txt",
                FileType = ".txt",
                DocumentId = "a2f6e314-5c7b-4cd6-851f-40d739dbcda9",
                AttachmentId = "test",
                FileSize = 10,
                SizeUnit = "1",

                CreatedDate = System.DateTime.Now,
                ModifiedDate = System.DateTime.Now,
                CreatedBy = userContext.UserInfo.UserId,
                ModifiedBy = userContext.UserInfo.UserId,
                TenantId = userContext.UserInfo.TenantId,
                IsActive = true

            };

            dbContext.Set<Models.Entity.EMailInfoAttachment.EmailInfoAttachment>().Add(attachment);
            dbContext.SaveChanges();

            var submission = new List<Models.Entity.Submission.Submission>()
            {

              new Models.Entity.Submission.Submission
              {
                    AssignedId = "2345678",
                    InsuredName = "No Insurer",
                    EmailInfoId = emailInfo.Id,
                    BrokerName = "No Broker",
                    CaseId = "3456h7j",
                    CompletionDate = DateTime.Now.AddDays(-5),
                    DueDate = System.DateTime.Now,
                    SubmissionStatusId = submissionStatus.Id,
                    SubmissionStageId = stage.Id,
                    IsInScope = true,
                    LobId = lob.Id,
                    ExtendedDate = System.DateTime.Now,
                    EmailBody = "no mail body",
                    TaskId = "121",
                    RegionId = 1,

                    CreatedDate = System.DateTime.Now,
                    ModifiedDate = System.DateTime.Now,
                    CreatedBy = userContext.UserInfo.UserId,
                    ModifiedBy = userContext.UserInfo.UserId,
                    TenantId = userContext.UserInfo.TenantId,
                    IsActive = true,
              },
                 new Models.Entity.Submission.Submission
                 {
                    AssignedId = null,
                    InsuredName = "No Insurer",
                    EmailInfoId = emailInfo.Id,
                    BrokerName = "No Broker",
                    CaseId = "3456h7j",
                    CompletionDate = DateTime.Today.AddDays(-10),
                    DueDate = System.DateTime.Now,
                    SubmissionStatusId = submissionStatus.Id,
                    SubmissionStageId = stage.Id,
                    IsInScope = true,
                    LobId = lob.Id,
                    ExtendedDate = System.DateTime.Now,
                    EmailBody = "no mail body",
                    TaskId = "121",
                    RegionId=1,

                    CreatedDate = System.DateTime.Now,
                    ModifiedDate = System.DateTime.Now,
                    CreatedBy = userContext.UserInfo.UserId,
                    ModifiedBy = userContext.UserInfo.UserId,
                    TenantId = userContext.UserInfo.TenantId,
                    IsActive = true,
                 },
                 new Models.Entity.Submission.Submission
                 {
                    AssignedId= userContext.UserInfo.UserId,
                    InsuredName = "No Insurer",
                    EmailInfoId = emailInfo.Id,
                    BrokerName = "No Broker",
                    CaseId = "3456h7j",
                    CompletionDate = DateTime.Now.AddDays(-5),
                    DueDate = System.DateTime.Now,
                    SubmissionStatusId = submissionStatus.Id,
                    SubmissionStageId = stage.Id,
                    IsInScope = true,
                    LobId = 1,
                    ExtendedDate = System.DateTime.Now,
                    EmailBody = "no mail body",
                    TaskId = "121",
                    RegionId = 1,
                    CreatedDate = System.DateTime.Now,
                    ModifiedDate = System.DateTime.Now,
                    CreatedBy = userContext.UserInfo.UserId,
                    ModifiedBy = userContext.UserInfo.UserId,
                    TenantId = userContext.UserInfo.TenantId,
                    IsActive = true,
                 },
            };

            dbContext.Set<Models.Entity.Submission.Submission>().AddRange(submission);
            dbContext.SaveChanges();

            var SubmissionAllocationlist = new List<Models.Entity.SubmissionsAllocated.SubmissionAllocated>()
            {
                new Models.Entity.SubmissionsAllocated.SubmissionAllocated
                {
                    AllocatedBy="Admin",
                    AllocatedDate=DateTime.Now,
                    AllocatedTo="Test@Xceedance.com",
                    SubmissionId=1,
                    CreatedDate=System.DateTime.Now,
                    ModifiedDate=System.DateTime.Now,
                    CreatedBy= userContext.UserInfo.UserId,
                    ModifiedBy=userContext.UserInfo.UserId,
                    TenantId=userContext.UserInfo.TenantId,
                    IsActive=true
                }
            };

            dbContext.Set<Models.Entity.SubmissionsAllocated.SubmissionAllocated>().AddRange(SubmissionAllocationlist);
            dbContext.SaveChanges();

            var preference = new List<Preference>()
            {
               new Preference()
                {
                    CreatedBy = userContext.UserInfo.UserId,
                    CreatedDate = System.DateTime.Now,
                    Description = "",
                    IsActive = true,
                    Key = "SOURCEENTITYID_SCOPE",
                    ModifiedBy = "System",
                    ModifiedDate = System.DateTime.Now,
                    TenantId = userContext.UserInfo.TenantId,
                    UserId = userContext.UserInfo.UserId,
                    Value = "94"
                },
            new Preference()
            {
                CreatedBy = userContext.UserInfo.UserId,
                CreatedDate = System.DateTime.Now,
                Description = "",
                IsActive = true,
                Key = "RULECONTEXTID_SCOPE",
                ModifiedBy = "System",
                ModifiedDate = System.DateTime.Now,
                TenantId = userContext.UserInfo.TenantId,
                UserId = userContext.UserInfo.UserId,
                Value = "16"
            },
            new Preference()
            {
                CreatedBy = userContext.UserInfo.UserId,
                CreatedDate = System.DateTime.Now,
                Description = "MailDuplicityField check if mail is duplicate or not",
                IsActive = true,
                Key = "MLDPLCTYFLD",
                ModifiedBy = "System",
                ModifiedDate = System.DateTime.Now,
                TenantId = userContext.UserInfo.TenantId,
                UserId = userContext.UserInfo.UserId,
                Value = "Insured Name,Effective Date"
            },
             new Preference()
            {
                CreatedBy = userContext.UserInfo.UserId,
                CreatedDate = System.DateTime.Now,
                Description = "Business Days text used in tat matrics",
                IsActive = true,
                Key = "BSNDYS",
                ModifiedBy = "System",
                ModifiedDate = System.DateTime.Now,
                TenantId = userContext.UserInfo.TenantId,
                UserId = userContext.UserInfo.UserId,
                Value = "Business Days"
            },
              new Preference()
            {
                CreatedBy = userContext.UserInfo.UserId,
                CreatedDate = System.DateTime.Now,
                Description = "Week of days.",
                IsActive = true,
                Key = "WKFDYS",
                ModifiedBy = "System",
                ModifiedDate = System.DateTime.Now,
                TenantId = userContext.UserInfo.TenantId,
                UserId = userContext.UserInfo.UserId,
                Value = "SATURDAY,SUNDAY"
            },
                new Preference()
            {
                CreatedBy = userContext.UserInfo.UserId,
                CreatedDate = System.DateTime.Now,
                Description = "Last submission status submitted to pass.",
                IsActive = true,
                Key = "SBMTDTPS",
                ModifiedBy = "System",
                ModifiedDate = System.DateTime.Now,
                TenantId = userContext.UserInfo.TenantId,
                UserId = userContext.UserInfo.UserId,
                Value = "1"
            }
        };

            dbContext.Set<Preference>().AddRange(preference);
            dbContext.SaveChanges();


            var auditHistory = new List<Models.Entity.TaskAuditHistory.TaskAuditHistory>()
            {
                new Models.Entity.TaskAuditHistory.TaskAuditHistory()
                {
                SubmissionId = submission[0].Id,
                SubmissionStatusId = 1,
                SubmissionStageId = stage.Id,
                IsAcrchieved = false,
                StageData = "[{\"action\":\"Email Received\",\"date\":\"23-11-22 02:33:45\",\"by\":\"Ashutosh Kumar\"}]",
                StartTime = System.DateTime.Now,
                EndTime = System.DateTime.Now.AddDays(1),
                UserId = userContext.UserInfo.UserId,
                CreatedDate = System.DateTime.Now,
                ModifiedDate = System.DateTime.Now,
                CreatedBy = userContext.UserInfo.UserId,
                ModifiedBy = userContext.UserInfo.UserId,
                TenantId = userContext.UserInfo.TenantId,
                IsActive = true,
                },
                new Models.Entity.TaskAuditHistory.TaskAuditHistory()
                {
                     SubmissionId = submission[0].Id,
                SubmissionStatusId = 2,
                SubmissionStageId = stage.Id,
                IsAcrchieved = false,
                StageData = "[{\"action\":\"AllocatedTo Ravi\",\"date\":\"23-11-2022 02:36:45\",\"by\":\"Ramchandra\"}]",
                StartTime = System.DateTime.Now,
                EndTime = System.DateTime.Now.AddDays(1),
                UserId = userContext.UserInfo.UserId,
                CreatedDate = System.DateTime.Now,
                ModifiedDate = System.DateTime.Now,
                CreatedBy = userContext.UserInfo.UserId,
                ModifiedBy = userContext.UserInfo.UserId,
                TenantId = userContext.UserInfo.TenantId,
                IsActive = true,
                },
                new Models.Entity.TaskAuditHistory.TaskAuditHistory()
                {
                SubmissionId = submission[0].Id,
                SubmissionStatusId = 3,
                SubmissionStageId = stage.Id,
                IsAcrchieved = false,
                StageData = "[{\"action\":\"Email Received\",\"date\":\"23-11-22 02:33:45\",\"by\":\"Ashutosh Kumar\"}]",
                StartTime = System.DateTime.Now,
                EndTime = System.DateTime.Now.AddDays(1),
                UserId = userContext.UserInfo.UserId,
                CreatedDate = System.DateTime.Now,
                ModifiedDate = System.DateTime.Now,
                CreatedBy = userContext.UserInfo.UserId,
                ModifiedBy = userContext.UserInfo.UserId,
                TenantId = userContext.UserInfo.TenantId,
                IsActive = true,
                },
                new Models.Entity.TaskAuditHistory.TaskAuditHistory()
                {
                SubmissionId = submission[0].Id,
                SubmissionStatusId = 4,
                SubmissionStageId = stage.Id,
                IsAcrchieved = false,
                StageData = "[{\"action\":\"Under Review\",\"date\":\"23-11-2022 02:36:45\",\"bY\":\"Ashutosh\"}]",
                StartTime = System.DateTime.Now,
                EndTime = System.DateTime.Now.AddDays(1),
                UserId = userContext.UserInfo.UserId,
                CreatedDate = System.DateTime.Now,
                ModifiedDate = System.DateTime.Now,
                CreatedBy = userContext.UserInfo.UserId,
                ModifiedBy = userContext.UserInfo.UserId,
                TenantId = userContext.UserInfo.TenantId,
                IsActive = true,
                },
                      new Models.Entity.TaskAuditHistory.TaskAuditHistory()
                {
                SubmissionId = submission[0].Id,
                SubmissionStatusId = 5,
                SubmissionStageId = stage.Id,
                IsAcrchieved = false,
                StageData = "[{\"action\":\"Under Rejected\",\"date\":\"23-11-2022 02:36:45\",\"by\":\"Ashutosh\"}]",
                StartTime = System.DateTime.Now,
                EndTime = System.DateTime.Now.AddDays(1),
                UserId = userContext.UserInfo.UserId,
                CreatedDate = System.DateTime.Now,
                ModifiedDate = System.DateTime.Now,
                CreatedBy = userContext.UserInfo.UserId,
                ModifiedBy = userContext.UserInfo.UserId,
                TenantId = userContext.UserInfo.TenantId,
                IsActive = true,
                },
                new Models.Entity.TaskAuditHistory.TaskAuditHistory()
                {
                 SubmissionId = submission[0].Id,
                SubmissionStatusId = 6,
                SubmissionStageId = stage.Id,
                IsAcrchieved = false,
                StageData = "[{\"action\":\"Email Received\",\"date\":\"23-11-22 02:33:45\",\"by\":\"Ashutosh Kumar\"}]",
                StartTime = System.DateTime.Now,
                EndTime = System.DateTime.Now.AddDays(1),
                UserId = userContext.UserInfo.UserId,
                CreatedDate = System.DateTime.Now,
                ModifiedDate = System.DateTime.Now,
                CreatedBy = userContext.UserInfo.UserId,
                ModifiedBy = userContext.UserInfo.UserId,
                TenantId = userContext.UserInfo.TenantId,
                IsActive = true,
                },
                new Models.Entity.TaskAuditHistory.TaskAuditHistory()
                {
                SubmissionId = submission[0].Id,
                SubmissionStatusId = 7,
                SubmissionStageId = stage.Id,
                IsAcrchieved = false,
                StageData = "[{\"action\":\"Under Pass\",\"date\":\"23-11-2022 02:36:45\",\"by\":\"Ashutosh\"}]",
                StartTime = System.DateTime.Now,
                EndTime = System.DateTime.Now.AddDays(1),
                UserId = userContext.UserInfo.UserId,
                CreatedDate = System.DateTime.Now,
                ModifiedDate = System.DateTime.Now,
                CreatedBy = userContext.UserInfo.UserId,
                ModifiedBy = userContext.UserInfo.UserId,
                TenantId = userContext.UserInfo.TenantId,
                IsActive = true,
                },

                new Models.Entity.TaskAuditHistory.TaskAuditHistory()
                {
                     SubmissionId = submission[0].Id,
                SubmissionStatusId = 9,
                SubmissionStageId = stage.Id,
                IsAcrchieved = false,
                StageData = "[{\"action\":\"Email Received\",\"date\":\"23-11-22 02:33:45\",\"by\":\"Ashutosh Kumar\"}]",
                StartTime = System.DateTime.Now,
                EndTime = System.DateTime.Now.AddDays(1),
                UserId = userContext.UserInfo.UserId,
                CreatedDate = System.DateTime.Now,
                ModifiedDate = System.DateTime.Now,
                CreatedBy = userContext.UserInfo.UserId,
                ModifiedBy = userContext.UserInfo.UserId,
                TenantId = userContext.UserInfo.TenantId,
                IsActive = true,
                },

                new Models.Entity.TaskAuditHistory.TaskAuditHistory()
                {
                    SubmissionId = submission[0].Id,
                    SubmissionStatusId = 10,
                    SubmissionStageId = stage.Id,
                    IsAcrchieved = false,
                    StageData = "[{\"action\":\"Email Received\",\"date\":\"23-11-22 02:33:45\",\"by\":\"Ashutosh Kumar\"}]",
                    StartTime = System.DateTime.Now,
                    EndTime = System.DateTime.Now.AddDays(1),
                    UserId = userContext.UserInfo.UserId,
                    CreatedDate = System.DateTime.Now,
                    ModifiedDate = System.DateTime.Now,
                    CreatedBy = userContext.UserInfo.UserId,
                    ModifiedBy = userContext.UserInfo.UserId,
                    TenantId = userContext.UserInfo.TenantId,
                    IsActive = true,
                 },

                 new Models.Entity.TaskAuditHistory.TaskAuditHistory()
                    {
                     SubmissionId = submission[0].Id,
                    SubmissionStatusId = 12,
                    SubmissionStageId = stage.Id,
                    IsAcrchieved = false,
                    StageData = "[{\"action\":\"In Progress (Pause)\",\"date\":\"23-11-2022 02:33:45\",\"by\":\"Ashutosh\"}]",
                    StartTime = System.DateTime.Now,
                    EndTime = System.DateTime.Now.AddDays(1),
                    UserId = userContext.UserInfo.UserId,
                    CreatedDate = System.DateTime.Now,
                    ModifiedDate = System.DateTime.Now,
                    CreatedBy = userContext.UserInfo.UserId,
                    ModifiedBy = userContext.UserInfo.UserId,
                    TenantId = userContext.UserInfo.TenantId,
                    IsActive = true,
                    },
                 new Models.Entity.TaskAuditHistory.TaskAuditHistory()
                {
                 SubmissionId = submission[0].Id,
                SubmissionStatusId = 12,
                SubmissionStageId = stage.Id,
                IsAcrchieved = false,
                StageData = "[{\"action\":\"In Progress (Pause)\",\"date\":\"23-11-2022 02:33:45\",\"by\":\"Ashutosh\"}]",
                StartTime = System.DateTime.Now,
                EndTime = System.DateTime.Now.AddDays(1),
                UserId = userContext.UserInfo.UserId,
                CreatedDate = System.DateTime.Now,
                ModifiedDate = System.DateTime.Now,
                CreatedBy = userContext.UserInfo.UserId,
                ModifiedBy = userContext.UserInfo.UserId,
                TenantId = userContext.UserInfo.TenantId,
                IsActive = true,
                }

            };
            dbContext.Set<Models.Entity.TaskAuditHistory.TaskAuditHistory>().AddRange(auditHistory);
            dbContext.SaveChanges();

            TenantContextMapping tenantContextMapping = new TenantContextMapping();
            tenantContextMapping.Region = "0";
            tenantContextMapping.Lob = "1";
            tenantContextMapping.ContextId = 17;
            tenantContextMapping.EntityId = 95;
            tenantContextMapping.TenantId = userContext.UserInfo.TenantId;
            tenantContextMapping.CreatedDate = DateTime.Now;
            tenantContextMapping.CreatedBy = "System";
            tenantContextMapping.ModifiedDate = DateTime.Now;
            tenantContextMapping.ModifiedBy = "System";
            tenantContextMapping.IsActive = true;
            dbContext.Set<TenantContextMapping>().Add(tenantContextMapping);
            dbContext.SaveChanges();

            SubmissionClearance submissionClearance = new SubmissionClearance();
            submissionClearance.SubmissionId = submission[0].Id;
            submissionClearance.RuleName = "Registered Broker Firm Check";
            submissionClearance.RuleStatus = true;
            submissionClearance.Description = "Registered Broker Firm Check msg";
            submissionClearance.Remark = "Registered Broker Firm Check msg";
            submissionClearance.ContextId = 17;
            submissionClearance.EntityId = 95;
            submissionClearance.TenantId = userContext.UserInfo.TenantId;
            submissionClearance.CreatedDate = DateTime.Now;
            submissionClearance.CreatedBy = "System";
            submissionClearance.ModifiedDate = DateTime.Now;
            submissionClearance.ModifiedBy = "System";
            submissionClearance.IsActive = true;
            dbContext.Set<SubmissionClearance>().Add(submissionClearance);
            dbContext.SaveChanges();
            dbContext.Set<Models.Entity.SubmissionAuditLog.SubmissionAuditLog>();

            var notification = new Models.Entity.Notification.Notification()
            {

                UserId = "23q2w3e4r5t6y7sdf6tg",
                SubmissionId = 1,
                MsgType = MessageType.Email,
                IsRead = false,
                Subject = "test",
                Description = "test",
                TemplateKey = "testKey",

                CreatedDate = System.DateTime.Now,
                ModifiedDate = System.DateTime.Now,
                CreatedBy = userContext.UserInfo.UserId,
                ModifiedBy = userContext.UserInfo.UserId,
                TenantId = userContext.UserInfo.TenantId,
                IsActive = true,
            };
            dbContext.Set<Models.Entity.Notification.Notification>().Add(notification);
            dbContext.SaveChanges();

             var submissionAuditLog = new List<Models.Entity.SubmissionAuditLog.SubmissionAuditLog>()
             {
                  new Models.Entity.SubmissionAuditLog.SubmissionAuditLog()
                  {
                     SubmissionId = 4,
                     PrevStatus = 1,
                     NewStatus = 2,
                     PrevAssignedToId = userContext.UserInfo.UserId,
                     NewAssignedToId = userContext.UserInfo.UserId,
                     TenantId = "xceedance",
                     CreatedDate = DateTime.Today.AddDays(-7),
                     CreatedBy = userContext.UserInfo.UserId,
                     ModifiedDate = DateTime.Now,
                     ModifiedBy= userContext.UserInfo.UserId,
                     IsActive = true,
                  },

                 new Models.Entity.SubmissionAuditLog.SubmissionAuditLog()
                 {
                    SubmissionId = 1,
                    PrevStatus = 1,
                    NewStatus = 2,
                    PrevAssignedToId = userContext.UserInfo.UserId,
                    NewAssignedToId = userContext.UserInfo.UserId,
                    TenantId = "xceedance",
                    CreatedDate = DateTime.Today.AddDays(-4),
                    CreatedBy = userContext.UserInfo.UserId,
                    ModifiedDate = DateTime.Now,
                    ModifiedBy = userContext.UserInfo.UserId,
                    IsActive = true,
                 }
             };
              dbContext.Set<Models.Entity.SubmissionAuditLog.SubmissionAuditLog>().AddRange(submissionAuditLog);
              dbContext.SaveChanges();
        }
        public void Dispose()
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }



    }
}
