using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MPMSRS.Models.Entities;
using MPMSRS.Services.Interfaces;

namespace MPMSRS.Services.Repositories
{
    public class RoleRepository : UserBase<Roles>, IRoleProfileRepository
    {
        public RoleRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateRole(Roles role)
        {
            Create(role);
        }

        public void DeleteRole(Roles role)
        {
            Update(role);
        }

        public async Task<IEnumerable<Roles>> GetAllRoles(int pageSize, int pageIndex)
        {
            return await FindAll()
                .Where(ow => ow.IsDeleted == false)
                .OrderBy(ow => ow.RoleName)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Roles> GetRoleById(Guid roleId)
        {
            return await FindByCondition(role => role.RoleId.Equals(roleId))
            .Where(role => role.IsDeleted == false)
            .FirstOrDefaultAsync();
        }

        public async Task<Roles> GetRoleByRoleName(string roleName)
        {
            return await FindByCondition(role => role.RoleName.ToLower() == roleName.ToLower())
            .Where(role => role.IsDeleted == false)
            .FirstOrDefaultAsync();
        }

        public void UpdateRole(Roles role)
        {
            Update(role);
        }
    }
}
