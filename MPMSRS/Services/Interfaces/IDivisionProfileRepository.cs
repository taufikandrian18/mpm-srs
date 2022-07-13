using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MPMSRS.Models.Entities;

namespace MPMSRS.Services.Interfaces
{
    public interface IDivisionProfileRepository : IUserProfile<Divisions>
    {
        Task<IEnumerable<Divisions>> GetAllDivisions(int pageSize, int pageIndex);
        Task<Divisions> GetDivisionById(Guid divisionId);
        Task<Divisions> GetDivisionByUsername(string divisionName);
        void CreateDivision(Divisions division);
        void UpdateDivision(Divisions division);
        void DeleteDivision(Divisions division);
    }
}
