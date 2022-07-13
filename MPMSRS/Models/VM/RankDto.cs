using System;
namespace MPMSRS.Models.VM
{
    public class RankDto
    {
        public Guid RankId { get; set; }
        public string UserBOD { get; set; }
        public string UserBODName { get; set; }
        public string UserGM { get; set; }
        public string UserGMName { get; set; }
        public string UserManager { get; set; }
        public string UserManagerName { get; set; }
        public string UserStaff { get; set; }
        public string UserStaffName { get; set; }
        public string DivisionName { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class RankForCreationDto
    {
        public string UserBOD { get; set; }
        public string UserBODName { get; set; }
        public string UserGM { get; set; }
        public string UserGMName { get; set; }
        public string UserManager { get; set; }
        public string UserManagerName { get; set; }
        public string UserStaff { get; set; }
        public string UserStaffName { get; set; }
        public string DivisionName { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class RankForUpdateDto
    {
        public Guid RankId { get; set; }
        public string UserBOD { get; set; }
        public string UserBODName { get; set; }
        public string UserGM { get; set; }
        public string UserGMName { get; set; }
        public string UserManager { get; set; }
        public string UserManagerName { get; set; }
        public string UserStaff { get; set; }
        public string UserStaffName { get; set; }
        public string DivisionName { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class RankForDeletionDto
    {
        public bool IsDeleted { get; set; }
    }
}
