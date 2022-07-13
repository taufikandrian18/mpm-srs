using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPMSRS.Models.Entities
{
    [Table("TB_Ranks")]
    public class Ranks
    {
        public Ranks()
        {
        }
        [Key]
        public Guid RankId { get; set; }
        [StringLength(50)]
        public string UserBOD { get; set; }
        [StringLength(50)]
        public string UserBODName { get; set; }
        [StringLength(50)]
        public string UserGM { get; set; }
        [StringLength(50)]
        public string UserGMName { get; set; }
        [StringLength(50)]
        public string UserManager { get; set; }
        [StringLength(50)]
        public string UserManagerName { get; set; }
        [StringLength(50)]
        public string UserStaff { get; set; }
        [StringLength(50)]
        public string UserStaffName { get; set; }
        [StringLength(150)]
        public string DivisionName { get; set; }
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
