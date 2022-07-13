using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MPMSRS.Models.Entities;

namespace MPMSRS.Services.Interfaces
{
    public interface IProblemCategoryProfileRepository : IUserProfile<ProblemCategories>
    {
        Task<IEnumerable<ProblemCategories>> GetAllProblemCategories(int pageSize, int pageIndex);
        Task<ProblemCategories> GetProblemCategoryById(Guid problemCategoryId);
        Task<ProblemCategories> GetProblemCategoryByUsername(string problemCategoryName);
        Task<string> GetProblemCategoryNameById(Guid problemCategoryId);
        Task<Guid> GetProblemCategoryIdByProblemCategoryName(string problemCategoryName);
        void CreateProblemCategory(ProblemCategories problemCategories);
        void UpdateProblemCategory(ProblemCategories problemCategories);
        void DeleteProblemCategory(ProblemCategories problemCategories);
    }
}
