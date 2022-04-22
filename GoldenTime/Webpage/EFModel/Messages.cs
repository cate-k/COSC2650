using System;
using System.Collections.Generic;

#nullable disable

namespace Webpage.EFModel
{
    public partial class Messages
    {
        public Messages()
        {
            InverseParentIdxNavigation = new HashSet<Messages>();
        }

        public int Idx { get; set; }
        public int SenderIdx { get; set; }
        public int ReceiverIdx { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public int? ParentIdx { get; set; }

        public virtual Messages ParentIdxNavigation { get; set; }
        public virtual Users ReceiverIdxNavigation { get; set; }
        public virtual Users SenderIdxNavigation { get; set; }
        public virtual ICollection<Messages> InverseParentIdxNavigation { get; set; }
    }
}
