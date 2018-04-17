using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vrektproject.Models
{
    public class Like
    {
        public int Id { get; set; }
        public virtual ApplicationUser Member { get; set; }
        public virtual ApplicationUser Recruiter { get; set; }
        public bool MemberLike { get; set; }
        public bool RecruiterLike { get; set; }
    }
}
