using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MPMSRS.Models.Entities;
using MPMSRS.Services.Interfaces;

namespace MPMSRS.Services.Repositories
{
    public class ChecklistEvidenceRepository : UserBase<ChecklistEvidences>, IChecklistEvidenceProfileRepository
    {
        public ChecklistEvidenceRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateChecklistEvidence(ChecklistEvidences checklistEvidence)
        {
            Create(checklistEvidence);
        }

        public void DeleteChecklistEvidenceByChecklistId(List<ChecklistEvidences> listChecklistEvidences)
        {
            RepositoryContext.ChecklistEvidences.RemoveRange(listChecklistEvidences);
        }

        public async Task<IEnumerable<ChecklistEvidences>> GetChecklistEvidenceByChecklistIdWithoutPage(Guid checklistId)
        {
            return await FindAll()
                .Where(ow => ow.IsDeleted == false && ow.ChecklistId == checklistId)
                .OrderByDescending(ow => ow.CreatedAt)
                .ToListAsync();
        }
    }
}
