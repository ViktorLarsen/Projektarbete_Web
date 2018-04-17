using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vrektproject.Models
{
    public class Profile
    {
        public int Id { get; set; }
        public string PictureURL { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public string Company { get; set; }
    }
}
