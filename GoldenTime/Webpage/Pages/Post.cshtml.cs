using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Webpage.EFModel;
using Webpage.PageObjects.Post;
using Webpage.Shared;

namespace Webpage.Pages
{
    [BindProperties]
    public class PostModel : PageModel
    {
        private readonly IDbContextFactory<cosc2650Context> _contextFactory;
        private readonly ILogger<IndexModel> _logger;

        public PageObjects.Post.Post Post { get; set; }
        public List<POCO.Category> AvailableCategories { get; set; }

        // This is definitely not best approach, but not going into full binding
        // of the complex models back from model, so just query and regenerate each
        // time.
        private void BuildPostModelComplexProperties()
        {
            try
            {
                // Categories
                AvailableCategories = Helper.BuildCategories(_contextFactory).ToList();
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, string.Concat("PostModel:BuildPostModelComplexProperties: ", ex.Message), new object[0]);
            }
        }

        public PostModel(ILogger<IndexModel> logger, IDbContextFactory<cosc2650Context> contextFactory)
        {
            _logger = logger;
            _contextFactory = contextFactory;

            // Avoiding data seed in the constructor for performance reasons.
            // All work done on request
        }

        public void OnGet()
        {
            // Get all the *complex* stuff we need.
            BuildPostModelComplexProperties();
        }

        public IActionResult OnPost()
        {
            BuildPostModelComplexProperties();

            if (!ModelState.IsValid)
                return Page(); // TODO: Caitlen: CSS should be created so the failed items show the validation text;

            try
            {
                // This is where we inspect the http post, bound properties on the model and save...
                using (var dbc = _contextFactory.CreateDbContext())
                {
                    var p = new Posts()
                    {
                        Content = Post.Content,
                        Subject = Post.Title,
                        UserIdx = Helper.GetUserIndex(_contextFactory, "s3739099@student.rmit.edu.au"), // <-- TODO: This is where we need claims from Sam's work.                 
                        StartingOn = Post.StartsOn,
                        EndingOn = Post.EndsOn,
                        LocationIdx = Helper.GetLocationIndex(_contextFactory, Post.PostCode)
                    };

                    // Fetch Categories selected and make the links.
                    Helper.GetPostSelectedCategories(_contextFactory, Request.Form)
                        .ForEach(c => p.PostCategories.Add(new PostCategories()
                            { CategoryIdx = c.Idx, PostIdxNavigation = p }));

                    // Upload file if exists
                    if (Post.AttachedFile != null)
                    {
                        var attachment = Helper.CreateAttachment(_contextFactory, Post.AttachedFile, p);
                        p.Attachments.Add(attachment);
                    }
                    
                    dbc.Posts.Add(p);
                    dbc.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, string.Concat("PostModel:OnPost: ", ex.Message), new object[0]);
            }

            return Redirect("Index");
        }
    }

}

