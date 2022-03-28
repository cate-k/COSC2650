using System;
using System.Collections.Generic;

#nullable disable

namespace Webpage.EFModel
{
    public partial class Users
    {
        public Users()
        {
            PostReqResponses = new HashSet<PostReqResponses>();
            Posts = new HashSet<Posts>();
            Preferences = new HashSet<Preferences>();
        }

        public int Idx { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public int? Age { get; set; }
        public int? LocationIdx { get; set; }
        public string Mobile { get; set; }

        public virtual Location LocationIdxNavigation { get; set; }
        public virtual ICollection<PostReqResponses> PostReqResponses { get; set; }
        public virtual ICollection<Posts> Posts { get; set; }
        public virtual ICollection<Preferences> Preferences { get; set; }
    }
}
