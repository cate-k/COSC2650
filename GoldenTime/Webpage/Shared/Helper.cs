using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Webpage.EFModel;
using System.Threading.Tasks;

namespace Webpage.Shared
{
    public static class Helper
    {
        /*
        Configuration constants - Ideally this would be in json file, read in at
        the start
         */
        private static readonly int DEFAULT_LOCATION_IDX = 1; // This is based on db key seed.
        private static readonly string POST_CATEGORY_MATCH = "Post";

        private static List<POCO.Category> CACHED_CATEGORY_FLAT_LIST = null; //Make sure we need to init static property.
        private static List<POCO.Category> CACHED_CATEGORY_LIST = null; //Make sure we need to init static property.

        // Private, build flat categories
        public static List<POCO.Category> GetCategoriesFlat(IDbContextFactory<cosc2650Context> contextFactory)
        {
            if (CACHED_CATEGORY_FLAT_LIST != null)
                return CACHED_CATEGORY_FLAT_LIST;

            // Flat Categories -- Cache and only build if empty.
            // Note for devs: If you add/remove categories while the solution is running
            // this code will not refresh the list.
            CACHED_CATEGORY_FLAT_LIST = new List<POCO.Category>();

            using (var dbc = contextFactory.CreateDbContext())
            {
                // All Categories
                dbc.Category.Where(c => c.CategoryType.Equals(POST_CATEGORY_MATCH))
                    .ToList()
                    .ForEach(c => CACHED_CATEGORY_FLAT_LIST.Add(POCO.Category.ToPOCO(c, contextFactory)));
            }

            return CACHED_CATEGORY_FLAT_LIST;
        }

        // Create Categories in a structured tree
        public static IList<POCO.Category> BuildCategories(IDbContextFactory<cosc2650Context> contextFactory)
        {
            if (CACHED_CATEGORY_LIST != null)
                return CACHED_CATEGORY_LIST;

            // Categories -- Cache and only build if empty.
            // Note for devs: If you add/remove categories while the solution is running
            // this code will not refresh the list.
            CACHED_CATEGORY_LIST = new List<POCO.Category>();
            List<POCO.Category> availableCategories = new List<POCO.Category>();
            availableCategories.AddRange(GetCategoriesFlat(contextFactory));

            // Start at the top -- all the items are already created on the flat structure
            // Other levels are recursive in the object itself
            availableCategories.Where(c => c.ParentIdx == null)
                .ToList()
                .ForEach(i => i.Children.AddRange(POCO.Category.GetChildren(availableCategories, i.Idx)));

            // Cleanup flat structure, all removed but top ones.
            availableCategories.RemoveAll(r => r.ParentIdx != null);
            CACHED_CATEGORY_LIST = availableCategories;

            return CACHED_CATEGORY_LIST;
        }

        // Fetch User idx by email claim
        public static int GetUserIndex(IDbContextFactory<cosc2650Context> contextFactory, string claim_email) {
            using (var dbc = contextFactory.CreateDbContext())
                return dbc.Users.Where(u => u.Email == claim_email).FirstOrDefault().Idx;
        }

        // Fetch Location idx
        public static int GetLocationIndex(IDbContextFactory<cosc2650Context> contextFactory, int postCode)
        {
            using (var dbc = contextFactory.CreateDbContext())
            {
                var location =  dbc.Location.Where(u => u.AreaCode == postCode.ToString()).FirstOrDefault();
                return location != null ? location.Idx : DEFAULT_LOCATION_IDX; 
            }
        }

        // Creates and adds to Attachments,
        // but does not store/persist.
        // The class is returned as EF entity.
        public static async Task<Attachments> CreateAttachment(IDbContextFactory<cosc2650Context> contextFactory, IFormFile attachedFile, Posts post)
        {
            using (var stream = new MemoryStream())
            {
                await attachedFile.CopyToAsync(stream);
                stream.Position = 0;

                using (var dbc = contextFactory.CreateDbContext())
                {
                    var attachment = new Attachments()
                    {
                        Description = $"Post upload",
                        Filename = attachedFile.FileName,
                        Mime = attachedFile.ContentType,
                        PostIdxNavigation = post,
                        Content = stream.ToArray()
                    };
                    dbc.Attachments.Add(attachment);
                    return attachment;
                }
            }
        }

