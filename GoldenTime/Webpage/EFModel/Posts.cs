using System;
using System.Collections.Generic;

#nullable disable

namespace Webpage.EFModel
{
    public partial class Posts
    {
        public Posts()
        {
            Attachments = new HashSet<Attachments>();
            InverseParentIdxNavigation = new HashSet<Posts>();
            PostCategories = new HashSet<PostCategories>();
            PostReqResponses = new HashSet<PostReqResponses>();
        }

        public int Idx { get; set; }
        public int UserIdx { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public int? ParentIdx { get; set; }
        public DateTime? StartingOn { get; set; }
        public DateTime? EndingOn { get; set; }
        public int? LocationIdx { get; set; }

        public virtual Location LocationIdxNavigation { get; set; }
        public virtual Posts ParentIdxNavigation { get; set; }
        public virtual Users UserIdxNavigation { get; set; }
        public virtual ICollection<Attachments> Attachments { get; set; }
        public virtual ICollection<Posts> InverseParentIdxNavigation { get; set; }
        public virtual ICollection<PostCategories> PostCategories { get; set; }
        public virtual ICollection<PostReqResponses> PostReqResponses { get; set; }
    }
}
