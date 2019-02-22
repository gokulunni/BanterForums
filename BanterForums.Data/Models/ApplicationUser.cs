using Microsoft.AspNetCore.Identity;
using System;

namespace BanterForums.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string ProfileImageUrl { get; set; }
        public DateTime MemberSince { get; set; }
        public bool IsActive { get; set; }


    }
}
