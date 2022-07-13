using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPMSRS.Models.Entities
{
    [Table("TB_Networks")]
    public class Networks
    {
        public Networks()
        {
            Assignments = new HashSet<Assignments>();
            Visitings = new HashSet<Visitings>();
            UserNetworkMappings = new HashSet<UserNetworkMappings>();
            VisitingDetailReports = new HashSet<VisitingDetailReports>();
            Checklists = new HashSet<Checklists>();
        }
        [Key]
        public Guid NetworkId { get; set; }
        [Required]
        [StringLength(10)]
        public string AccountNum { get; set; }
        [Required]
        [StringLength(5)]
        public string AhmCode { get; set; }
        [Required]
        [StringLength(5)]
        public string MDCode { get; set; }
        [Required]
        [StringLength(36)]
        public string DealerName { get; set; }
        [Required]
        [StringLength(255)]
        public string Address { get; set; }
        [StringLength(20)]
        public string City { get; set; }
        [Required]
        [StringLength(2)]
        public string ChannelDealer { get; set; }
        [StringLength(255)]
        public string DLREmail { get; set; }
        [StringLength(4)]
        public string KodeKareswil { get; set; }
        [StringLength(11)]
        public string Karesidenan { get; set; }
        [StringLength(5)]
        public string NPKSupervisor { get; set; }
        [StringLength(25)]
        public string NamaSupervisor { get; set; }
        [StringLength(33)]
        public string Email { get; set; }
        [StringLength(14)]
        public string IdKaresidenanHC3 { get; set; }
        [StringLength(25)]
        public string NamaKaresHC3 { get; set; }
        public int NPKSpvHC3 { get; set; }
        [StringLength(23)]
        public string NamaSPVHC3 { get; set; }
        public int NPKDeptHeadHC3 { get; set; }
        [StringLength(17)]
        public string NamaDeptHeadHC3 { get; set; }
        public int NPKDivHeadHC3 { get; set; }
        [StringLength(24)]
        public string NamaDivHeadHC3 { get; set; }
        [StringLength(30)]
        public string IdKaresidenanTSD { get; set; }
        [StringLength(30)]
        public string NamaKaresTSD { get; set; }
        [StringLength(30)]
        public string NPKSpvTSD { get; set; }
        [StringLength(30)]
        public string NamaSPVTSD { get; set; }
        [StringLength(30)]
        public string NPKDeptHeadTSD { get; set; }
        [StringLength(30)]
        public string NamaDeptHeadTSD { get; set; }
        [StringLength(30)]
        public string NPKDivHeadTSD { get; set; }
        [StringLength(30)]
        public string NamaDivHeadTSD { get; set; }
        [StringLength(255)]
        public string NetworkLatitude { get; set; }
        [StringLength(255)]
        public string NetworkLongitude { get; set; }
        public DateTime CreatedAt { get; set; }
        [Required]
        [StringLength(64)]
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        [Required]
        [StringLength(64)]
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }

        [InverseProperty("Network")]
        public virtual ICollection<Assignments> Assignments { get; set; }

        [InverseProperty("Network")]
        public virtual ICollection<Visitings> Visitings { get; set; }

        [InverseProperty("Network")]
        public virtual ICollection<UserNetworkMappings> UserNetworkMappings { get; set; }

        [InverseProperty("Network")]
        public virtual ICollection<VisitingDetailReports> VisitingDetailReports { get; set; }

        [InverseProperty("Network")]
        public virtual ICollection<Checklists> Checklists { get; set; }

    }
}
