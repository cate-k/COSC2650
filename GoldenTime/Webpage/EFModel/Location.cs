using System;
using System.Collections.Generic;

#nullable disable

namespace Webpage.EFModel
{
    public partial class Location
    {
        public Location()
        {
            Posts = new HashSet<Posts>();
            Users = new HashSet<Users>();
        }

        public int Idx { get; set; }
        public string AreaCode { get; set; }
        public string Caption { get; set; }
        public string Description { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public decimal? WeakLongitude { get; set; }
        public decimal? WeakLatitude { get; set; }

        public virtual ICollection<Posts> Posts { get; set; }
        public virtual ICollection<Users> Users { get; set; }
    }
}
