using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MPMSRS.Models.Entities;
using MPMSRS.Models.VM;
using MPMSRS.Services.Interfaces;

namespace MPMSRS.Services.Repositories
{
    public class NetworkRepository : UserBase<Networks>, INetworkProfileRepository
    {
        public NetworkRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateNetwork(Networks network)
        {
            Create(network);
        }

        public void DeleteNetwork(Networks network)
        {
            Update(network);
        }

        public async Task<IEnumerable<string>> GetAllNetworkCity()
        {
            return await RepositoryContext.Networks
                .Where(ow => ow.IsDeleted == false && ow.City.Trim() != "" )
                .OrderBy(ow => ow.City)
                .Select(ow => ow.City)
                .Distinct()
                .ToListAsync();
        }

        public async Task<NetworkDtoViewModel> GetAllNetworks(int pageSize, int pageIndex, string area, string query)
        {
            var lstContext = FindAll()
                .Where(ow => ow.IsDeleted == false)
                .AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrEmpty(area))
            {
                lstContext = lstContext.Where(Q => Q.City.ToLower() == area.ToLower());
            }

            if (!string.IsNullOrEmpty(query))
            {
                lstContext = lstContext.Where(Q => Q.AhmCode.ToLower().Contains(query.ToLower()) || Q.MDCode.ToLower().Contains(query.ToLower()) || Q.DealerName.ToLower().Contains(query.ToLower()) || Q.Address.ToLower().Contains(query.ToLower()));
            }

            PagesViewModel pages = new PagesViewModel();
            pages.Total = lstContext.Count();
            pages.PerPage = pageSize;
            pages.Page = pageIndex;
            pages.LstPage = pages.Total / pageSize;

            var data = await lstContext.OrderBy(Q => Q.AccountNum).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            return new NetworkDtoViewModel
            {
                Data = data,
                Pages = pages
            };
        }

        public async Task<List<Networks>> GetNetworksByAhmCode(string ahmCode)
        {
            return await FindByCondition(network => network.AhmCode.ToLower().Contains(ahmCode.ToLower()))
            .Where(network => network.IsDeleted == false)
            .ToListAsync();
        }

        public async Task<Networks> GetNetworksById(Guid networkId)
        {
            return await FindByCondition(network => network.NetworkId.Equals(networkId))
            .Where(network => network.IsDeleted == false)
            .FirstOrDefaultAsync();
        }

        public async Task<List<Networks>> GetNetworksByMDCode(string mdCode)
        {
            return await FindByCondition(network => network.MDCode.ToLower().Contains(mdCode.ToLower()))
            .Where(network => network.IsDeleted == false)
            .ToListAsync();
        }

        public async Task<List<Networks>> GetNetworksByNamaDealer(string namaDealer)
        {
            return await FindByCondition(network => network.DealerName.ToLower().Contains(namaDealer.ToLower()))
            .Where(network => network.IsDeleted == false)
            .OrderBy(network => network.DealerName)
            .ToListAsync();
        }

        public void UpdateNetwork(Networks network)
        {
            Update(network);
        }
    }
}
