using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MPMSRS.Models.Entities;

namespace MPMSRS.Services.Interfaces
{
    public interface IChecklistPICProfileRepository : IUserProfile<ChecklistPICs>
    {
        void CreateChecklistPIC(ChecklistPICs checklistPIC);
        Task<IEnumerable<ChecklistPICs>> GetChecklistPICByChecklistIdWithoutPage(Guid checklistId);
        void DeleteChecklistPICByChecklistId(List<ChecklistPICs> listChecklistPICs);
    }
}
