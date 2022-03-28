using System;
using System.Collections.Generic;

#nullable disable

namespace Webpage.EFModel
{
    public partial class PostCategories
    {
        public int Idx { get; set; }
        public int PostIdx { get; set; }
        public int CategoryIdx { get; set; }

        public virtual Category CategoryIdxNavigation { get; set; }
        public virtual Posts PostIdxNavigation { get; set; }
    }
}
