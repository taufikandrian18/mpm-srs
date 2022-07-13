using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MPMSRS.Models.Entities;
using MPMSRS.Models.VM;

namespace MPMSRS.Services.Interfaces
{
    public interface IVisitingPeopleProfileRepository : IUserProfile<VisitingPeoples>
    {
        Task<IEnumerable<VisitingPeoples>> GetAllVisitingPeoples(int pageSize, int pageIndex);
        Task<VisitingPeoples> GetVisitingPeopleById(Guid visitingPeopleId);
        Task<VisitingPeoples> GetVisitingPeopleByEmployeeId(Guid employeeId);
        Task<IEnumerable<VisitingPeopleViewModel>> GetVisitingPeopleByVisitingId(Guid visitingId, int pageSize, int pageIndex);
        Task<IEnumerable<VisitingPeoples>> GetVisitingPeopleByVisitingIdWithoutPage(Guid visitingId);
        void CreateVisitingPeople(VisitingPeoples visitingPeople);
        void UpdateVisitingPeople(VisitingPeoples visitingPeople);
        void DeleteVisitingPeople(VisitingPeoples visitingPeople);
        void DeleteVisitingPeopleByVisitingId(List<VisitingPeoples> listVisitingPeople);
    }
}
