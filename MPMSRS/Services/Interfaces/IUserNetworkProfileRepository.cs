using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MPMSRS.Models.Entities;
using MPMSRS.Models.VM;

namespace MPMSRS.Services.Interfaces
{
    public interface IUserNetworkProfileRepository : IUserProfile<UserNetworkMappings>
    {
        Task<IEnumerable<UserNetworkMappings>> GetAllUserNetworkMappings(int pageSize, int pageIndex);
        Task<UserNetworkMappings> GetUserNetworkById(Guid userNetworkId);
        Task<UserNetworkMappings> GetUserNetworkByUserId(Guid employeeId);
        Task<UserNetworkMappings> GetUserNetworkByNetworkId(Guid networkId, Guid employeeId);
        Task<IEnumerable<UserNetworkMappings>> GetUserNetworkByEmployeeId(Guid employeeId, int pageSize, int pageIndex);
        Task<NetworkDtoViewModel> GetNetworksByEmployeeId(Guid EmployeeId, int pageSize, int pageIndex, string area, string query);
        Task<IEnumerable<UserNetworkMappingDtoViewModel>> GetUserNetworkObjectByEmpId(Guid employeeId, int pageSize, int pageIndex);
        void CreateUserNetwork(UserNetworkMappings UserNetworkMapping);
        void UpdateUserNetwork(UserNetworkMappings UserNetworkMapping);
        void DeleteUserNetwork(UserNetworkMappings UserNetworkMapping);
    }
}
