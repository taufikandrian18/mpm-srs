using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MPMSRS.Models.Entities;

namespace MPMSRS.Services.Interfaces.Auth
{
    public interface IAuthenticationProfileRepository : IUserProfile<Authentications>
    {
        Task<IEnumerable<Authentications>> GetAllAuthentications();
        Task<Authentications> GetAuthenticationById(Guid authenticationId);
        Task<Authentications> GetAuthenticationByUsername(string username);
        void CreateAuthentication(Authentications authentication);
        void UpdateAuthentication(Authentications authentication);
        void DeleteAuthentication(Authentications authentication);
    }
}
