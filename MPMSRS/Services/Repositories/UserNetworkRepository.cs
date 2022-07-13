using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MPMSRS.Models.Entities;
using MPMSRS.Models.VM;
using MPMSRS.Services.Interfaces;

namespace MPMSRS.Services.Repositories
{
    public class UserNetworkRepository : UserBase<UserNetworkMappings>, IUserNetworkProfileRepository
    {
        public UserNetworkRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateUserNetwork(UserNetworkMappings UserNetworkMapping)
        {
            Create(UserNetworkMapping);
        }

        public void CreateUserNetworkBulk(List<UserNetworkMappings> UserNetworkMappings)
        {
            CreateBulk(UserNetworkMappings);
        }

        public void DeleteUserNetwork(UserNetworkMappings UserNetworkMapping)
        {
            Update(UserNetworkMapping);
        }

        public async Task<IEnumerable<UserNetworkMappings>> GetAllUserNetworkMappings(int pageSize, int pageIndex)
        {
            return await FindAll()
                .Where(ow => ow.IsDeleted == false)
                .OrderByDescending(ow => ow.CreatedAt)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<NetworkDtoViewModel> GetNetworksByEmployeeId(Guid EmployeeId, int pageSize, int pageIndex, string area, string query)
        {
            var lstNetwork = FindAll()
                .Where(ow => ow.IsDeleted == false && ow.EmployeeId == EmployeeId)
                .Select(ow => ow.Network)
                .OrderByDescending(ow => ow.CreatedAt)
                .AsNoTracking()
                .AsQueryable();

            if(!string.IsNullOrEmpty(area))
            {
                lstNetwork = lstNetwork.Where(Q => Q.City.ToLower() == area.ToLower());
            }

            if (!string.IsNullOrEmpty(query))
            {
                lstNetwork = lstNetwork.Where(Q => Q.AhmCode.ToLower().Contains(query.ToLower()) || Q.MDCode.ToLower().Contains(query.ToLower()) || Q.DealerName.ToLower().Contains(query.ToLower()) || Q.Address.ToLower().Contains(query.ToLower()));
            }

            PagesViewModel pages = new PagesViewModel();
            pages.Total = lstNetwork.Count();
            pages.PerPage = pageSize;
            pages.Page = pageIndex;
            pages.LstPage = pages.Total / pageSize;

            var data = await lstNetwork.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            return new NetworkDtoViewModel
            {
                Data = data,
                Pages = pages
            };
        }

        public async Task<IEnumerable<UserNetworkMappings>> GetUserNetworkByEmployeeId(Guid employeeId, int pageSize, int pageIndex)
        {
            return await FindAll()
                .Where(ow => ow.IsDeleted == false && ow.EmployeeId == employeeId)
                .OrderByDescending(ow => ow.CreatedAt)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<UserNetworkMappings> GetUserNetworkById(Guid userNetworkId)
        {
            return await FindByCondition(un => un.UserNetworkMappingId.Equals(userNetworkId))
            .Where(un => un.IsDeleted == false)
            .FirstOrDefaultAsync();
        }

        public async Task<UserNetworkMappings> GetUserNetworkByNetworkId(Guid networkId, Guid employeeId)
        {
            return await FindByCondition(un => un.NetworkId.Equals(networkId) && un.EmployeeId.Equals(employeeId))
            .Where(un => un.IsDeleted == false)
            .FirstOrDefaultAsync();
        }

        public async Task<UserNetworkMappings> GetUserNetworkByUserId(Guid employeeId)
        {
            return await FindByCondition(un => un.EmployeeId.Equals(employeeId))
            .Where(un => un.IsDeleted == false)
            .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<UserNetworkMappingDtoViewModel>> GetUserNetworkObjectByEmpId(Guid employeeId, int pageSize, int pageIndex)
        {
            return await RepositoryContext.UserNetworkMappings
                .Where(ow => ow.EmployeeId == employeeId && ow.IsDeleted == false)
                .Join(RepositoryContext.Networks,
                u => u.NetworkId,
                n => n.NetworkId,
                (u, n) => new { u, n })
                .Select((x) => new UserNetworkMappingDtoViewModel
                {
                    UserNetworkMappingId = x.u.UserNetworkMappingId,
                    Network = x.u.Network
                }).ToListAsync();
        }

        public void UpdateUserNetwork(UserNetworkMappings UserNetworkMapping)
        {
            Update(UserNetworkMapping);
        }
    }
}
