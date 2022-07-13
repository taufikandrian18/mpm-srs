using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MPMSRS.Models.Entities
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options)
            : base(options)
        {
        }
        public DbSet<Users> Users { get; set; }
        public DbSet<Networks> Networks { get; set; }
        public DbSet<Positions> Positions { get; set; }
        public DbSet<Divisions> Divisions { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Attachments> Attachments { get; set; }
        public DbSet<Assignments> Assignments { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<ProblemCategories> ProblemCategories { get; set; }
        public DbSet<VisitingTypes> VisitingTypes { get; set; }
        public DbSet<Visitings> Visitings { get; set; }
        public DbSet<VisitingPeoples> VisitingPeoples { get; set; }
        public DbSet<VisitingNoteMappings> VisitingNoteMappings { get; set; }
        public DbSet<VisitingDetailReports> VisitingDetailReports { get; set; }
        public DbSet<VisitingDetailReportProblemCategories> VisitingDetailReportProblemCategories { get; set; }
        public DbSet<VisitingDetailReportPICs> VisitingDetailReportPICs { get; set; }
        public DbSet<VisitingDetailReportAttachments> VisitingDetailReportAttachments { get; set; }
        public DbSet<CorrectiveActions> CorrectiveActions { get; set; }
        public DbSet<CorrectiveActionAttachments> CorrectiveActionAttachments { get; set; }
        public DbSet<CorrectiveActionProblemCategories> CorrectiveActionProblemCategories { get; set; }
        public DbSet<CorrectiveActionPICs> CorrectiveActionPICs { get; set; }
        public DbSet<UserNetworkMappings> UserNetworkMappings { get; set; }
        public DbSet<Authentications> Authentications { get; set; }
        public DbSet<UserProblemCategoryMappings> UserProblemCategoryMappings { get; set; }
        public DbSet<UserFcmTokens> UserFcmTokens { get; set; }
        public DbSet<PushNotifications> PushNotifications { get; set; }
        public DbSet<EventMasterDatas> EventMasterDatas { get; set; }
        public DbSet<Events> Events { get; set; }
        public DbSet<EventPeoples> EventPeoples { get; set; }
        public DbSet<EventDetailReports> EventDetailReports { get; set; }
        public DbSet<EventDetailReportProblemCategories> EventDetailReportProblemCategories { get; set; }
        public DbSet<EventDetailReportPICs> EventDetailReportPICs { get; set; }
        public DbSet<EventDetailReportAttachments> EventDetailReportAttachments { get; set; }
        public DbSet<EventCAs> EventCAs { get; set; }
        public DbSet<EventCAAttachments> EventCAAttachments { get; set; }
        public DbSet<EventCAProblemCategories> EventCAProblemCategories { get; set; }
        public DbSet<EventCAPICs> EventCAPICs { get; set; }
        public DbSet<Checklists> Checklists { get; set; }
        public DbSet<ChecklistPICs> ChecklistPICs { get; set; }
        public DbSet<ChecklistEvidences> ChecklistEvidences { get; set; }
        public DbSet<Ranks> Ranks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.CompanyId).IsUnicode(false);
                entity.Property(e => e.Username).IsUnicode(false);
                entity.Property(e => e.Password).IsUnicode(false);
                entity.Property(e => e.WorkLocation).IsUnicode(false);
                entity.Property(e => e.DisplayName).IsUnicode(false);
                entity.Property(e => e.Department).IsUnicode(false);
                entity.Property(e => e.Phone).IsUnicode(false);
                entity.Property(e => e.Email).IsUnicode(false);
                entity.Property(e => e.InternalTitle).IsUnicode(false);

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy)
                            .IsUnicode(false)
                            .HasDefaultValueSql("('SYSTEM')");

                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedBy)
                            .IsUnicode(false)
                            .HasDefaultValueSql("('SYSTEM')");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Attachment)
                            .WithMany(p => p.Users)
                            .HasForeignKey(d => d.AttachmentId)
                            .HasConstraintName("FK_Users_Attachments");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_Roles");

                entity.HasOne(d => d.Division)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.DivisionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_Divisions");
            });

            modelBuilder.Entity<Attachments>(entity =>
            {
                entity.Property(e => e.AttachmentId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AttachmentMime).IsUnicode(false);

                entity.Property(e => e.AttachmentUrl).IsUnicode(false);
            });

            modelBuilder.Entity<Assignments>(entity =>
            {

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('SYSTEM')");

                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedBy)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('SYSTEM')");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Assignments)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Assignments_Users");

                entity.HasOne(d => d.Network)
                    .WithMany(p => p.Assignments)
                    .HasForeignKey(d => d.NetworkId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Assignments_Networks");

                entity.HasOne(d => d.Division)
                    .WithMany(p => p.Assignments)
                    .HasForeignKey(d => d.DivisionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Assignments_Divisions");

            });

            modelBuilder.Entity<Networks>(entity =>
            {
                entity.Property(e => e.AccountNum).IsUnicode(false);
                entity.Property(e => e.AhmCode).IsUnicode(false);
                entity.Property(e => e.MDCode).IsUnicode(false);
                entity.Property(e => e.DealerName).IsUnicode(false);
                entity.Property(e => e.Address).IsUnicode(false);
                entity.Property(e => e.City).IsUnicode(false);
                entity.Property(e => e.ChannelDealer).IsUnicode(false);
                entity.Property(e => e.DLREmail).IsUnicode(false);
                entity.Property(e => e.KodeKareswil).IsUnicode(false);
                entity.Property(e => e.Karesidenan).IsUnicode(false);
                entity.Property(e => e.NPKSupervisor).IsUnicode(false);
                entity.Property(e => e.Email).IsUnicode(false);
                entity.Property(e => e.IdKaresidenanHC3).IsUnicode(false);
                entity.Property(e => e.NamaKaresHC3).IsUnicode(false);
                entity.Property(e => e.NamaSPVHC3).IsUnicode(false);
                entity.Property(e => e.NamaDeptHeadHC3).IsUnicode(false);
                entity.Property(e => e.NamaDivHeadHC3).IsUnicode(false);
                entity.Property(e => e.IdKaresidenanTSD).IsUnicode(false);
                entity.Property(e => e.NamaKaresTSD).IsUnicode(false);
                entity.Property(e => e.NPKSpvTSD).IsUnicode(false);
                entity.Property(e => e.NamaSPVTSD).IsUnicode(false);
                entity.Property(e => e.NPKDeptHeadTSD).IsUnicode(false);
                entity.Property(e => e.NamaDeptHeadTSD).IsUnicode(false);
                entity.Property(e => e.NPKDivHeadTSD).IsUnicode(false);
                entity.Property(e => e.NamaDivHeadTSD).IsUnicode(false);
                entity.Property(e => e.NetworkLatitude).IsUnicode(false);
                entity.Property(e => e.NetworkLongitude).IsUnicode(false);

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy)
                            .IsUnicode(false)
                            .HasDefaultValueSql("('SYSTEM')");

                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedBy)
                            .IsUnicode(false)
                            .HasDefaultValueSql("('SYSTEM')");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<Divisions>(entity =>
            {
                entity.Property(e => e.DivisionName).IsUnicode(false);

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy)
                            .IsUnicode(false)
                            .HasDefaultValueSql("('SYSTEM')");

                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedBy)
                            .IsUnicode(false)
                            .HasDefaultValueSql("('SYSTEM')");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<Positions>(entity =>
            {
                entity.Property(e => e.Channel).IsUnicode(false);
                entity.Property(e => e.NamaJabatan).IsUnicode(false);
                entity.Property(e => e.GroupJabatanId).IsUnicode(false);
                entity.Property(e => e.NamaGroupPosition).IsUnicode(false);

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy)
                            .IsUnicode(false)
                            .HasDefaultValueSql("('SYSTEM')");

                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedBy)
                            .IsUnicode(false)
                            .HasDefaultValueSql("('SYSTEM')");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Token).IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.RefreshToken)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RefreshTokens_Users");

            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.Property(e => e.RoleName).IsUnicode(false);

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy)
                            .IsUnicode(false)
                            .HasDefaultValueSql("('SYSTEM')");

                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedBy)
                            .IsUnicode(false)
                            .HasDefaultValueSql("('SYSTEM')");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<ProblemCategories>(entity =>
            {
                entity.Property(e => e.ProblemCategoryName).IsUnicode(false);

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy)
                            .IsUnicode(false)
                            .HasDefaultValueSql("('SYSTEM')");

                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedBy)
                            .IsUnicode(false)
                            .HasDefaultValueSql("('SYSTEM')");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<VisitingTypes>(entity =>
            {
                entity.Property(e => e.VisitingTypeName).IsUnicode(false);

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy)
                            .IsUnicode(false)
                            .HasDefaultValueSql("('SYSTEM')");

                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedBy)
                            .IsUnicode(false)
                            .HasDefaultValueSql("('SYSTEM')");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<Visitings>(entity =>
            {
                entity.Property(e => e.VisitingStatus).IsUnicode(false);

                entity.Property(e => e.VisitingComment).IsUnicode(false);

                entity.Property(e => e.VisitingCommentByManager).IsUnicode(false);

                entity.Property(e => e.ApprovedByManager).IsUnicode(false);

                entity.Property(e => e.ApprovedByGM).IsUnicode(false);

                entity.Property(e => e.StartDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EndDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('SYSTEM')");

                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedBy)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('SYSTEM')");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Network)
                    .WithMany(p => p.Visitings)
                    .HasForeignKey(d => d.NetworkId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Visitings_Networks");

                entity.HasOne(d => d.VisitingType)
                    .WithMany(p => p.Visitings)
                    .HasForeignKey(d => d.VisitingTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Visitings_VisitingTypes");

            });

            modelBuilder.Entity<VisitingPeoples>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('SYSTEM')");

                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedBy)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('SYSTEM')");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Visiting)
                    .WithMany(p => p.VisitingPeoples)
                    .HasForeignKey(d => d.VisitingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VisitingPeoples_Visitings");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.VisitingPeoples)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VisitingPeoples_Users");

            });

            modelBuilder.Entity<VisitingNoteMappings>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('SYSTEM')");

                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedBy)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('SYSTEM')");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Visiting)
                    .WithMany(p => p.VisitingNoteMappings)
                    .HasForeignKey(d => d.VisitingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VisitingNoteMappings_Visitings");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.VisitingNoteMappings)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VisitingNoteMappings_Users");
            });

            modelBuilder.Entity<VisitingDetailReports>(entity =>
            {
                entity.Property(e => e.VisitingDetailReportProblemIdentification).IsUnicode(false);

                entity.Property(e => e.CorrectiveActionProblemIdentification).IsUnicode(false);

                entity.Property(e => e.VisitingDetailReportStatus).IsUnicode(false);

                entity.Property(e => e.VisitingDetailReportFlagging).IsUnicode(false);

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('SYSTEM')");

                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedBy)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('SYSTEM')");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Visiting)
                    .WithMany(p => p.VisitingDetailReports)
                    .HasForeignKey(d => d.VisitingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VisitingDetailReports_Visitings");

                entity.HasOne(d => d.Network)
                    .WithMany(p => p.VisitingDetailReports)
                    .HasForeignKey(d => d.NetworkId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VisitingDetailReports_Networks");

                entity.HasOne(d => d.Division)
                    .WithMany(p => p.VisitingDetailReports)
                    .HasForeignKey(d => d.DivisionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VisitingDetailReports_Divisions");

            });

            modelBuilder.Entity<VisitingDetailReportProblemCategories>(entity =>
            {
                entity.Property(e => e.VisitingDetailReportPCName).IsUnicode(false);

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('SYSTEM')");

                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedBy)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('SYSTEM')");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.ProblemCategory)
                    .WithMany(p => p.VisitingDetailReportProblemCategories)
                    .HasForeignKey(d => d.ProblemCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VisitingDetailReportProblemCategories_ProblemCategories");

                entity.HasOne(d => d.VisitingDetailReport)
                    .WithMany(p => p.VisitingDetailReportProblemCategories)
                    .HasForeignKey(d => d.VisitingDetailReportId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VisitingDetailReportProblemCategories_VisitingDetailReports");

            });

            modelBuilder.Entity<VisitingDetailReportPICs>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('SYSTEM')");

                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedBy)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('SYSTEM')");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.VisitingDetailReport)
                    .WithMany(p => p.VisitingDetailReportPICs)
                    .HasForeignKey(d => d.VisitingDetailReportId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VisitingDetailReportPICs_VisitingDetailReports");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.VisitingDetailReportPICs)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VisitingDetailReportPICs_Users");

            });

            modelBuilder.Entity<VisitingDetailReportAttachments>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('SYSTEM')");

                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedBy)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('SYSTEM')");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.VisitingDetailReport)
                    .WithMany(p => p.VisitingDetailReportAttachments)
                    .HasForeignKey(d => d.VisitingDetailReportId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VisitingDetailReportAttachments_VisitingDetailReports");

                entity.HasOne(d => d.Attachment)
                    .WithMany(p => p.VisitingDetailReportAttachments)
                    .HasForeignKey(d => d.AttachmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VisitingDetailReportAttachments_Attachments");

            });

            modelBuilder.Entity<CorrectiveActions>(entity =>
            {
                entity.Property(e => e.CorrectiveActionName).IsUnicode(false);
                entity.Property(e => e.CorrectiveActionDetail).IsUnicode(false);
                entity.Property(e => e.ProgressBy).IsUnicode(false);
                entity.Property(e => e.ValidateBy).IsUnicode(false);

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('SYSTEM')");

                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedBy)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('SYSTEM')");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.VisitingDetailReport)
                    .WithMany(p => p.CorrectiveActions)
                    .HasForeignKey(d => d.VisitingDetailReportId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CorrectiveActions_VisitingDetailReports");

            });

            modelBuilder.Entity<CorrectiveActionAttachments>(entity =>
            {

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('SYSTEM')");

                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedBy)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('SYSTEM')");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Attachment)
                    .WithMany(p => p.CorrectiveActionAttachments)
                    .HasForeignKey(d => d.AttachmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CorrectiveActionAttachments_Attachments");

                entity.HasOne(d => d.CorrectiveAction)
                    .WithMany(p => p.CorrectiveActionAttachments)
                    .HasForeignKey(d => d.CorrectiveActionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CorrectiveActionAttachments_CorrectiveActions");

            });

            modelBuilder.Entity<CorrectiveActionProblemCategories>(entity =>
            {
                entity.Property(e => e.CorrectiveActionPCName).IsUnicode(false);

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('SYSTEM')");

                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedBy)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('SYSTEM')");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.ProblemCategory)
                    .WithMany(p => p.CorrectiveActionProblemCategories)
                    .HasForeignKey(d => d.ProblemCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CorrectiveActionProblemCategories_ProblemCategories");

                entity.HasOne(d => d.CorrectiveAction)
                    .WithMany(p => p.CorrectiveActionProblemCategories)
                    .HasForeignKey(d => d.CorrectiveActionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CorrectiveActionProblemCategories_CorrectiveActions");

            });

            modelBuilder.Entity<CorrectiveActionPICs>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('SYSTEM')");

                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedBy)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('SYSTEM')");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.CorrectiveAction)
                    .WithMany(p => p.CorrectiveActionPICs)
                    .HasForeignKey(d => d.CorrectiveActionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CorrectiveActionPICs_CorrectiveActions");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CorrectiveActionPICs)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CorrectiveActionPICs_Users");

            });

            modelBuilder.Entity<UserNetworkMappings>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy)
                            .IsUnicode(false)
                            .HasDefaultValueSql("('SYSTEM')");

                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedBy)
                            .IsUnicode(false)
                            .HasDefaultValueSql("('SYSTEM')");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Network)
                            .WithMany(p => p.UserNetworkMappings)
                            .HasForeignKey(d => d.NetworkId)
                            .HasConstraintName("FK_UserNetworkMappings_Networks");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserNetworkMappings)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserNetworkMappings_Users");
            });

            modelBuilder.Entity<UserProblemCategoryMappings>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy)
                            .IsUnicode(false)
                            .HasDefaultValueSql("('SYSTEM')");

                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedBy)
                            .IsUnicode(false)
                            .HasDefaultValueSql("('SYSTEM')");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.ProblemCategory)
                            .WithMany(p => p.UserProblemCategoryMappings)
                            .HasForeignKey(d => d.ProblemCategoryId)
                            .HasConstraintName("FK_UserProblemCategoryMappings_ProblemCategories");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserProblemCategoryMappings)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserProblemCategoryMappings_Users");
            });

            modelBuilder.Entity<Authentications>(entity =>
            {
                entity.Property(e => e.Otp).IsUnicode(false);
                entity.Property(e => e.Username).IsUnicode(false);
                entity.Property(e => e.UserData).IsUnicode(false);
            });

            modelBuilder.Entity<UserFcmTokens>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('SYSTEM')");

                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedBy)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('SYSTEM')");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.Token).IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserFcmTokens)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserFcmTokens_Users");
            });

            modelBuilder.Entity<PushNotifications>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('SYSTEM')");

                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedBy)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('SYSTEM')");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.PushNotificationTitle).IsUnicode(false);

                entity.Property(e => e.PushNotificationBody).IsUnicode(false);

                entity.Property(e => e.ScreenID).IsUnicode(false);

                entity.Property(e => e.Screen).IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PushNotifications)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PushNotifications_UserSender");

                entity.HasOne(d => d.UserRecipient)
                    .WithMany(p => p.PushNotificationRecipients)
                    .HasForeignKey(d => d.RecipientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PushNotificationRecipients_UserRecipients");
            });

            modelBuilder.Entity<EventMasterDatas>(entity =>
            {
                entity.Property(e => e.EventMasterDataName).IsUnicode(false);

                entity.Property(e => e.EventMasterDataLocation).IsUnicode(false);

                entity.Property(e => e.EventMasterDataLatitude).IsUnicode(false);

                entity.Property(e => e.EventMasterDataLongitude).IsUnicode(false);

                entity.Property(e => e.StartDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EndDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('SYSTEM')");

                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedBy)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('SYSTEM')");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<Events>(entity =>
            {
                entity.Property(e => e.EventStatus).IsUnicode(false);

                entity.Property(e => e.EventComment).IsUnicode(false);

                entity.Property(e => e.ApprovedByManager).IsUnicode(false);

                entity.Property(e => e.ApprovedByGM).IsUnicode(false);

                entity.Property(e => e.StartDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EndDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('SYSTEM')");

                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedBy)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('SYSTEM')");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.EventMasterData)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.EventMasterDataId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Events_EventMasterDatas");

                entity.HasOne(d => d.VisitingType)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.VisitingTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Events_VisitingTypes");

            });

            modelBuilder.Entity<EventPeoples>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('SYSTEM')");

                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedBy)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('SYSTEM')");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.EventPeoples)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EventPeoples_Events");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.EventPeoples)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EventPeoples_Users");

            });

            modelBuilder.Entity<EventDetailReports>(entity =>
            {
                entity.Property(e => e.EventDetailReportProblemIdentification).IsUnicode(false);

                entity.Property(e => e.EventCAProblemIdentification).IsUnicode(false);

                entity.Property(e => e.EventDetailReportStatus).IsUnicode(false);

                entity.Property(e => e.EventDetailReportFlagging).IsUnicode(false);

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('SYSTEM')");

                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedBy)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('SYSTEM')");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.EventDetailReports)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EventDetailReports_Events");

                entity.HasOne(d => d.EventMasterData)
                    .WithMany(p => p.EventDetailReports)
                    .HasForeignKey(d => d.EventMasterDataId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EventDetailReports_EventMasterDatas");

                entity.HasOne(d => d.Division)
                    .WithMany(p => p.EventDetailReports)
                    .HasForeignKey(d => d.DivisionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EventDetailReports_Divisions");

            });

            modelBuilder.Entity<EventDetailReportProblemCategories>(entity =>
            {
                entity.Property(e => e.EventDetailReportPCName).IsUnicode(false);

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('SYSTEM')");

                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedBy)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('SYSTEM')");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.ProblemCategory)
                    .WithMany(p => p.EventDetailReportProblemCategories)
                    .HasForeignKey(d => d.ProblemCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EventDetailReportProblemCategories_ProblemCategories");

                entity.HasOne(d => d.EventDetailReport)
                    .WithMany(p => p.EventDetailReportProblemCategories)
                    .HasForeignKey(d => d.EventDetailReportId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EventDetailReportProblemCategories_EventDetailReports");

            });

            modelBuilder.Entity<EventDetailReportPICs>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('SYSTEM')");

                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedBy)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('SYSTEM')");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.EventDetailReport)
                    .WithMany(p => p.EventDetailReportPICs)
                    .HasForeignKey(d => d.EventDetailReportId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EventDetailReportPICs_EventDetailReports");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.EventDetailReportPICs)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EventDetailReportPICs_Users");

            });

            modelBuilder.Entity<EventDetailReportAttachments>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('SYSTEM')");

                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedBy)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('SYSTEM')");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.EventDetailReport)
                    .WithMany(p => p.EventDetailReportAttachments)
                    .HasForeignKey(d => d.EventDetailReportId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EventDetailReportAttachments_EventDetailReports");

                entity.HasOne(d => d.Attachment)
                    .WithMany(p => p.EventDetailReportAttachments)
                    .HasForeignKey(d => d.AttachmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EventDetailReportAttachments_Attachments");

            });

            modelBuilder.Entity<EventCAs>(entity =>
            {
                entity.Property(e => e.EventCAName).IsUnicode(false);
                entity.Property(e => e.EventCADetail).IsUnicode(false);
                entity.Property(e => e.ProgressBy).IsUnicode(false);
                entity.Property(e => e.ValidateBy).IsUnicode(false);

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('SYSTEM')");

                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedBy)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('SYSTEM')");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.EventDetailReport)
                    .WithMany(p => p.EventCAs)
                    .HasForeignKey(d => d.EventDetailReportId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EventCorrectiveActions_EventDetailReports");

            });

            modelBuilder.Entity<EventCAAttachments>(entity =>
            {

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('SYSTEM')");

                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedBy)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('SYSTEM')");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Attachment)
                    .WithMany(p => p.EventCAAttachments)
                    .HasForeignKey(d => d.AttachmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EventCorrectiveActionAttachments_Attachments");

                entity.HasOne(d => d.EventCA)
                    .WithMany(p => p.EventCAAttachments)
                    .HasForeignKey(d => d.EventCAId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EventCorrectiveActionAttachments_EventCorrectiveActions");

            });

            modelBuilder.Entity<EventCAProblemCategories>(entity =>
            {
                entity.Property(e => e.EventCAPCName).IsUnicode(false);

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('SYSTEM')");

                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedBy)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('SYSTEM')");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.ProblemCategory)
                    .WithMany(p => p.EventCAProblemCategories)
                    .HasForeignKey(d => d.ProblemCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EventCorrectiveActionProblemCategories_ProblemCategories");

                entity.HasOne(d => d.EventCA)
                    .WithMany(p => p.EventCAProblemCategories)
                    .HasForeignKey(d => d.EventCAId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EventCorrectiveActionProblemCategories_EventCorrectiveActions");

            });

            modelBuilder.Entity<EventCAPICs>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('SYSTEM')");

                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedBy)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('SYSTEM')");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.EventCA)
                    .WithMany(p => p.EventCAPICs)
                    .HasForeignKey(d => d.EventCAId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EventCorrectiveActionPICs_EventCorrectiveActions");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.EventCAPICs)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EventCorrectiveActionPICs_Users");

            });

            modelBuilder.Entity<Checklists>(entity =>
            {
                entity.Property(e => e.ChecklistItem).IsUnicode(false);

                entity.Property(e => e.ChecklistIdentification).IsUnicode(false);

                entity.Property(e => e.ChecklistActualCondition).IsUnicode(false);

                entity.Property(e => e.ChecklistActualDetail).IsUnicode(false);

                entity.Property(e => e.ChecklistStatus).IsUnicode(false);

                entity.Property(e => e.ChecklistFix).IsUnicode(false);

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('SYSTEM')");

                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedBy)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('SYSTEM')");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Visiting)
                    .WithMany(p => p.Checklists)
                    .HasForeignKey(d => d.VisitingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Checklists_Visitings");

                entity.HasOne(d => d.Network)
                    .WithMany(p => p.Checklists)
                    .HasForeignKey(d => d.NetworkId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Checklists_Networks");

                entity.HasOne(d => d.Attachment)
                    .WithMany(p => p.Checklists)
                    .HasForeignKey(d => d.AttachmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Checklists_Attachments");
            });

            modelBuilder.Entity<ChecklistPICs>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('SYSTEM')");

                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedBy)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('SYSTEM')");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Checklist)
                    .WithMany(p => p.ChecklistPICs)
                    .HasForeignKey(d => d.ChecklistId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ChecklistPICs_Checklists");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ChecklistPICs)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ChecklistPICs_Users");
            });

            modelBuilder.Entity<ChecklistEvidences>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('SYSTEM')");

                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedBy)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('SYSTEM')");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Checklist)
                    .WithMany(p => p.ChecklistEvidences)
                    .HasForeignKey(d => d.ChecklistId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ChecklistEvidences_Checklists");

                entity.HasOne(d => d.Attachment)
                    .WithMany(p => p.ChecklistEvidences)
                    .HasForeignKey(d => d.AttachmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ChecklistEvidences_Attachments");
            });

            modelBuilder.Entity<Ranks>(entity =>
            {
                entity.Property(e => e.UserBOD).IsUnicode(false);

                entity.Property(e => e.UserBODName).IsUnicode(false);

                entity.Property(e => e.UserGM).IsUnicode(false);

                entity.Property(e => e.UserGMName).IsUnicode(false);

                entity.Property(e => e.UserManager).IsUnicode(false);

                entity.Property(e => e.UserManagerName).IsUnicode(false);

                entity.Property(e => e.UserStaff).IsUnicode(false);

                entity.Property(e => e.UserStaffName).IsUnicode(false);

                entity.Property(e => e.DivisionName).IsUnicode(false);

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('SYSTEM')");

                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedBy)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('SYSTEM')");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");
            });
        }
    }
}
