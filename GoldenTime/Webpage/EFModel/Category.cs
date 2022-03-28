using System;
using System.Collections.Generic;

#nullable disable

namespace Webpage.EFModel
{
    public partial class Category
    {
        public Category()
        {
            InverseParentIdxNavigation = new HashSet<Category>();
            PostCategories = new HashSet<PostCategories>();
            Preference = new HashSet<Preference>();
            Response = new HashSet<Response>();
        }

        public int Idx { get; set; }
        public string Name { get; set; }
        public int? ParentIdx { get; set; }
        public string CategoryType { get; set; }
        public string Description { get; set; }

        public virtual Category ParentIdxNavigation { get; set; }
        public virtual ICollection<Category> InverseParentIdxNavigation { get; set; }
        public virtual ICollection<PostCategories> PostCategories { get; set; }
        public virtual ICollection<Preference> Preference { get; set; }
        public virtual ICollection<Response> Response { get; set; }
    }
}
