using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MPMSRS.Models.Entities;
using MPMSRS.Models.VM;

namespace MPMSRS.Services.Interfaces
{
    public interface IUserProfileRepository : IUserProfile<Users>
    {
        Task<IEnumerable<Users>> GetAllUsers(int pageSize, int pageIndex);
        Task<Users> GetUserById(Guid employeeId);
        Task<UserDtoViewModel> GetUserByIdDetail(Guid employeeId);
        Task<Users> GetUserByUsername(string username);
        Task<UserDto> GetUserByUsernameLogin(string username);
        Task<UserDto> GetUserByUsernameLoginDealer(string username);
        Task<Guid> GetUserIdByUsername(string username);
        Task<string> GetCompanyIdByEmployeeId(Guid employeeId);
        Task<UserDto> GetUserByIdWithFullField(Guid employeeId);
        Task<IEnumerable<Users>> GetUserByRole(Guid roleId, int pageSize, int pageIndex);
        Task<IEnumerable<Users>> GetUserByRoleIdAndDivisionId(Guid roleId, Guid divisionId, int pageSize, int pageIndex);
        Task<IEnumerable<Users>> GetUserByCompanyId(string companyId);
        void CreateUser(Users user);
        void UpdateUser(Users user);
        void DeleteUser(Users user);
    }
}
