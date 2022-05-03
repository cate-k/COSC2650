using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Webpage.EFModel;
using Webpage.Shared;

namespace Webpage.Pages
{
    public class FeedModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IDbContextFactory<cosc2650Context> _contextFactory;
        public string Identity;

        public List<POCO.Post> LastPosts;
        public FeedModel(ILogger<IndexModel> logger, IDbContextFactory<cosc2650Context> contextFactory)
        {
            _logger = logger;
            _contextFactory = contextFactory;
            LastPosts = new List<POCO.Post>();
        }
        public void OnGet()
        {
            var userId = User.GetUserEmail();
            POCO.User user;

            if (!string.IsNullOrEmpty(userId))
            {
                user = Helper.GetUserDeep(_contextFactory, userId);

            }
            LastPosts = Helper.GetPosts(_contextFactory);
        }
    }
}
