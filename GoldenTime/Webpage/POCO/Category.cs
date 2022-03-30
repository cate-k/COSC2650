using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Webpage.POCO
{
    public class Category
    {
        public int Idx { get; set; }
        public string Name { get; set; }
        public string CategoryType { get; set; }
        public string Description { get; set; }
        public Category Parent { get; set; }
        public List<Category> Children { get; set; }
        public int? ParentIdx { get; set; }

        public Category()
        {
            Parent = null;
            Children = new List<Category>();
        }

        public Category(int Idx, string Name, string CategoryType, string Description, int? ParentIdx) : base()
        {
            this.Idx = Idx;
            this.CategoryType = CategoryType;
            this.Description = Description;
            this.Name = Name;
            this.ParentIdx = ParentIdx;
        }

        public static POCO.Category ToPOCO(EFModel.Category category)
        {
            return new Category()
            {
                Idx = category.Idx,
                CategoryType = category.CategoryType,
                Description = category.Description,
                Name = category.Name,
                ParentIdx = category.ParentIdx
            };
        }

        public static List<POCO.Category> GetChildren(List<POCO.Category> availableCategories, int parentIndex)
        {
            var resultList = new List<POCO.Category>();
            availableCategories.Where(c => c.ParentIdx == parentIndex)
                .ToList()
                .ForEach(i =>
                {
                    var ct = new Category()
                    {
                        Idx = i.Idx,
                        CategoryType = i.CategoryType,
                        Description = i.Description,
                        Name = i.Name,
                        ParentIdx = i.ParentIdx,
                    };
                    ct.Children.AddRange(POCO.Category.GetChildren(availableCategories, i.Idx));
                    resultList.Add(ct);
                }
            );

            return resultList;
        }

        internal static EFModel.Category ToEF(IDbContextFactory<EFModel.cosc2650Context> contextFactory, int idx)
        {
            using (var dbc = contextFactory.CreateDbContext())
                return dbc.Category.Where(c => c.Idx == idx).FirstOrDefault();
        }
    }
}
