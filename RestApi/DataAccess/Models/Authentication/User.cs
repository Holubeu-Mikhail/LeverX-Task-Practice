using Microsoft.AspNetCore.Identity;
using System;

namespace DataAccessLayer.Models.Authentication
{
    public class User : IdentityUser
    {
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
