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
    public class UserProblemCategoryRepository : UserBase<UserProblemCategoryMappings>, IUserProblemCategoryProfileRepository
    {
        public UserProblemCategoryRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateUserProblemCategory(UserProblemCategoryMappings UserProblemCategoryMapping)
        {
            Create(UserProblemCategoryMapping);
        }

        public void DeleteUserProblemCategoryMappingByProblemCategoryId(List<UserProblemCategoryMappings> listUserProblemCategoryMapping)
        {
            RepositoryContext.UserProblemCategoryMappings.RemoveRange(listUserProblemCategoryMapping);
        }

        public async Task<IEnumerable<UserProblemCategoryMappings>> GetUserProblemCategoryListByProblemCategoryId(Guid problemCategoryId)
        {
            return await FindAll()
                .Where(ow => ow.IsDeleted == false && ow.ProblemCategoryId.Equals(problemCategoryId))
                .ToListAsync();
        }

        public async Task<IEnumerable<UserProblemCategoryMappingListViewDto>> GetUserProblemCategoryListView(Guid problemCategoryId)
        {
            return await (from pcMapping in RepositoryContext.UserProblemCategoryMappings
                          where pcMapping.IsDeleted == false && pcMapping.ProblemCategoryId == problemCategoryId
                          select new UserProblemCategoryMappingListViewDto
                          {
                              EmployeeId = pcMapping.EmployeeId,
                              DisplayName = RepositoryContext.Users.Where(c => c.EmployeeId == pcMapping.EmployeeId).Select(c => c.DisplayName).FirstOrDefault()
                          })
                          .ToListAsync();
        }

        public void UpdateUserProblemCategory(UserProblemCategoryMappings UserProblemCategoryMapping)
        {
            Update(UserProblemCategoryMapping);
        }
    }
}
