using System;
namespace MPMSRS.Models.VM
{
    public class PositionDto
    {
        public Guid PositionId { get; set; }
        public string Channel { get; set; }
        public int KodeJabatan { get; set; }
        public string NamaJabatan { get; set; }
        public string GroupJabatanId { get; set; }
        public string NamaGroupPosition { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class PositionCreationForDto
    {
        public string Channel { get; set; }
        public int KodeJabatan { get; set; }
        public string NamaJabatan { get; set; }
        public string GroupJabatanId { get; set; }
        public string NamaGroupPosition { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class PositionUpdateForDto
    {
        public Guid PositionId { get; set; }
        public string Channel { get; set; }
        public int KodeJabatan { get; set; }
        public string NamaJabatan { get; set; }
        public string GroupJabatanId { get; set; }
        public string NamaGroupPosition { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class PositionDeleteForDto
    {
        public bool IsDeleted { get; set; }
    }
}
