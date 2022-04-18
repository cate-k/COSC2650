using System;
using Webpage.EFModel;
using Webpage.Shared;

namespace Webpage.POCO
{
    public class Attachment
    {
        public int Idx { get; set; }
        public int PostIdx { get; set; }
        public string Description { get; set; }
        public string Filename { get; set; }
        public byte[] Content { get; set; }

        public string WebContent => Helper.ConvertToB64(Content);
        public string Mime { get; set; }
        public DateTime? CreatedOn { get; set; }

        public Attachment()
        {
        }

        public static Attachment ToPOCO(EFModel.Attachments att)
        {
            return new Attachment()
            {
                Description = att.Description,
                Filename = att.Filename,
                Idx = att.Idx,
                Mime = att.Mime,
                CreatedOn = att.CreatedOn,
                PostIdx = att.PostIdx,
                Content = att.Content
            };
        }
    }
}