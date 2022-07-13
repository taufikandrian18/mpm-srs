using System;
namespace MPMSRS.Models.VM
{
    public class LoginDto
    {
        public string UserId { get; set; }
        public string Password { get; set; }
        public string UserFcmToken { get; set; }
    }

    public class LoginDealerDto
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Password { get; set; }
        public string DealerCode { get; set; }
        public string UserFcmToken { get; set; }
    }

    public class LoginForgetPasswordDto
    {
        public string UserId { get; set; }
        public string Email { get; set; }
    }
}
