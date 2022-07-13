using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MPMSRS.Models.Entities;
using MPMSRS.Services.Interfaces;

namespace MPMSRS.Services.Repositories
{
    public class ProblemCategoryRepository : UserBase<ProblemCategories>, IProblemCategoryProfileRepository
    {
        public ProblemCategoryRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateProblemCategory(ProblemCategories problemCategories)
        {
            Create(problemCategories);
        }

        public void DeleteProblemCategory(ProblemCategories problemCategories)
        {
            Update(problemCategories);
        }

        public async Task<IEnumerable<ProblemCategories>> GetAllProblemCategories(int pageSize, int pageIndex)
        {
            return await FindAll()
                .Where(ow => ow.IsDeleted == false)
                .OrderBy(ow => ow.ProblemCategoryName)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<ProblemCategories> GetProblemCategoryById(Guid problemCategoryId)
        {
            return await FindByCondition(pc => pc.ProblemCategoryId.Equals(problemCategoryId))
            .Where(pc => pc.IsDeleted == false)
            .FirstOrDefaultAsync();
        }

        public async Task<ProblemCategories> GetProblemCategoryByUsername(string problemCategoryName)
        {
            return await FindByCondition(pc => pc.ProblemCategoryName.ToLower().Contains(problemCategoryName.ToLower()))
            .Where(pc => pc.IsDeleted == false)
            .FirstOrDefaultAsync();
        }

        public async Task<Guid> GetProblemCategoryIdByProblemCategoryName(string problemCategoryName)
        {
            return await FindByCondition(pc => pc.ProblemCategoryName.Trim().ToLower().Equals(problemCategoryName.Trim().ToLower()))
            .Where(pc => pc.IsDeleted == false)
            .Select(pc => pc.ProblemCategoryId)
            .FirstOrDefaultAsync();
        }

        public async Task<string> GetProblemCategoryNameById(Guid problemCategoryId)
        {
            return await FindByCondition(pc => pc.ProblemCategoryId.Equals(problemCategoryId))
            .Where(pc => pc.IsDeleted == false)
            .Select(pc => pc.ProblemCategoryName)
            .FirstOrDefaultAsync();
        }

        public void UpdateProblemCategory(ProblemCategories problemCategories)
        {
            Update(problemCategories);
        }
    }
}
