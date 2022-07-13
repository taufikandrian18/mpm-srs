using System;
using System.ComponentModel.DataAnnotations;

namespace MPMSRS.Models.VM
{
    public class RefreshRequest
    {
        [Required]
        public string RefreshToken { get; set; }
    }
}
