using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Webpage.EFModel;
using Webpage.Shared;

namespace Webpage.POCO
{
    public class Post
    {
        public int Idx { get; set; }
        public int UserIdx { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public int? ParentIdx { get; set; }
        public DateTime? StartingOn { get; set; }
        public DateTime? EndingOn { get; set; }
        public int? LocationIdx { get; set; }
        // Flattened
        public string Email { get; set; }
        public string FullName { get; set; }
        public int? Age { get; set; }
        public string Mobile { get; set; }
        public string UserLocationCode { get; set; }
        public string PostLocationCode { get; set; }
        public string Filename { get; set; }

        public List<Attachment> Attachments { get; set; }
        public List<Category> Categories { get; set; }

        public Post()
        {
            Attachments = new List<Attachment>();
            Categories = new List<Category>();
        }

        public static Post ToPOCO(EFModel.Posts post) {
            var p = new Post()
            {
                Idx = post.Idx,
                UserIdx = post.UserIdx,
                CreatedOn = post.CreatedOn,
                ModifiedOn = post.ModifiedOn,
                Subject = post.Subject,
                Content = post.Content,
                ParentIdx = post.ParentIdx,
                StartingOn = post.StartingOn,
                EndingOn = post.EndingOn,
                LocationIdx = post.LocationIdx,
                Email = post.UserIdxNavigation.Email,
                FullName = post.UserIdxNavigation.FullName,
                Age = post.UserIdxNavigation.Age,
                Mobile = post.UserIdxNavigation.Mobile,
                PostLocationCode = post.LocationIdxNavigation.AreaCode,
                UserLocationCode = post.UserIdxNavigation.LocationIdxNavigation.AreaCode
            };

            var postCategories = post.PostCategories.Select(i => i.CategoryIdx);
            p.Categories.AddRange(Helper.Cached_Categories_Flat.Where(c => postCategories.Contains(c.Idx)));
            post.Attachments.ToList()
                .ForEach(a => p.Attachments.Add(Attachment.ToPOCO(a)));
            
            return p;
        }
    }
}
