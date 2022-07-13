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
    public class UserRepository : UserBase<Users>, IUserProfileRepository
    {
        public UserRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateUser(Users user)
        {
            Create(user);
        }

        public async Task<IEnumerable<Users>> GetAllUsers(int pageSize, int pageIndex)
        {
            return await FindAll()
                .Where(ow => ow.IsDeleted == false)
                .OrderBy(ow => ow.DisplayName)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Users> GetUserByUsername(string username)
        {
            return await FindByCondition(user => user.Username.ToLower().Contains(username.ToLower()))
            .Where(user => user.IsDeleted == false)
            .FirstOrDefaultAsync();
        }

        public async Task<Users> GetUserById(Guid employeeId)
        {
            return await FindByCondition(user => user.EmployeeId.Equals(employeeId))
            .Where(user => user.IsDeleted == false)
            .FirstOrDefaultAsync();
        }

        public async Task<UserDtoViewModel> GetUserByIdDetail(Guid employeeId)
        {
            return await (from u in RepositoryContext.Users
                          join a in RepositoryContext.Attachments on u.AttachmentId equals a.AttachmentId into grpatt
                          from a in grpatt.DefaultIfEmpty()
                          join r in RepositoryContext.Roles on u.RoleId equals r.RoleId into grprole
                          from r in grprole.DefaultIfEmpty()
                          join d in RepositoryContext.Divisions on u.DivisionId equals d.DivisionId into grpdiv
                          from d in grpdiv.DefaultIfEmpty()
                          where u.IsDeleted == false && u.EmployeeId == employeeId
                          select new UserDtoViewModel
                          {
                              EmployeeId = u.EmployeeId,
                              AttachmentId = u.AttachmentId,
                              AttachmentUrl = a.AttachmentUrl,
                              CompanyId = u.CompanyId,
                              Username = u.Username,
                              Password = u.Password,
                              WorkLocation = u.WorkLocation,
                              DisplayName = u.DisplayName,
                              Department = u.Department,
                              Phone = u.Phone,
                              Email = u.Email,
                              InternalTitle = u.InternalTitle,
                              CreatedAt = u.CreatedAt,
                              CreatedBy = u.CreatedBy,
                              UpdatedAt = u.UpdatedAt,
                              UpdatedBy = u.UpdatedBy,
                              RoleId = u.RoleId,
                              RoleName = r.RoleName,
                              DivisionId = u.DivisionId,
                              DivisionName = d.DivisionName
                          }).FirstOrDefaultAsync();
        }

        public async Task<Guid> GetUserIdByUsername(string username)
        {
            return await FindByCondition(user => user.Username.Trim().ToLower().Equals(username.Trim().ToLower()))
            .Where(user => user.IsDeleted == false)
            .Select(user => user.EmployeeId)
            .FirstOrDefaultAsync();
        }

        public void UpdateUser(Users user)
        {
            Update(user);
        }

        public void DeleteUser(Users user)
        {
            Update(user);
        }

        public async Task<IEnumerable<Users>> GetUserByRole(Guid roleId, int pageSize, int pageIndex)
        {
            return await FindByCondition(user => user.RoleId.Equals(roleId))
            .Where(user => user.IsDeleted == false)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        }

        public async Task<UserDto> GetUserByUsernameLoginDealer(string username)
        {
            //return FindByCondition(user => user.Username.Equals(username))
            //.Where(user => user.IsDeleted == false)
            //.FirstOrDefault();

            return await RepositoryContext.Users
                .Where(Q => Q.CompanyId.Equals(username) && Q.IsDeleted == false)
                .GroupJoin(
                RepositoryContext.Roles,
                u => u.RoleId,
                r => r.RoleId,
                (u, r) => new { u, r })
                .SelectMany(y => y.r.DefaultIfEmpty(), (y, rol) => new { y, rol })
                .GroupJoin(
                RepositoryContext.Divisions,
                us => us.y.u.DivisionId,
                d => d.DivisionId,
                (us, d) => new { us, d })
                .SelectMany(x => x.d.DefaultIfEmpty(), (x, c) => new UserDto
                {
                    EmployeeId = x.us.y.u.EmployeeId,
                    AttachmentId = x.us.y.u.AttachmentId,
                    CompanyId = x.us.y.u.CompanyId,
                    Username = x.us.y.u.Username,
                    Password = x.us.y.u.Password,
                    WorkLocation = x.us.y.u.WorkLocation,
                    DisplayName = x.us.y.u.DisplayName,
                    Department = x.us.y.u.Department,
                    Phone = x.us.y.u.Phone,
                    Email = x.us.y.u.Email,
                    InternalTitle = x.us.y.u.InternalTitle,
                    CreatedAt = x.us.y.u.CreatedAt,
                    CreatedBy = x.us.y.u.CreatedBy,
                    UpdatedAt = x.us.y.u.UpdatedAt,
                    UpdatedBy = x.us.y.u.UpdatedBy,
                    RoleId = x.us.y.u.RoleId,
                    RoleName = x.us.rol.RoleName,
                    DivisionId = x.us.y.u.DivisionId,
                    DivisionName = c.DivisionName
                }).FirstOrDefaultAsync();

        }

        public async Task<UserDto> GetUserByUsernameLogin(string username)
        {
            //return FindByCondition(user => user.Username.Equals(username))
            //.Where(user => user.IsDeleted == false)
            //.FirstOrDefault();

            return await RepositoryContext.Users
                .Where(Q => Q.Username.Equals(username) && Q.IsDeleted == false)
                .GroupJoin(
                RepositoryContext.Roles,
                u => u.RoleId,
                r => r.RoleId,
                (u, r) => new { u,r })
                .SelectMany(y => y.r.DefaultIfEmpty(), (y, rol) => new { y, rol })
                .GroupJoin(
                RepositoryContext.Divisions,
                us => us.y.u.DivisionId,
                d => d.DivisionId,
                (us, d) => new { us, d })
                .SelectMany(x => x.d.DefaultIfEmpty(), (x, c) => new UserDto
                {
                    EmployeeId = x.us.y.u.EmployeeId,
                    AttachmentId = x.us.y.u.AttachmentId,
                    CompanyId = x.us.y.u.CompanyId,
                    Username = x.us.y.u.Username,
                    Password = x.us.y.u.Password,
                    WorkLocation = x.us.y.u.WorkLocation,
                    DisplayName = x.us.y.u.DisplayName,
                    Department = x.us.y.u.Department,
                    Phone = x.us.y.u.Phone,
                    Email = x.us.y.u.Email,
                    InternalTitle = x.us.y.u.InternalTitle,
                    CreatedAt = x.us.y.u.CreatedAt,
                    CreatedBy = x.us.y.u.CreatedBy,
                    UpdatedAt = x.us.y.u.UpdatedAt,
                    UpdatedBy = x.us.y.u.UpdatedBy,
                    RoleId = x.us.y.u.RoleId,
                    RoleName = x.us.rol.RoleName,
                    DivisionId = x.us.y.u.DivisionId,
                    DivisionName = c.DivisionName
                }).FirstOrDefaultAsync();

        }

        public async Task<IEnumerable<Users>> GetUserByRoleIdAndDivisionId(Guid roleId, Guid divisionId, int pageSize, int pageIndex)
        {
            return await FindByCondition(user => user.RoleId.Equals(roleId) && user.DivisionId.Equals(divisionId))
                .Where(user => user.IsDeleted == false)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<UserDto> GetUserByIdWithFullField(Guid employeeId)
        {
            return await RepositoryContext.Users
                .Where(Q => Q.EmployeeId.Equals(employeeId) && Q.IsDeleted == false)
                .GroupJoin(
                RepositoryContext.Roles,
                u => u.RoleId,
                r => r.RoleId,
                (u, r) => new { u, r })
                .SelectMany(y => y.r.DefaultIfEmpty(), (y, rol) => new { y, rol })
                .GroupJoin(
                RepositoryContext.Divisions,
                us => us.y.u.DivisionId,
                d => d.DivisionId,
                (us, d) => new { us, d })
                .SelectMany(x => x.d.DefaultIfEmpty(), (x, c) => new UserDto
                {
                    EmployeeId = x.us.y.u.EmployeeId,
                    AttachmentId = x.us.y.u.AttachmentId,
                    CompanyId = x.us.y.u.CompanyId,
                    Username = x.us.y.u.Username,
                    Password = x.us.y.u.Password,
                    WorkLocation = x.us.y.u.WorkLocation,
                    DisplayName = x.us.y.u.DisplayName,
                    Department = x.us.y.u.Department,
                    Phone = x.us.y.u.Phone,
                    Email = x.us.y.u.Email,
                    InternalTitle = x.us.y.u.InternalTitle,
                    CreatedAt = x.us.y.u.CreatedAt,
                    CreatedBy = x.us.y.u.CreatedBy,
                    UpdatedAt = x.us.y.u.UpdatedAt,
                    UpdatedBy = x.us.y.u.UpdatedBy,
                    RoleId = x.us.y.u.RoleId,
                    RoleName = x.us.rol.RoleName,
                    DivisionId = x.us.y.u.DivisionId,
                    DivisionName = c.DivisionName
                }).FirstOrDefaultAsync();
        }

        public async Task<string> GetCompanyIdByEmployeeId(Guid employeeId)
        {
            return await FindByCondition(user => user.EmployeeId.Equals(employeeId))
                        .Where(user => user.IsDeleted == false)
                        .Select(user => user.CompanyId)
                        .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Users>> GetUserByCompanyId(string companyId)
        {
            return await FindAll()
                .Where(ow => ow.IsDeleted == false && ow.CompanyId.ToLower().Contains(companyId.ToLower()))
                .OrderBy(ow => ow.DisplayName)
                .ToListAsync();
        }
    }
}
