using System;
using System.Collections.Generic;
using System.Linq;

namespace Webpage.Shared
{
    public class Rating
    {
        private const decimal LOCATION_PENALTY = 0.5m;
        private const decimal NO_CATEGORY_PENALTY = 0.33m;
        
        private POCO.User _user;
        private readonly List<int> UserCategories;

        public Rating(POCO.User user)
        {
            _user = user;
            UserCategories = _user.Categories.Select(c => c.Idx).ToList();
        }

        public decimal Calculate(POCO.Post post) 
        {
            decimal rating = 1.0m;

            var pc = new List<int>();
            post.Categories.ForEach(c => c.Children.ForEach(ch => pc.Add(ch.Idx)));
            pc = pc.Distinct().ToList();

            // User criteria
            rating *= _user.LocationIdx == post.LocationIdx ? 1m : LOCATION_PENALTY;

            // Category criteria
            var directCategoryMatch = post.Categories.Where(c => UserCategories.Contains(c.Idx));
            var parentCategoryMatch = post.Categories.Where(c => UserCategories.Contains(c.ParentIdx.HasValue ? c.ParentIdx.Value : 0));
            var childCategoryMatch = pc.Where(c => UserCategories.Contains(c));

            // if there's at least one direct category, just return
            if (directCategoryMatch.ToList().Count > 0)
                return rating;

            // Sigmoid function will work well if we have up to 4, 5 sub categories
            // with more it will limit towards 1.
            if (parentCategoryMatch.ToList().Count > 0)
            {
                var sigmoid = 1 / (1 + Math.Pow(Math.E, -1 * parentCategoryMatch.ToList().Count)); 
                return rating *= Convert.ToDecimal(sigmoid);
            } else
            if (childCategoryMatch.ToList().Count > 0)
            {
                var sigmoid = 1 / (1 + Math.Pow(Math.E, -1 * childCategoryMatch.ToList().Count));
                return rating *= Convert.ToDecimal(sigmoid);
            }

            return rating *= NO_CATEGORY_PENALTY;
        }
    }
}
