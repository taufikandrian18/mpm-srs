using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MPMSRS.Models.Entities;

namespace MPMSRS.Services.Interfaces
{
    public interface IRoleProfileRepository : IUserProfile<Roles>
    {
        Task<IEnumerable<Roles>> GetAllRoles(int pageSize, int pageIndex);
        Task<Roles> GetRoleById(Guid roleId);
        Task<Roles> GetRoleByRoleName(string roleName);
        void CreateRole(Roles role);
        void UpdateRole(Roles role);
        void DeleteRole(Roles role);
    }
}
