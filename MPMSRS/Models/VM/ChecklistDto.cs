using System;
using System.Collections.Generic;
using MPMSRS.Models.Entities;

namespace MPMSRS.Models.VM
{
    public class ChecklistDto
    {
        public Guid ChecklistId { get; set; }
        public Guid VisitingId { get; set; }
        public Guid NetworkId { get; set; }
        public Guid AttachmentId { get; set; }
        public string ChecklistItem { get; set; }
        public string ChecklistIdentification { get; set; }
        public string ChecklistActualCondition { get; set; }
        public string ChecklistActualDetail { get; set; }
        public string ChecklistFix { get; set; }
        public string ChecklistStatus { get; set; }
        public DateTime? ChecklistDeadline { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class ChecklistDtoLoginViewModel
    {
        public Guid VisitingId { get; set; }
        public Guid VisitingTypeId { get; set; }
        public string VisitingTypeName { get; set; }
        public Guid NetworkId { get; set; }
        public string AhmCode { get; set; }
        public string MDCode { get; set; }
        public string DealerName { get; set; }
        public string NetworkAddress { get; set; }
        public string NetworkCity { get; set; }
        public string NetworkLatitude { get; set; }
        public string NetworkLongitude { get; set; }
        public List<ChecklistViewModel> Checklists { get; set; }
        public string VisitingComment { get; set; }
        public string VisitingStatus { get; set; }
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

    public class ChecklistViewModel
    {
        public Guid ChecklistId { get; set; }
        public List<ChecklistEvidences> ChecklistEvidences { get; set; }
        public List<VisitingDetailReportPICVMViewModel> ChecklistPICs { get; set; }
        public List<ChecklistImageUrl> ChecklistEvidenceDetail { get; set; }
        public Networks Network { get; set; }
        public Guid ChecklistFotoStandardId { get; set; }
        public string ChecklistFotoStandardUrl { get; set; }
        public string ChecklistItem { get; set; }
        public string ChecklistIdentification { get; set; }
        public string ChecklistActualCondition { get; set; }
        public string ChecklistActualDetail { get; set; }
        public string ChecklistFix { get; set; }
        public string ChecklistStatus { get; set; }
        public DateTime? ChecklistDeadline { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedByName { get; set; }
    }

    public class ChecklistForCreationDto
    {
        public Guid VisitingId { get; set; }
        public Guid NetworkId { get; set; }
        public Guid AttachmentId { get; set; }
        public string ChecklistItem { get; set; }
        public string ChecklistIdentification { get; set; }
        public string ChecklistActualCondition { get; set; }
        public string ChecklistActualDetail { get; set; }
        public string ChecklistFix { get; set; }
        public string ChecklistStatus { get; set; }
        public DateTime? ChecklistDeadline { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class ChecklistForUpdateDto
    {
        public Guid ChecklistId { get; set; }
        public Guid VisitingId { get; set; }
        public Guid NetworkId { get; set; }
        public Guid AttachmentId { get; set; }
        public string ChecklistItem { get; set; }
        public string ChecklistIdentification { get; set; }
        public string ChecklistActualCondition { get; set; }
        public string ChecklistActualDetail { get; set; }
        public string ChecklistFix { get; set; }
        public string ChecklistStatus { get; set; }
        public DateTime? ChecklistDeadline { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class ChecklistForDeleteDto
    {
        public bool IsDeleted { get; set; }
    }

    public class ChecklistListForCreationDto
    {
        public Guid VisitingId { get; set; }
        public List<ChecklistPicForCreationViewDto> PICList { get; set; }
        public List<ChecklistEvidenceForCreationViewDto> EvidenceList { get; set; }
        public Guid NetworkId { get; set; }
        public string ImageUrl { get; set; }
        public string ChecklistItem { get; set; }
        public string ChecklistIdentification { get; set; }
        public string ChecklistActualCondition { get; set; }
        public string ChecklistActualDetail { get; set; }
        public string ChecklistFix { get; set; }
        public string ChecklistStatus { get; set; }
        public DateTime? ChecklistDeadline { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class ChecklistListForUpdateDto
    {
        public Guid ChecklistId { get; set; }
        public Guid VisitingId { get; set; }
        public List<ChecklistPicForCreationDto> PICList { get; set; }
        public List<ChecklistEvidenceForCreationViewDto> EvidenceList { get; set; }
        public Guid NetworkId { get; set; }
        public string ImageUrl { get; set; }
        public string ChecklistItem { get; set; }
        public string ChecklistIdentification { get; set; }
        public string ChecklistActualCondition { get; set; }
        public string ChecklistActualDetail { get; set; }
        public string ChecklistFix { get; set; }
        public string ChecklistStatus { get; set; }
        public DateTime? ChecklistDeadline { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class ChecklistPicForCreationViewDto
    {
        public Guid EmployeeId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class ChecklistEvidenceForCreationViewDto
    {
        public string ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class ChecklistIdentificationModel
    {
        public List<ChecklistViewModel> Checklists { get; set; }
    }

    public class ChecklistImageUrl
    {
        public Guid ChecklistId { get; set; }
        public Guid ChecklistEvidenceId { get; set; }
        public string imageUrl { get; set; }
    }

    public class ChecklistPicDto
    {
        public Guid ChecklistPICId { get; set; }
        public Guid ChecklistId { get; set; }
        public Guid EmployeeId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class ChecklistPicForCreationDto
    {
        public Guid ChecklistId { get; set; }
        public Guid EmployeeId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class ChecklistEvidenceDto
    {
        public Guid ChecklistEvidenceId { get; set; }
        public Guid ChecklistId { get; set; }
        public Guid AttachmentId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class ChecklistEvidenceForCreationDto
    {
        public Guid ChecklistId { get; set; }
        public Guid AttachmentId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class ChecklistDeadlineUpdateDto
    {
        public DateTime ChecklistDeadline { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }
}
