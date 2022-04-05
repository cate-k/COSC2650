using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Webpage.POCO
{
    public class Messages
    {
        public int Idx { get; set; }
        public int SenderIdx { get; set; }

        public int ReceiverIdx { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public int? ParentIdx { get; set; }

        public string Filename { get; set; }

        public Messages()

        {
        }

        public static Messages ToPOCO(EFModel.Messages message)
        {
            return new Messages()
            {
                Idx = message.Idx,
                SenderIdx = message.SenderIdx,
                ReceiverIdx = message.ReceiverIdx,
                CreatedOn = message.CreatedOn,
                ModifiedOn = message.ModifiedOn,
                Subject = message.Subject,
                Message = message.Message,
                ParentIdx = message.ParentIdx,

    
      
            };
        }
    }
}