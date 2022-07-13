using System;
using System.Collections.Generic;
using MPMSRS.Models.Entities;

namespace MPMSRS.Models.VM
{
    public class EventDetailReportDto
    {
        public Guid EventId { get; set; }
        public Guid EventMasterDataId { get; set; }
        public string EventMasterDataName { get; set; }
        public string EventMasterDataLocation { get; set; }
        public string EventMasterDataLatitude { get; set; }
        public string EventMasterDataLongitude { get; set; }
        public List<EmployeeListInEventPeople> EventPeoples { get; set; }
        public List<EventDetailReportDtoViewModel> EventDetailReport { get; set; }
        public string EventComment { get; set; }
        public string EventStatus { get; set; }
        public string ApprovedByManager { get; set; }
        public string ApprovedByGM { get; set; }
        public bool IsOnline { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedByName { get; set; }
    }

    public class EventDetailReportDtoModel
    {
        public Guid EventDetailReportId { get; set; }
        public Guid EventId { get; set; }
        public Guid EventMasterDataId { get; set; }
        public Guid DivisionId { get; set; }
        public string EventDetailReportProblemIdentification { get; set; }
        public string EventCAProblemIdentification { get; set; }
        public string EventDetailReportStatus { get; set; }
        public DateTime? EventDetailReportDeadline { get; set; }
        public string EventDetailReportFlagging { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class EventDetailReportDtoViewModel
    {
        public Guid EventDetailReportId { get; set; }
        public List<EventDetailReportProblemCategories> EventDetailReportPC { get; set; }
        public List<EventDetailReportAttachments> EventDetailReportAttachment { get; set; }
        public List<VisitingDetailReportPICVMViewModel> EventDetailReportPIC { get; set; }
        public List<ImageUrl> EventDetailAttachmentDetail { get; set; }
        public Divisions Division { get; set; }
        public string EventDetailReportProblemIdentification { get; set; }
        public string EventCAProblemIdentification { get; set; }
        public string EventDetailReportStatus { get; set; }
        public DateTime? EventDetailReportDeadline { get; set; }
        public string EventDetailReportFlagging { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedByName { get; set; }
    }

    public class EventDetailReportForCreationDtoModel
    {
        public Guid EventId { get; set; }
        public Guid EventMasterDataId { get; set; }
        public Guid DivisionId { get; set; }
        public string EventDetailReportProblemIdentification { get; set; }
        public string EventCAProblemIdentification { get; set; }
        public string EventDetailReportStatus { get; set; }
        public DateTime? EventDetailReportDeadline { get; set; }
        public string EventDetailReportFlagging { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class EventDetailReportForCreationDto
    {
        public Guid EventId { get; set; }
        public Guid EventMasterDataId { get; set; }
        public Guid DivisionId { get; set; }
        public List<EventDetailReportProblemCategoryForCreationDto> PCList { get; set; }
        public List<EventDetailReportPicForCreationViewDto> PICList { get; set; }
        public List<EventDetailReportAttachmentForCreationDto> AttachmentList { get; set; }
        public string EventDetailReportProblemIdentification { get; set; }
        public string EventCAProblemIdentification { get; set; }
        public string EventDetailReportStatus { get; set; }
        public DateTime? EventDetailReportDeadline { get; set; }
        public string EventDetailReportFlagging { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class EventDetailReportForUpdateDtoModel
    {
        public Guid EventDetailReportId { get; set; }
        public Guid EventId { get; set; }
        public Guid EventMasterDataId { get; set; }
        public Guid DivisionId { get; set; }
        public string EventDetailReportProblemIdentification { get; set; }
        public string EventCAProblemIdentification { get; set; }
        public string EventDetailReportStatus { get; set; }
        public DateTime? EventDetailReportDeadline { get; set; }
        public string EventDetailReportFlagging { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class EventDetailReportForUpdateDto
    {
        public Guid EventDetailReportId { get; set; }
        public Guid EventId { get; set; }
        public Guid EventMasterDataId { get; set; }
        public Guid DivisionId { get; set; }
        public List<EventDetailReportProblemCategoryForCreationDtoModel> PCList { get; set; }
        public List<EventDetailReportPicForCreationViewDtoModel> PICList { get; set; }
        public List<EventDetailReportAttachmentForCreationDto> AttachmentList { get; set; }
        public string EventDetailReportProblemIdentification { get; set; }
        public string EventCAProblemIdentification { get; set; }
        public string EventDetailReportStatus { get; set; }
        public DateTime? EventDetailReportDeadline { get; set; }
        public string EventDetailReportFlagging { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class EventDetailReportForDeletionDto
    {
        public bool IsDeleted { get; set; }
    }

    public class EventDetailReportProblemCategoryDto
    {
        public Guid EventDetailReportPCId { get; set; }
        public Guid ProblemCategoryId { get; set; }
        public Guid EventDetailReportId { get; set; }
        public string EventDetailReportPCName { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class EventDetailReportProblemCategoryForCreationDto
    {
        public Guid ProblemCategoryId { get; set; }
        public Guid EventDetailReportId { get; set; }
        public string EventDetailReportPCName { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class EventDetailReportProblemCategoryForCreationDtoModel
    {
        public Guid ProblemCategoryId { get; set; }
        public string EventDetailReportPCName { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class EventDetailReportProblemCategoryForUpdateDto
    {
        public Guid EventDetailReportPCId { get; set; }
        public Guid ProblemCategoryId { get; set; }
        public Guid EventDetailReportId { get; set; }
        public string EventDetailReportPCName { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class EventDetailReportPicDto
    {
        public Guid EventDetailReportId { get; set; }
        public Guid EmployeeId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class EventDetailReportPicForCreationDto
    {
        public Guid EmployeeId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class EventDetailReportPicForCreationViewDto
    {
        public Guid EventDetailReportId { get; set; }
        public Guid EmployeeId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class EventDetailReportPicForCreationViewDtoModel
    {
        public Guid EmployeeId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class EventDetailReportPicForUpdateDto
    {
        public Guid EventDetailReportId { get; set; }
        public Guid EmployeeId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class EventDetailReportAttachmentDto
    {
        public Guid EventDetailReportAttachmentId { get; set; }
        public Guid EventDetailReportId { get; set; }
        public Guid AttachmentId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class EventDetailReportAttachmentForCreationDto
    {
        public string ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class EventDetailReportAttachmentForCreationViewDto
    {
        public Guid EventDetailReportId { get; set; }
        public Guid AttachmentId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class EventDetailReportAttachmentForUpdateDto
    {
        public Guid EventDetailReportAttachmentId { get; set; }
        public Guid EventDetailReportId { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class EventDetailReportAttachmentForUpdateDtoModel
    {
        public Guid EventDetailReportAttachmentId { get; set; }
        public Guid EventDetailReportId { get; set; }
        public Guid AttachmentId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class EventCADto
    {
        public Guid EventCAId { get; set; }
        public Guid EventDetailReportId { get; set; }
        public string EventCAName { get; set; }
        public string ProgressBy { get; set; }
        public string ValidateBy { get; set; }
        public string EventCADetail { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class EventCAForCreationDto
    {
        public Guid EventDetailReportId { get; set; }
        public string EventCAName { get; set; }
        public string ProgressBy { get; set; }
        public string ValidateBy { get; set; }
        public string EventCADetail { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class EventCAForUpdateDto
    {
        public Guid EventCAId { get; set; }
        public Guid EventDetailReportId { get; set; }
        public string EventDetailReportStatus { get; set; }
        public List<EventCAAttachmentForCreationViewDto> AttachmentList { get; set; }
        public string EventCAName { get; set; }
        public string ProgressBy { get; set; }
        public string ValidateBy { get; set; }
        public string EventCADetail { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class EventCorrectivActionAttachmentForCreationDto
    {
        public Guid EventCAId { get; set; }
        public Guid AttachmentId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class EventCAAttachmentForCreationViewDto
    {
        public string ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class EventDetailReportForDeleteDto
    {
        public bool IsDeleted { get; set; }
    }

    public class EventCADtoVMList
    {
        public Guid EventId { get; set; }
        public Guid EventMasterDataId { get; set; }
        public string EventMasterDataName { get; set; }
        public string EventMasterDataLocation { get; set; }
        public string EventMasterDataLatitude { get; set; }
        public string EventMasterDataLongitude { get; set; }
        public Guid CorrectiveActionId { get; set; }
        public Guid EventDetailReportId { get; set; }
        public string EventDetailReportProblemIdentification { get; set; }
        public string EventCAProblemIdentification { get; set; }
        public string EventDetailReportStatus { get; set; }
        public DateTime EventDetailReportDeadline { get; set; }
        public string EventCAName { get; set; }
        public string ProgressBy { get; set; }
        public string ValidateBy { get; set; }
        public string EventCADetail { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class EventCADtoViewModel
    {
        public List<EventCADtoVMList> Data { get; set; }
        public PagesViewModel Pages { get; set; }
    }

    public class EventCADetailDto
    {
        public Guid EventCAId { get; set; }
        public Guid EventId { get; set; }
        public Guid EventMasterDataId { get; set; }
        public string EventMasterDataName { get; set; }
        public Guid EventDetailReportId { get; set; }
        public string EventDetailReportProblemIdentification { get; set; }
        public string EventCAProblemIdentification { get; set; }
        public string EventDetailReportStatus { get; set; }
        public DateTime? EventDetailReportDeadline { get; set; }
        public List<EventCAAttachmentDetail> AttachmentDetail { get; set; }
        public string EventCAName { get; set; }
        public string ProgressBy { get; set; }
        public string ProgressByName { get; set; }
        public string ValidateBy { get; set; }
        public string ValidateByName { get; set; }
        public string EventCADetail { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedByName { get; set; }
    }

    public class EventCAAttachmentDetail
    {
        public Guid EventCAAttachmentId { get; set; }
        public Guid AttachmentId { get; set; }
        public string ImageUrl { get; set; }
    }

    public class EventDetailReportExportExcelDto
    {
        public Guid EventDetailReportId { get; set; }
        public Guid EventId { get; set; }
        public List<string> EventDetailReportPC { get; set; }
    }

    public class EventDetailReportListForCreationImportExcelDto
    {
        public Guid EventId { get; set; }
        public Guid EventMasterDataId { get; set; }
    }

    public class EventDetailReportPICVMViewModel
    {
        public Guid EmployeeId { get; set; }
        public string EmployeeName { get; set; }
    }
}
