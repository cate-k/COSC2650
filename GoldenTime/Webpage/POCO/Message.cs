using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Webpage.POCO
{
    public class Message
    {
        public int Idx { get; set; }
        public int SenderIdx { get; set; }
        public int ReceiverIdx { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string Subject { get; set; }
        [DisplayName("Message")]
        public string Content { get; set; }
        public int? ParentIdx { get; set; }

        public string Filename { get; set; }

        // Added POCO releationship for senders and receivers
        [DisplayName("Sender")]
        public POCO.User Sender { get; set; }
        [DisplayName("Receiver")]
        public POCO.User Receiver { get; set; }

        public Message()
        {
        }

        public static Message ToPOCO(EFModel.Messages message)
        {
            return new Message()
            {
                Idx = message.Idx,
                SenderIdx = message.SenderIdx,
                ReceiverIdx = message.ReceiverIdx,
                CreatedOn = message.CreatedOn,
                ModifiedOn = message.ModifiedOn,
                Subject = message.Subject,
                Content = message.Content,
                ParentIdx = message.ParentIdx,
                Sender = POCO.User.ToPOCO(message.SenderIdxNavigation),
                Receiver = POCO.User.ToPOCO(message.ReceiverIdxNavigation)
            };
        }
    }
}