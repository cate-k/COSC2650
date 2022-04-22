using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Webpage.EFModel;

namespace Webpage.Pages.Admin
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IDbContextFactory<cosc2650Context> _contextFactory;

        public int[,] stats = new int[3, 3];  
        
        public IndexModel(ILogger<IndexModel> logger, IDbContextFactory<cosc2650Context> contextFactory)
        {
            _logger = logger;
            _contextFactory = contextFactory;

        }

        private void GetStats()
        {
            Array.Clear(stats, 0, stats.Length);
            
            using var ctx = _contextFactory.CreateDbContext();
            // messages [0,] - CREATED
            stats[0, 0] = ctx.Stats.Count(s => s.Event == "Message" && s.Meta == "Created" && s.CreatedOn >= DateTime.Today);               // Today
            stats[0, 1] = ctx.Stats.Count(s => s.Event == "Message" && s.Meta == "Created" && s.CreatedOn >= DateTime.Today.AddDays(-6));   // Last 7 days
            stats[0, 2] = ctx.Stats.Count(s => s.Event == "Message" && s.Meta == "Created" && s.CreatedOn >= DateTime.Today.AddDays(-30));  // LAst 31 dats
            
            // post [1,] - CREATED
            stats[1, 0] = ctx.Stats.Count(s => s.Event == "Post" && s.Meta == "Created" && s.CreatedOn >= DateTime.Today);               // Today
            stats[1, 1] = ctx.Stats.Count(s => s.Event == "Post" && s.Meta == "Created" && s.CreatedOn >= DateTime.Today.AddDays(-6));   // Last 7 days
            stats[1, 2] = ctx.Stats.Count(s => s.Event == "Post" && s.Meta == "Created" && s.CreatedOn >= DateTime.Today.AddDays(-30));  // LAst 31 dats
            
            // users [0,] - LOGGED IN
            stats[2, 0] = ctx.Stats.Count(s => s.Event == "Login" && s.Meta == "Success" && s.CreatedOn >= DateTime.Today);               // Today
            stats[2, 1] = ctx.Stats.Count(s => s.Event == "Login" && s.Meta == "Success" && s.CreatedOn >= DateTime.Today.AddDays(-6));   // Last 7 days
            stats[2, 2] = ctx.Stats.Count(s => s.Event == "Login" && s.Meta == "Success" && s.CreatedOn >= DateTime.Today.AddDays(-30));  // LAst 31 dats
        }

        public void OnGet()
        {
            GetStats();
        }
    }
}
