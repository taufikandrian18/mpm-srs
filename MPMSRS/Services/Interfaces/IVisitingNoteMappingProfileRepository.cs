using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MPMSRS.Models.Entities;

namespace MPMSRS.Services.Interfaces
{
    public interface IVisitingNoteMappingProfileRepository : IUserProfile<VisitingNoteMappings>
    {
        Task<IEnumerable<VisitingNoteMappings>> GetAllVisitingNoteMappings(int pageSize, int pageIndex);
        Task<VisitingNoteMappings> GetVisitingNoteMappingById(Guid visitingNoteMappingId);
        Task<VisitingNoteMappings> GetVisitingNoteMappingByUserId(Guid employeeId);
        Task<IEnumerable<VisitingNoteMappings>> GetVisitingNoteMappingByVisitingId(Guid visitingId, int pageSize, int pageIndex);
        void CreateVisitingNoteMapping(VisitingNoteMappings VisitingNoteMapping);
        void UpdateVisitingNoteMapping(VisitingNoteMappings VisitingNoteMapping);
        void DeleteVisitingNoteMapping(VisitingNoteMappings VisitingNoteMapping);
    }
}
