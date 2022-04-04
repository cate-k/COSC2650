using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable

namespace Webpage.EFModel
{
    public partial class PostReqResponses
    {
        [DisplayName("Post ID")]
        public int Idx { get; set; }
        public int PostIdx { get; set; }
        [DisplayName("Email Address")]
        public int ResponderIdx { get; set; }

        [DisplayName("Date/Time")]
        public DateTime RespondedOn { get; set; }

        [DisplayName("Message")]
        public string ResponseText { get; set; }
        public int ResponseTypeIdx { get; set; }

        public virtual Posts PostIdxNavigation { get; set; }
        public virtual Users ResponderIdxNavigation { get; set; }
        public virtual Response ResponseTypeIdxNavigation { get; set; }
    }
}
