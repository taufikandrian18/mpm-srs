using System;
using System.Collections.Generic;
using MPMSRS.Models.Entities;

namespace MPMSRS.Models.VM
{
    public class NetworkDto
    {
        public Guid NetworkId { get; set; }
        public string AccountNum { get; set; }
        public string AhmCode { get; set; }
        public string MDCode { get; set; }
        public string DealerName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string ChannelDealer { get; set; }
        public string DLREmail { get; set; }
        public string KodeKareswil { get; set; }
        public string Karesidenan { get; set; }
        public string NPKSupervisor { get; set; }
        public string NamaSupervisor { get; set; }
        public string Email { get; set; }
        public string IdKaresidenanHC3 { get; set; }
        public string NamaKaresHC3 { get; set; }
        public int NPKSpvHC3 { get; set; }
        public string NamaSPVHC3 { get; set; }
        public int NPKDeptHeadHC3 { get; set; }
        public string NamaDeptHeadHC3 { get; set; }
        public int NPKDivHeadHC3 { get; set; }
        public string NamaDivHeadHC3 { get; set; }
        public string IdKaresidenanTSD { get; set; }
        public string NamaKaresTSD { get; set; }
        public string NPKSpvTSD { get; set; }
        public string NamaSPVTSD { get; set; }
        public string NPKDeptHeadTSD { get; set; }
        public string NamaDeptHeadTSD { get; set; }
        public string NPKDivHeadTSD { get; set; }
        public string NamaDivHeadTSD { get; set; }
        public string NetworkLatitude { get; set; }
        public string NetworkLongitude { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class NetworkDtoViewModel
    {
        public List<Networks> Data { get; set; }
        public PagesViewModel Pages { get; set; }

    }

    public class PagesViewModel
    {
        public int Total { get; set; }
        public int PerPage { get; set; }
        public int Page { get; set; }
        public int LstPage { get; set; }
    }

    public class NetworkForCreationDto
    {
        public string AccountNum { get; set; }
        public string AhmCode { get; set; }
        public string MDCode { get; set; }
        public string DealerName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string ChannelDealer { get; set; }
        public string DLREmail { get; set; }
        public string KodeKareswil { get; set; }
        public string Karesidenan { get; set; }
        public string NPKSupervisor { get; set; }
        public string NamaSupervisor { get; set; }
        public string Email { get; set; }
        public string IdKaresidenanHC3 { get; set; }
        public string NamaKaresHC3 { get; set; }
        public int NPKSpvHC3 { get; set; }
        public string NamaSPVHC3 { get; set; }
        public int NPKDeptHeadHC3 { get; set; }
        public string NamaDeptHeadHC3 { get; set; }
        public int NPKDivHeadHC3 { get; set; }
        public string NamaDivHeadHC3 { get; set; }
        public string IdKaresidenanTSD { get; set; }
        public string NamaKaresTSD { get; set; }
        public string NPKSpvTSD { get; set; }
        public string NamaSPVTSD { get; set; }
        public string NPKDeptHeadTSD { get; set; }
        public string NamaDeptHeadTSD { get; set; }
        public string NPKDivHeadTSD { get; set; }
        public string NamaDivHeadTSD { get; set; }
        public string NetworkLatitude { get; set; }
        public string NetworkLongitude { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class NetworkForUpdateDto
    {
        public Guid NetworkId { get; set; }
        public string AccountNum { get; set; }
        public string AhmCode { get; set; }
        public string MDCode { get; set; }
        public string DealerName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string ChannelDealer { get; set; }
        public string DLREmail { get; set; }
        public string KodeKareswil { get; set; }
        public string Karesidenan { get; set; }
        public string NPKSupervisor { get; set; }
        public string NamaSupervisor { get; set; }
        public string Email { get; set; }
        public string IdKaresidenanHC3 { get; set; }
        public string NamaKaresHC3 { get; set; }
        public int NPKSpvHC3 { get; set; }
        public string NamaSPVHC3 { get; set; }
        public int NPKDeptHeadHC3 { get; set; }
        public string NamaDeptHeadHC3 { get; set; }
        public int NPKDivHeadHC3 { get; set; }
        public string NamaDivHeadHC3 { get; set; }
        public string IdKaresidenanTSD { get; set; }
        public string NamaKaresTSD { get; set; }
        public string NPKSpvTSD { get; set; }
        public string NamaSPVTSD { get; set; }
        public string NPKDeptHeadTSD { get; set; }
        public string NamaDeptHeadTSD { get; set; }
        public string NPKDivHeadTSD { get; set; }
        public string NamaDivHeadTSD { get; set; }
        public string NetworkLatitude { get; set; }
        public string NetworkLongitude { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class NetworkForDeleteDto
    {
        public bool IsDeleted { get; set; }
    }
}
