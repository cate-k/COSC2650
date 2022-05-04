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

        public PostModel(ILogger<IndexModel> logger, IDbContextFactory<cosc2650Context> contextFactory)
        {
            _logger = logger;
            _contextFactory = contextFactory;

            // Avoiding data seed in the constructor for performance reasons.
            // All work done on request
        }

        public IActionResult OnGet()
        {
            if (string.IsNullOrEmpty(User.GetUserRole()))
                return RedirectToPage("/Account/Login");

            return Page();
        }

        public IActionResult OnPost()
        {
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
                        UserIdx = Helper.GetUserIndex(_contextFactory, User.GetUserEmail()),                 
                        StartingOn = Post.StartsOn,
                        EndingOn = Post.EndsOn,
                        LocationIdx = Helper.GetLocationIndex(_contextFactory, Post.PostCode)
                    };

                    // Fetch Categories selected and make the links.
                    Helper.GetItemSelectedCategories(_contextFactory, Request.Form)
                        .ForEach(c => p.PostCategories.Add(new PostCategories()
                            { CategoryIdx = c.Idx, PostIdxNavigation = p }));

                    // Upload file if exists
                    if (Post.AttachedFile != null)
                    {
                        var attachment = Helper.CreateAttachment(_contextFactory, Post.AttachedFile, p);
                        p.Attachments.Add(attachment.Result);
                    }
                    
                    dbc.Posts.Add(p);
                    dbc.Stats.Add(new Stats() {Event = "Post", Meta = "Created"});

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

