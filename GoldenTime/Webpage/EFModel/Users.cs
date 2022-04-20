using System;
using System.Collections.Generic;

#nullable disable

namespace Webpage.EFModel
{
    public partial class Users
    {
        public Users()
        {
            MessagesReceiverIdxNavigation = new HashSet<Messages>();
            MessagesSenderIdxNavigation = new HashSet<Messages>();
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
        public int IsAdmin { get; set; }
        public string password { get; set; }

        public virtual Location LocationIdxNavigation { get; set; }
        public virtual ICollection<Messages> MessagesReceiverIdxNavigation { get; set; }
        public virtual ICollection<Messages> MessagesSenderIdxNavigation { get; set; }
        public virtual ICollection<PostReqResponses> PostReqResponses { get; set; }
        public virtual ICollection<Posts> Posts { get; set; }
        public virtual ICollection<Preferences> Preferences { get; set; }

    }
}
