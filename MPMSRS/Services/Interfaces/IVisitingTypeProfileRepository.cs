using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MPMSRS.Models.Entities;

namespace MPMSRS.Services.Interfaces
{
    public interface IVisitingTypeProfileRepository : IUserProfile<VisitingTypes>
    {
        Task<IEnumerable<VisitingTypes>> GetAllVisitingTypes(int pageSize, int pageIndex);
        Task<VisitingTypes> GetVisitingTypeById(Guid visitingTypeId);
        Task<VisitingTypes> GetVisitingTypeByUsername(string visitingTypeName);
        void CreateVisitingType(VisitingTypes visitingType);
        void UpdateVisitingType(VisitingTypes visitingType);
        void DeleteVisitingType(VisitingTypes visitingType);
    }
}
