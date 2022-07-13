using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MPMSRS.Models.Entities;
using MPMSRS.Services.Interfaces;

namespace MPMSRS.Services.Repositories
{
    public class EventMasterDataRepository : UserBase<EventMasterDatas>, IEventMasterDataProfileRepository
    {
        public EventMasterDataRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateEventMasterData(EventMasterDatas eventMasterData)
        {
            Create(eventMasterData);
        }

        public void DeleteEventMasterData(EventMasterDatas eventMasterData)
        {
            Update(eventMasterData);
        }

        public async Task<IEnumerable<string>> GetAllEventMasterDataLocation()
        {
            return await RepositoryContext.EventMasterDatas
                .Where(ow => ow.IsDeleted == false && ow.EventMasterDataLocation.Trim() != "")
                .OrderBy(ow => ow.EventMasterDataLocation)
                .Select(ow => ow.EventMasterDataLocation)
                .Distinct()
                .ToListAsync();
        }

        public async Task<IEnumerable<EventMasterDatas>> GetAllEventMasterDatas(int pageSize, int pageIndex, string location)
        {
            var lstContext = FindAll()
                .Where(ow => ow.IsDeleted == false)
                .AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrEmpty(location))
            {
                lstContext = lstContext.Where(Q => Q.EventMasterDataLocation.ToLower() == location.ToLower());
            }

            var data = await lstContext.OrderBy(Q => Q.EventMasterDataName).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            return data;
        }

        public async Task<EventMasterDatas> GetEventMasterDataById(Guid eventMasterDataId)
        {
            return await FindByCondition(eventMaster => eventMaster.EventMasterDataId.Equals(eventMasterDataId))
                .Where(eventMaster => eventMaster.IsDeleted == false)
                .FirstOrDefaultAsync();
        }

        public void UpdateEventMasterData(EventMasterDatas eventMasterData)
        {
            Update(eventMasterData);
        }
    }
}
