using System;
using System.Collections.Generic;
using System.Linq;
using Webpage.Shared;

namespace Webpage.POCO
{
    public class User
    {
        public int Idx { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public int? Age { get; set; }
        public int? LocationIdx { get; set; }
        public string Mobile { get; set; }
        public List<Category> Categories { get; set; }


        public User()
        {
            Categories = new List<Category>();
        }

        public static User ToPOCO(EFModel.Users user)
        {
            var u = new User()
            {
                 Idx = user.Idx,
                 Email = user.Email,
                 FullName = user.FullName,
                 Age = user.Age,
                 LocationIdx = user.LocationIdx,
                 Mobile = user.Mobile
            };

            try
            {
                var userCategories = user.Preferences.Where(p => p.PreferenceIdxNavigation.Name.Equals("MatchCategory")).Select(i => int.Parse(i.PreferenceValue)).ToList();
                u.Categories.AddRange(Helper.Cached_Categories_Flat.Where(c => userCategories.Contains(c.Idx)));
            }
            catch { }
           
            return u;
        }       
    }
}