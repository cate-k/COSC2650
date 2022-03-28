using System;
using System.Collections.Generic;

#nullable disable

namespace Webpage.EFModel
{
    public partial class Attachments
    {
        public int Idx { get; set; }
        public int PostIdx { get; set; }
        public string Description { get; set; }
        public string Filename { get; set; }
        public byte[] Content { get; set; }
        public string Mime { get; set; }
        public DateTime? CreatedOn { get; set; }

        public virtual Posts PostIdxNavigation { get; set; }
    }
}
