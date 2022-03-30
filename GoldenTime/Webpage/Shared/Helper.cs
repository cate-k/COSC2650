using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Webpage.EFModel;

namespace Webpage.Shared
{
    public static class Helper
    {
        // Create Categories in a structured tree
        public static IList<POCO.Category> BuildCategories(IDbContextFactory<cosc2650Context> contextFactory)
        {
            // Categories
            var availableCategories = new List<POCO.Category>();
            using (var dbc = contextFactory.CreateDbContext())
            {
                // Only top Categories, the rest come through conversion.
                dbc.Category.Where(c => c.CategoryType.Equals("Post"))
                    .ToList()
                    .ForEach(c => availableCategories.Add(POCO.Category.ToPOCO(c)));
            }

            // Start at the top -- all the items are already created on the flat structure
            // Other levels are recursive in the object itself
            availableCategories.Where(c => c.ParentIdx == null)
                .ToList()
                .ForEach(i => i.Children.AddRange(POCO.Category.GetChildren(availableCategories, i.Idx)));

            // Cleanup flat structure, all removed but top ones.
            availableCategories.RemoveAll(r => r.ParentIdx != null);

            return availableCategories;
        }

        public static int GetUserIndex(IDbContextFactory<cosc2650Context> contextFactory, string claim_email) {
            using (var dbc = contextFactory.CreateDbContext())
                return dbc.Users.Where(u => u.Email == claim_email).FirstOrDefault().Idx;
        }
    }
}
