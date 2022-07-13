using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MPMSRS.Models.Entities;

namespace MPMSRS.Services.Interfaces
{
    public interface IChecklistEvidenceProfileRepository : IUserProfile<ChecklistEvidences>
    {
        void CreateChecklistEvidence(ChecklistEvidences checklistEvidence);
        Task<IEnumerable<ChecklistEvidences>> GetChecklistEvidenceByChecklistIdWithoutPage(Guid checklistId);
        void DeleteChecklistEvidenceByChecklistId(List<ChecklistEvidences> listChecklistEvidences);
    }
}
