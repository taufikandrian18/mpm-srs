using System;
using System.Threading.Tasks;
using MPMSRS.Models.VM;

namespace MPMSRS.Services.Interfaces.ICaller
{
    public interface IVPSClient
    {
        Task<TokenMPMData> GetTokenAuthorization();
        Task<UserMPMResponse> GetUserFromClient(string user, string token);
        Task<userVpsSecretKey> GetVPSSecretKey(string token, LoginDealerDto model);
        Task<userVpsSecretKey> GetVPSUserDealer(string token, string secretKey, LoginDealerDto model);
    }
}
