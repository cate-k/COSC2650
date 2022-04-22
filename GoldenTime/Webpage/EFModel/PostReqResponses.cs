using System;
using System.Collections.Generic;

#nullable disable

namespace Webpage.EFModel
{
    public partial class PostReqResponses
    {
        public int Idx { get; set; }
        public int PostIdx { get; set; }
        public int ResponderIdx { get; set; }
        public DateTime RespondedOn { get; set; }
        public string ResponseText { get; set; }
        public int ResponseTypeIdx { get; set; }

        public virtual Posts PostIdxNavigation { get; set; }
        public virtual Users ResponderIdxNavigation { get; set; }
        public virtual Response ResponseTypeIdxNavigation { get; set; }
    }
}
