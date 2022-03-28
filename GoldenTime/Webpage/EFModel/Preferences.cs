using System;
using System.Collections.Generic;

#nullable disable

namespace Webpage.EFModel
{
    public partial class Preferences
    {
        public int Idx { get; set; }
        public int UserIdx { get; set; }
        public int PreferenceIdx { get; set; }
        public string PreferenceValue { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public virtual Preference PreferenceIdxNavigation { get; set; }
        public virtual Users UserIdxNavigation { get; set; }
    }
}
