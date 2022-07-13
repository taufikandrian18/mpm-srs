using System;
using System.Collections.Generic;
using MPMSRS.Models.Entities;

namespace MPMSRS.Models.VM
{
    public class VisitingDto
    {
        public Guid VisitingId { get; set; }
        public Guid NetworkId { get; set; }
        public Guid VisitingTypeId { get; set; }
        public string VisitingComment { get; set; }
        public string VisitingStatus { get; set; }
        public string VisitingCommentByManager { get; set; }
        public string ApprovedByManager { get; set; }
        public string ApprovedByGM { get; set; }
        public bool IsOnline { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class VisitingDtoViewModel
    {
        public Guid VisitingId { get; set; }
        public Networks Network { get; set; }
        public Guid VisitingTypeId { get; set; }
        public string VisitingComment { get; set; }
        public string VisitingCommentByManager { get; set; }
        public string VisitingStatus { get; set; }
        public string ApprovedByManager { get; set; }
        public string ApprovedByGM { get; set; }
        public bool IsOnline { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }

        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class VisitingDtoViewDetailModel
    {
        public Guid VisitingId { get; set; }
        public Networks Network { get; set; }
        public List<EmployeeListInVisitingPeople> VisitingPeoples { get; set; }
        public Guid VisitingTypeId { get; set; }
        public string VisitingTypeName { get; set; }
        public string VisitingComment { get; set; }
        public string VisitingCommentByManager { get; set; }
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

    public class EmployeeListInVisitingPeople
    {
        public Guid EmployeeId { get; set; }
        public string EmployeeName { get; set; }
    }

    public class VisitingDtoLoginViewModel
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
        public List<EmployeeListInVisitingPeople> VisitingPeoples { get; set; }
        public string VisitingComment { get; set; }
        public string VisitingCommentByManager { get; set; }
        public string VisitingStatus { get; set; }
        public string ApprovedByManager { get; set; }
        public string ApprovedByGM { get; set; }
        public bool IsOnline { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class VisitingReportDtoLoginViewModel
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
        public List<EmployeeListInVisitingPeople> VisitingPeoples { get; set; }
        public string VisitingComment { get; set; }
        public string VisitingCommentByManager { get; set; }
        public string VisitingStatus { get; set; }
        public string ApprovedByManager { get; set; }
        public string ApprovedByGM { get; set; }
        public bool IsOnline { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public string JenisVisitingSingleOrJoin { get; set; }
    }

    public class VisitingDtoLoginViewAllModel
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
        public List<EmployeeListInVisitingPeople> VisitingPeoples { get; set; }
        public string VisitingComment { get; set; }
        public string VisitingCommentByManager { get; set; }
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

    public class VisitingDetailExportExcelDtoViewModel
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
        public List<VisitingDetailReportExportExcelDto> VisitingDetailReport { get; set; }
        public string VisitingComment { get; set; }
        public string VisitingCommentByManager { get; set; }
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
    }

    public class VisitingDetailDtoLoginViewModel
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
        public List<EmployeeListInVisitingPeople> VisitingPeoples { get; set; }
        public List<VisitingNoteMappings> VisitingNoteMapping { get; set; }
        public List<VisitingDetailReportViewModel> VisitingDetailReport { get; set; }
        public string VisitingComment { get; set; }
        public string VisitingCommentByManager { get; set; }
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
    }

    public class VisitingForCreationDto
    {
        public Guid NetworkId { get; set; }
        public string[] EmployeeId { get; set; }
        public Guid VisitingTypeId { get; set; }
        public string VisitingComment { get; set; }
        public string VisitingCommentByManager { get; set; }
        public string VisitingStatus { get; set; }
        public string ApprovedByManager { get; set; }
        public string ApprovedByGM { get; set; }
        public bool IsOnline { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class VisitingObjectForApprovedDto
    {
        public Guid VisitingId { get; set; }
        public string VisitingStatus { get; set; }
        public string ApprovedByGM { get; set; }
    }

    public class VisitingForApprovedBulkDto
    {
        public List<VisitingObjectForApprovedDto> ListVisitingUpdateBulk { get; set; }
    }

    public class VisitingForUpdateDto
    {
        public Guid VisitingId { get; set; }
        public Guid NetworkId { get; set; }
        public string[] EmployeeId { get; set; }
        public Guid VisitingTypeId { get; set; }
        public string VisitingComment { get; set; }
        public string VisitingCommentByManager { get; set; }
        public string VisitingStatus { get; set; }
        public string ApprovedByManager { get; set; }
        public string ApprovedByGM { get; set; }
        public bool IsOnline { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class VisitingForUpdateApprovalDto
    {
        public Guid VisitingId { get; set; }
        public string VisitingComment { get; set; }
        public string VisitingCommentByManager { get; set; }
        public string VisitingStatus { get; set; }
        public string ApprovedByManager { get; set; }
        public string ApprovedByGM { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class VisitingForDeleteDto
    {
        public bool IsDeleted { get; set; }
    }
}
