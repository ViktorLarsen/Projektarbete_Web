using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Vrektproject.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public int RoleIdentifier { get; set; }
        public bool Authorized { get; set; }
        public int ProfileId { get; set; }
        public virtual Profile Profile { get; set; }
    }
}
