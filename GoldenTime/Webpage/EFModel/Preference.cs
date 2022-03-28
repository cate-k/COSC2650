using System;
using System.Collections.Generic;

#nullable disable

namespace Webpage.EFModel
{
    public partial class Preference
    {
        public Preference()
        {
            Preferences = new HashSet<Preferences>();
        }

        public int Idx { get; set; }
        public string Name { get; set; }
        public string Context { get; set; }
        public string DataType { get; set; }
        public int? CategoryIdx { get; set; }

        public virtual Category CategoryIdxNavigation { get; set; }
        public virtual ICollection<Preferences> Preferences { get; set; }
    }
}
