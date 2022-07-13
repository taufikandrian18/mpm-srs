using System;
using System.Collections.Generic;
using MPMSRS.Models.Entities;

namespace MPMSRS.Models.VM
{
    public class VisitingDetailReportDto
    {
        public Guid VisitingId { get; set; }
        public Networks Network { get; set; }
        public VisitingTypes VisitingType { get; set; }
        public string VisitingComment { get; set; }
        public string VisitingCommentByManager { get; set; }
        public string VisitingStatus { get; set; }
        public string ApprovedByManager { get; set; }
        public string ApprovedByGM { get; set; }
        public bool IsOnline { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<VisitingPeoples> VisitingPeople { get; set; }
        public List<VisitingNoteMappings> VisitingNoteMapping { get; set; }
        public List<VisitingDetailReportViewModel> VisitingDetailReport { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class VisitingDetailReportPICVMViewModel
    {
        public Guid EmployeeId { get; set; }
        public string EmployeeName { get; set; }
    }

    public class VisitingDetailReportExportExcelDto
    {
        public Guid VisitingDetailReportId { get; set; }
        public Guid VisitingId { get; set; }
        public List<string> VisitingDetailReportPC { get; set; }
    }

    public class VisitingDetailReportIdentificationModel
    {
        public List<VisitingDetailReportViewModel> VisitingDetailReport { get; set; }
    }

    public class VisitingDetailReportViewModel
    {
        public Guid VisitingDetailReportId { get; set; }
        public List<VisitingDetailReportProblemCategories> VisitingDetailReportPC { get; set; }
        public List<VisitingDetailReportAttachments> VisitingDetailReportAttachment { get; set; }
        public List<VisitingDetailReportPICVMViewModel> VisitingDetailReportPIC { get; set; }
        public List<ImageUrl> VisitingDetailAttachmentDetail { get; set; }
        public Divisions Division { get; set; }
        public Networks Network { get; set; }
        public string VisitingDetailReportProblemIdentification { get; set; }
        public string CorrectiveActionProblemIdentification { get; set; }
        public string VisitingDetailReportStatus { get; set; }
        public DateTime VisitingDetailReportDeadline { get; set; }
        public string VisitingDetailReportFlagging { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class VisitingDetailReportViewExcelModel
    {
        public Guid VisitingDetailReportId { get; set; }
        public List<VisitingDetailReportProblemCategories> VisitingDetailReportPC { get; set; }
        public List<VisitingDetailReportAttachments> VisitingDetailReportAttachment { get; set; }
        public List<VisitingDetailReportPICVMViewModel> VisitingDetailReportPIC { get; set; }
        public List<ImageUrl> VisitingDetailAttachmentDetail { get; set; }
        public Divisions Division { get; set; }
        public Guid NetworkId { get; set; }
        public string AhmCode { get; set; }
        public string MDCode { get; set; }
        public string DealerName { get; set; }
        public string NetworkAddress { get; set; }
        public string NetworkCity { get; set; }
        public string NetworkLatitude { get; set; }
        public string NetworkLongitude { get; set; }
        public string VisitingTypeName { get; set; }
        public DateTime VisitingStartDate { get; set; }
        public DateTime VisitingEndDate { get; set; }
        public string VisitingDetailReportProblemIdentification { get; set; }
        public string CorrectiveActionProblemIdentification { get; set; }
        public string VisitingDetailReportStatus { get; set; }
        public DateTime VisitingDetailReportDeadline { get; set; }
        public string VisitingDetailReportFlagging { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedByName { get; set; }
    }

    public class ImageUrl
    {
        public Guid VisitingDetailReportId { get; set; }
        public Guid VisitingDetailReportAttachmentId { get; set; }
        public string imageUrl { get; set; }
    }

    public class VisitingDetailReportListForCreationImportExcelDto
    {
        public Guid VisitingId { get; set; }
        public Guid NetworkId { get; set; }
        public Guid DivisionId { get; set; }
    }

    public class VisitingDetailReportListForCreationDto
    {
        public Guid VisitingId { get; set; }
        public List<VisitingDetailReportProblemCategoryForCreationViewDto> PCList { get; set; }
        public List<VisitingDetailReportPicForCreationViewDto> PICList { get; set; }
        public List<VisitingDetailReportAttachmentForCreationViewDto> AttachmentList { get; set; }
        public Guid NetworkId { get; set; }
        public Guid DivisionId { get; set; }
        public string VisitingDetailReportProblemIdentification { get; set; }
        public string CorrectiveActionProblemIdentification { get; set; }
        public string VisitingDetailReportStatus { get; set; }
        public DateTime VisitingDetailReportDeadline { get; set; }
        public string VisitingDetailReportFlagging { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class VisitingDetailReportListForUpdateDto
    {
        public Guid VisitingDetailReportId { get; set; }
        public Guid VisitingId { get; set; }
        public List<VisitingDetailReportProblemCategoryForCreationDto> PCList { get; set; }
        public List<VisitingDetailReportPicForCreationDto> PICList { get; set; }
        public List<VisitingDetailReportAttachmentForCreationViewDto> AttachmentList { get; set; }
        public Guid NetworkId { get; set; }
        public Guid DivisionId { get; set; }
        public string VisitingDetailReportProblemIdentification { get; set; }
        public string CorrectiveActionProblemIdentification { get; set; }
        public string VisitingDetailReportStatus { get; set; }
        public DateTime VisitingDetailReportDeadline { get; set; }
        public string VisitingDetailReportFlagging { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class VisitingDetailReportDeadlineUpdateDto
    {
        public DateTime VisitingDetailReportDeadline { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class VisitingDetailReportDtoModel
    {
        public Guid VisitingDetailReportId { get; set; }
        public Guid VisitingId { get; set; }
        public Guid NetworkId { get; set; }
        public Guid DivisionId { get; set; }
        public string VisitingDetailReportProblemIdentification { get; set; }
        public string CorrectiveActionProblemIdentification { get; set; }
        public string VisitingDetailReportStatus { get; set; }
        public DateTime VisitingDetailReportDeadline { get; set; }
        public string VisitingDetailReportFlagging { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class VisitingDetailReportForCreationDto
    {
        public Guid VisitingId { get; set; }
        public Guid NetworkId { get; set; }
        public Guid DivisionId { get; set; }
        public string VisitingDetailReportProblemIdentification { get; set; }
        public string CorrectiveActionProblemIdentification { get; set; }
        public string VisitingDetailReportStatus { get; set; }
        public DateTime VisitingDetailReportDeadline { get; set; }
        public string VisitingDetailReportFlagging { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class VisitingDetailReportForUpdateDto
    {
        public Guid VisitingDetailReportId { get; set; }
        public Guid VisitingId { get; set; }
        public Guid NetworkId { get; set; }
        public Guid DivisionId { get; set; }
        public string VisitingDetailReportProblemIdentification { get; set; }
        public string CorrectiveActionProblemIdentification { get; set; }
        public string VisitingDetailReportStatus { get; set; }
        public DateTime VisitingDetailReportDeadline { get; set; }
        public string VisitingDetailReportFlagging { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class VisitingDetailReportProblemCategoryDto
    {
        public Guid VisitingDetailReportPCId { get; set; }
        public Guid ProblemCategoryId { get; set; }
        public Guid VisitingDetailReportId { get; set; }
        public string VisitingDetailReportPCName { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class VisitingDetailReportProblemCategoryForCreationViewDto
    {
        public Guid ProblemCategoryId { get; set; }
        public string VisitingDetailReportPCName { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class VisitingDetailReportProblemCategoryForCreationDto
    {
        public Guid ProblemCategoryId { get; set; }
        public Guid VisitingDetailReportId { get; set; }
        public string VisitingDetailReportPCName { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class VisitingDetailReportProblemCategoryForUpdateDto
    {
        public Guid VisitingDetailReportPCId { get; set; }
        public Guid ProblemCategoryId { get; set; }
        public Guid VisitingDetailReportId { get; set; }
        public string VisitingDetailReportPCName { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class VisitingDetailReportPicDto
    {
        public Guid VisitingDetailReportPICId { get; set; }
        public Guid VisitingDetailReportId { get; set; }
        public Guid EmployeeId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class VisitingDetailReportPicForCreationViewDto
    {
        public Guid EmployeeId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class VisitingDetailReportPicForCreationDto
    {
        public Guid VisitingDetailReportId { get; set; }
        public Guid EmployeeId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class VisitingDetailReportPicForUpdateDto
    {
        public Guid VisitingDetailReportPICId { get; set; }
        public Guid VisitingDetailReportId { get; set; }
        public Guid EmployeeId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class VisitingDetailReportAttachmentDto
    {
        public Guid VisitingDetailReportAttachmentId { get; set; }
        public Guid VisitingDetailReportId { get; set; }
        public Guid AttachmentId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class VisitingDetailReportAttachmentForCreationDto
    {
        public Guid VisitingDetailReportId { get; set; }
        public Guid AttachmentId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class VisitingDetailReportAttachmentForUpdateDto
    {
        public Guid VisitingDetailReportAttachmentId { get; set; }
        public Guid VisitingDetailReportId { get; set; }
        public Guid AttachmentId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class VisitingDetailReportAttachmentForCreationViewDto
    {
        public string ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class VisitingDetailReportAttachmentForUpdateViewDto
    {
        public Guid VisitingDetailReportAttachmentId { get; set; }
        public Guid VisitingDetailReportId { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class CorrectiveActionDto
    {
        public Guid CorrectiveActionId { get; set; }
        public Guid VisitingDetailReportId { get; set; }
        public string CorrectiveActionName { get; set; }
        public string ProgressBy { get; set; }
        public string ValidateBy { get; set; }
        public string CorrectiveActionDetail { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class CorrectiveActionForCreationDto
    {
        public Guid VisitingDetailReportId { get; set; }
        public string CorrectiveActionName { get; set; }
        public string ProgressBy { get; set; }
        public string ValidateBy { get; set; }
        public string CorrectiveActionDetail { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class CorrectiveActionForUpdateDto
    {
        public Guid CorrectiveActionId { get; set; }
        public Guid VisitingDetailReportId { get; set; }
        public string VisitingDetailReportStatus { get; set; }
        public List<CorrectiveActionAttachmentForCreationViewDto> AttachmentList { get; set; }
        public string CorrectiveActionName { get; set; }
        public string ProgressBy { get; set; }
        public string ValidateBy { get; set; }
        public string CorrectiveActionDetail { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class VisitingDetailReportForDeleteDto
    {
        public bool IsDeleted { get; set; }
    }
}
