using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable

namespace Webpage.EFModel
{
    public partial class Messages
    {
        public Messages()
        {
            InverseParentIdxNavigation = new HashSet<Messages>();
        }

        [DisplayName("Message ID")]
        public int Idx { get; set; }
        [DisplayName("Sender ID")]
        public int SenderIdx { get; set; }
        [DisplayName("Receiver ID")]
        public int ReceiverIdx { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        [DisplayName("Subject")]
        public string Subject { get; set; }

        [DisplayName("Message")]
        public string Content { get; set; }

        public int? ParentIdx { get; set; }

        public virtual Messages ParentIdxNavigation { get; set; }
        public virtual Users ReceiverIdxNavigation { get; set; }
        public virtual Users SenderIdxNavigation { get; set; }
        public virtual ICollection<Messages> InverseParentIdxNavigation { get; set; }
    }
}
