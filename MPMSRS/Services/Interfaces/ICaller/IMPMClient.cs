using System;
using System.Threading.Tasks;
using MPMSRS.Models.VM;

namespace MPMSRS.Services.Interfaces.ICaller
{
    public interface IMPMClient
    {
        Task<string> GetLoginAuthorization(LoginDto model);
        Task<LoginSerialization> GetMPMOrange(LoginDto model);
        Task<ResetPasswordSerialization> MPMResetPassword(string paramUserId, string paramEmail);
    }
}
