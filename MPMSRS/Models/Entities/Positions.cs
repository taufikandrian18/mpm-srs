using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPMSRS.Models.Entities
{
    [Table("TB_Positions")]
    public class Positions
    {
        public Positions()
        {
        }
        [Key]
        public Guid PositionId { get; set; }
        [StringLength(2)]
        public string Channel { get; set; }
        [Required]
        public int KodeJabatan { get; set; }
        [Required]
        [StringLength(15)]
        public string NamaJabatan { get; set; }
        [StringLength(11)]
        public string GroupJabatanId { get; set; }
        [StringLength(15)]
        public string NamaGroupPosition { get; set; }
        public DateTime CreatedAt { get; set; }
        [Required]
        [StringLength(64)]
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        [Required]
        [StringLength(64)]
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
