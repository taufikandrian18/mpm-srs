using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MPMSRS.Models.Entities;

namespace MPMSRS.Services.Interfaces
{
    public interface IEventMasterDataProfileRepository : IUserProfile<EventMasterDatas>
    {
        Task<IEnumerable<EventMasterDatas>> GetAllEventMasterDatas(int pageSize, int pageIndex, string location);
        Task<IEnumerable<string>> GetAllEventMasterDataLocation();
        Task<EventMasterDatas> GetEventMasterDataById(Guid eventMasterDataId);
        void CreateEventMasterData(EventMasterDatas eventMasterData);
        void UpdateEventMasterData(EventMasterDatas eventMasterData);
        void DeleteEventMasterData(EventMasterDatas eventMasterData);
    }
}
