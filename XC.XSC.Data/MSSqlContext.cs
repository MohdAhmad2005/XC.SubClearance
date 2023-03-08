using Microsoft.EntityFrameworkCore;
using XC.XSC.Models.Entity.Comment;
using XC.XSC.Models.Entity.EmailInfo;
using XC.XSC.Models.Entity.EMailInfoAttachment;
using XC.XSC.Models.Entity.Lob;
using XC.XSC.Models.Entity.Master;
using XC.XSC.Models.Entity.Prefrence;
using XC.XSC.Models.Entity.Scheduler;
using XC.XSC.Models.Entity.Sla;
using XC.XSC.Models.Entity.Submission;
using XC.XSC.Models.Entity.SubmissionAuditLog;
using XC.XSC.Models.Entity.SubmissionClearance;
using XC.XSC.Models.Entity.SubmissionsAllocated;
using XC.XSC.Models.Entity.SubmissionStage;
using XC.XSC.Models.Entity.SubmissionStatus;
using XC.XSC.Models.Entity.SubmissionStatusStageMapping;
using XC.XSC.Models.Entity.TaskAuditHistory;
using XC.XSC.Models.Entity.TenantContextMapping;
using XC.XSC.Models.Entity.ReviewConfiguration;
using XC.XSC.Models.Entity.Notification;

namespace XC.XSC.Data
{
    public class MSSqlContext : DbContext
    {
        private readonly string _schema = "xsc";

        public MSSqlContext(DbContextOptions<MSSqlContext> options) : base(options)
        {
        }
        public DbSet<Case> Cases { get; set; }
        public virtual DbSet<SubmissionStatus> SubmissionStatus { get; set; }
        public virtual DbSet<SubmissionStage> SubmissionStage { get; set; }
        public virtual DbSet<SubmissionStatusStageMapping> SubmissionStatusStageMapping { get; set; }
        public virtual DbSet<Submission> Submissions { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Preference> Preferences { get; set; }
        public virtual DbSet<TaskAuditHistory> TaskAuditHistory { get; set; }
        public virtual DbSet<EmailInfo> EmailInfos { get; set; }
        public virtual DbSet< EmailInfoAttachment> EmailInfoAttachments { get; set; }
        public virtual DbSet<SubmissionAllocated> SubmissionsAllocated { get; set; }
        public virtual DbSet<Lob> Lob { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
       
       
        public virtual DbSet<TenantContextMapping> TenantContextMappings { get; set; }
        public virtual DbSet<SubmissionAuditLog> SubmissionAuditLog { get; set; }

        public virtual DbSet<SubmissionClearance> SubmissionClearances { get; set; }
        
        public virtual DbSet<SchedulerConfiguration> SchedulerConfiguration { get; set; }
        public virtual DbSet<SlaConfiguration> SlaConfiguration { get; set; }

        public virtual DbSet<ReviewConfiguration> ReviewConfiguration { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(_schema);
            //base.OnModelCreating(modelBuilder);
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            //Make SubmissionId, ContextId, RuleName is unique composite key
            modelBuilder.Entity<SubmissionClearance>(entity =>
            {
                entity.HasIndex(e => new { e.SubmissionId, e.ContextId, e.RuleName }, "SubmissionId_ContextId_RuleName")
                    .IsUnique()
                    .HasFilter("([SubmissionId] IS NOT NULL AND [ContextId] IS NOT NULL AND [RuleName] IS NOT NULL)");
            });
        }
    }
}
