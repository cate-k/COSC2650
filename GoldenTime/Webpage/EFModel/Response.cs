using System;
using System.Collections.Generic;

#nullable disable

namespace Webpage.EFModel
{
    public partial class Response
    {
        public Response()
        {
            PostReqResponses = new HashSet<PostReqResponses>();
        }

        public int Idx { get; set; }
        public string Caption { get; set; }
        public int? CategoryIdx { get; set; }
        public int? LogicalResponse { get; set; }

        public virtual Category CategoryIdxNavigation { get; set; }
        public virtual ICollection<PostReqResponses> PostReqResponses { get; set; }
    }
}
