using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MPMSRS.Models.Entities;
using MPMSRS.Services.Interfaces;

namespace MPMSRS.Services.Repositories
{
    public class ChecklistPICRepository : UserBase<ChecklistPICs>, IChecklistPICProfileRepository
    {
        public ChecklistPICRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateChecklistPIC(ChecklistPICs checklistPIC)
        {
            Create(checklistPIC);
        }

        public void DeleteChecklistPICByChecklistId(List<ChecklistPICs> listChecklistPICs)
        {
            RepositoryContext.ChecklistPICs.RemoveRange(listChecklistPICs);
        }

        public async Task<IEnumerable<ChecklistPICs>> GetChecklistPICByChecklistIdWithoutPage(Guid checklistId)
        {
            return await FindAll()
                .Where(ow => ow.IsDeleted == false && ow.ChecklistId == checklistId)
                .OrderByDescending(ow => ow.CreatedAt)
                .ToListAsync();
        }
    }
}
