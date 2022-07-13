using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MPMSRS.Models.Entities;
using MPMSRS.Models.VM;

namespace MPMSRS.Services.Interfaces
{
    public interface INetworkProfileRepository : IUserProfile<Networks>
    {
        Task<NetworkDtoViewModel> GetAllNetworks(int pageSize, int pageIndex, string area, string query);
        Task<IEnumerable<string>> GetAllNetworkCity();
        Task<Networks> GetNetworksById(Guid networkId);
        Task<List<Networks>> GetNetworksByAhmCode(string ahmCode);
        Task<List<Networks>> GetNetworksByMDCode(string mdCode);
        Task<List<Networks>> GetNetworksByNamaDealer(string namaDealer);
        void CreateNetwork(Networks network);
        void UpdateNetwork(Networks network);
        void DeleteNetwork(Networks network);
    }
}
