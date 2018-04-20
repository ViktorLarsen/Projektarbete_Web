using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vrektproject.Models
{
    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProfileId { get; set; }
        public virtual Profile Profile { get; set; }
    }
}
