using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        [Required]
        [RegularExpression(@"^[0-9]{4}$",
            ErrorMessage = "Please enter a valid area code.")]
        [Display(Name = "Postcode")]
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
