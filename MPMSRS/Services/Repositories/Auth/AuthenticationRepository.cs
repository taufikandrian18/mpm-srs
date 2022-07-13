using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MPMSRS.Models.Entities;
using MPMSRS.Services.Interfaces.Auth;

namespace MPMSRS.Services.Repositories.Auth
{
    public class AuthenticationRepository : UserBase<Authentications>, IAuthenticationProfileRepository
    {
        public AuthenticationRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateAuthentication(Authentications authentication)
        {
            Create(authentication);
        }

        public void DeleteAuthentication(Authentications authentication)
        {
            Delete(authentication);
        }

        public async Task<IEnumerable<Authentications>> GetAllAuthentications()
        {
            return await FindAll()
                .OrderBy(ow => ow.Username)
                .ToListAsync();
        }

        public async Task<Authentications> GetAuthenticationById(Guid authenticationId)
        {
            return await FindByCondition(auth => auth.AuthenticationId.Equals(authenticationId))
            .FirstOrDefaultAsync();
        }

        public async Task<Authentications> GetAuthenticationByUsername(string username)
        {
            return await FindByCondition(auth => auth.Username.Equals(username))
            .FirstOrDefaultAsync();
        }

        public void UpdateAuthentication(Authentications authentication)
        {
            Update(authentication);
        }
    }
}
