using System;
using System.Collections.Generic;
using MPMSRS.Models.Entities;

namespace MPMSRS.Models.VM
{
    public class CorrectiveActionDtoVMList
    {
        public Guid VisitingId { get; set; }
        public Guid VisitingTypeId { get; set; }
        public string VisitingTypeName { get; set; }
        public Guid NetworkId { get; set; }
        public Guid DivisionId { get; set; }
        public string DivisionName { get; set; }
        public string AhmCode { get; set; }
        public string MDCode { get; set; }
        public string DealerName { get; set; }
        public string NetworkAddress { get; set; }
        public string NetworkCity { get; set; }
        public Guid CorrectiveActionId { get; set; }
        public Guid VisitingDetailReportId { get; set; }
        public string VisitingDetailReportProblemIdentification { get; set; }
        public string CorrectiveActionProblemIdentification { get; set; }
        public string VisitingDetailReportStatus { get; set; }
        public DateTime VisitingDetailReportDeadline { get; set; }
        public string CorrectiveActionName { get; set; }
        public string ProgressBy { get; set; }
        public string ValidateBy { get; set; }
        public string CorrectiveActionDetail { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedByName { get; set; }
    }

    public class CorrectiveActionNetworkDtoViewModel
    {
        public List<CorrectiveActionDtoVMList> Data { get; set; }
        public PagesViewModel Pages { get; set; }
    }

    public class CorrectiveActionDetailDto
    {
        public Guid CorrectiveActionId { get; set; }
        public Guid VisitingId { get; set; }
        public Guid VisitingTypeId { get; set; }
        public string VisitingTypeName { get; set; }
        public Networks Network { get; set; }
        public Guid VisitingDetailReportId { get; set; }
        public string VisitingDetailReportProblemIdentification { get; set; }
        public string CorrectiveActionProblemIdentification { get; set; }
        public string VisitingDetailReportStatus { get; set; }
        public DateTime VisitingDetailReportDeadline { get; set; }
        public List<CorrectiveActionAttachmentDetail> AttachmentDetail { get; set; }
        public string CorrectiveActionName { get; set; }
        public string ProgressBy { get; set; }
        public string ProgressByName { get; set; }
        public string ValidateBy { get; set; }
        public string ValidateByName { get; set; }
        public string CorrectiveActionDetail { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedByName { get; set; }
    }

    public class CorrectiveActionAttachmentDetail
    {
        public Guid CorrectiveActionAttachmentId { get; set; }
        public Guid AttachmentId { get; set; }
        public string ImageUrl { get; set; }
    }

    public class CorrectiveActionAttachmentForCreationViewDto
    {
        public string ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }
}
