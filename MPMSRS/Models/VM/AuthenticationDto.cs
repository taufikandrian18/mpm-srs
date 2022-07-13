using System;
namespace MPMSRS.Models.VM
{
    public class AuthenticationDto
    {
        public Guid AuthenticationId { get; set; }
        public string Username { get; set; }
        public string Otp { get; set; }
        public string UserData { get; set; }
    }

    public class AuthenticationForCreationDto
    {
        public string Username { get; set; }
        public string Otp { get; set; }
        public string UserData { get; set; }
    }

    public class AuthenticationForUpdateDto
    {
        public Guid AuthenticationId { get; set; }
        public string Username { get; set; }
        public string Otp { get; set; }
        public string UserData { get; set; }
    }

    public class AuthenticationForDeleteDto
    {
        public Guid AuthenticationId { get; set; }
        public string Username { get; set; }
        public string Otp { get; set; }
        public string UserData { get; set; }
    }
}