        // Returns selected categories on the post as list of POCOs
        public static List<POCO.Category> GetPostSelectedCategories(IDbContextFactory<cosc2650Context> contextFactory, IFormCollection form)
        {
            var names = form.Select(s => s.Key);
            return GetCategoriesFlat(contextFactory).Where(c => names.Contains(c.Name)).ToList();
        }

        // Get last x posts
        public static List<POCO.Post> GetPosts(IDbContextFactory<cosc2650Context> contextFactory, int topItems = 10)
        {
            // Refactored; Multiple Includes() cause cartesian product explosion and exponentially degraded performance
            // with each additional item load. 
            // Loads non-lazy what is needed only
            using (var dbc = contextFactory.CreateDbContext())
            {
                dbc.ChangeTracker.LazyLoadingEnabled = false;

                var result = new List<POCO.Post>();
                var p = dbc.Posts
                    .OrderByDescending(p => p.CreatedOn)
                    .Take(topItems);
                var postIndexes = p.Select(f => f.Idx);
                var userIndexes = p.Select(f=> f.UserIdx).Distinct();
                var locationIndexes = p.Select(f => f.LocationIdx).Distinct();
                var attachmentIndexes = dbc.Attachments.Where(att => postIndexes.Contains(att.PostIdx))
                    .Select(attidx => attidx.Idx).Distinct();
                var categoryIndexes = dbc.PostCategories.Where(pca => postIndexes.Contains(pca.PostIdx))
                    .Select(catidx => catidx.CategoryIdx).Distinct();

                dbc.Users.Where(u => userIndexes.Contains(u.Idx))
                    .Include(ul => ul.LocationIdxNavigation)
                    .Load();
                dbc.Location.Where(u => locationIndexes.Contains(u.Idx))
                    .Load();
                dbc.Attachments.Where(u => attachmentIndexes.Contains(u.Idx))
                    .Load();
                dbc.PostCategories.Where(u => postIndexes.Contains(u.PostIdx))
                    .Load();
                dbc.Category.Where(u => categoryIndexes.Contains(u.Idx))
                    .Load();
                
                p.ToList().ForEach(i => result.Add(POCO.Post.ToPOCO(i, contextFactory)));

                return result;
            }
        }

        public static List<POCO.Message> GetMessages(IDbContextFactory<cosc2650Context> contextFactory)
        {
            using (var dbc = contextFactory.CreateDbContext())
            {
                var result = new List<POCO.Message>();
                dbc.Messages
                    .Include(u => u.SenderIdxNavigation) // These two Include statements tell EF to go one level deeper and retrieve navigational items
                    .Include(u => u.ReceiverIdxNavigation)
                    .OrderByDescending(p => p.CreatedOn)
                    .Where(u => u.ReceiverIdx == Helper.GetUserIndex(contextFactory, "s3820255@student.rmit.edu.au"))//TODO: add claim
                    .ToList()
                    .ForEach(i => result.Add(POCO.Message.ToPOCO(i)));
                return result;
            }
        }

        //Helper function to get the user information
        public static List<POCO.User> GetUsers(IDbContextFactory<cosc2650Context> contextFactory)
        {
            using (var dbc = contextFactory.CreateDbContext())
            {
                var result = new List<POCO.User>();
                    dbc.Users
                    .Where(u => u.Idx == Helper.GetUserIndex(contextFactory, "s3820255@student.rmit.edu.au"))
                    .ToList()
                    .ForEach( i => result.Add(POCO.User.ToPOCO(i)));
                    return result;
            }
        }

        public static string ConvertToB64(byte[] imageData)
        {
            try
            {
                return Convert.ToBase64String(imageData, 0, imageData.Length);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return string.Empty;
            }
        }

    }
}
