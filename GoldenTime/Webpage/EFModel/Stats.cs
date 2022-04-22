using System;
using System.Collections.Generic;

#nullable disable

namespace Webpage.EFModel
{
    public partial class Stats
    {
        public int Idx { get; set; }
        public string Event { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Meta { get; set; }
    }
}
