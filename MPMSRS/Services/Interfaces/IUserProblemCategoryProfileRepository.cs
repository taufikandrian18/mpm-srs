using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MPMSRS.Models.Entities;
using MPMSRS.Models.VM;

namespace MPMSRS.Services.Interfaces
{
    public interface IUserProblemCategoryProfileRepository : IUserProfile<UserProblemCategoryMappings>
    {
        Task<IEnumerable<UserProblemCategoryMappings>> GetUserProblemCategoryListByProblemCategoryId(Guid problemCategoryId);
        Task<IEnumerable<UserProblemCategoryMappingListViewDto>> GetUserProblemCategoryListView(Guid problemCategoryId);
        void CreateUserProblemCategory(UserProblemCategoryMappings UserProblemCategoryMapping);
        void UpdateUserProblemCategory(UserProblemCategoryMappings UserProblemCategoryMapping);
        void DeleteUserProblemCategoryMappingByProblemCategoryId(List<UserProblemCategoryMappings> listUserProblemCategoryMapping);
    }
}
