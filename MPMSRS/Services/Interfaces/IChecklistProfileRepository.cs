using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MPMSRS.Models.Entities;
using MPMSRS.Models.VM;

namespace MPMSRS.Services.Interfaces
{
    public interface IChecklistProfileRepository : IUserProfile<Checklists>
    {
        Task<ChecklistDtoLoginViewModel> GetAllChecklists(Guid visitingId, int pageSize, int pageIndex);
        Task<ChecklistIdentificationModel> GetChecklistIdentification(Guid visitingId, int pageSize, int pageIndex);
        Task<Checklists> GetChecklistById(Guid checklistId);
        Task<IEnumerable<Checklists>> GetChecklistByVisitingIdWithoutPage(Guid visitingId);
        Task<ChecklistViewModel> GetChecklistByChecklistId(Guid checklistId);
        Task<string> GetCreatedByName(Guid employeeId);
        Task<string> GetAttachmentUrl(Guid attachmentId);
        void UpdateBulkChecklist(List<Checklists> checklists);
        void CreateChecklist(Checklists checklist);
        void UpdateChecklist(Checklists checklist);
        void DeleteChecklist(Checklists checklist);
    }
}
