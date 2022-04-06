using System;
using System.ComponentModel;

namespace Webpage.EFModel
{
    public partial class Messages
    {
        public Messages()
        {

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

       // public virtual Messages MessageIdxNavigation { get; set; }
       // public virtual Users senderIdxNavigation { get; set; }
       // public virtual Users receiverIdxNavigation { get; set; }

    }
}
