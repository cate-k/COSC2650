using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Webpage.EFModel;

namespace Webpage.Pages.Admin.Administer_Posts
{
    public class IndexModel : PageModel
    {
        private readonly IDbContextFactory<cosc2650Context> _contextFactory;
        private readonly Webpage.EFModel.cosc2650Context _context;

        public IndexModel(IDbContextFactory<cosc2650Context> contextFactory)
        {
            _contextFactory = contextFactory;
            _context = _contextFactory.CreateDbContext();
        }

        public IList<Posts> Posts { get;set; }

        public async Task OnGetAsync()
        {
            Posts = await _context.Posts
                .Include(p => p.LocationIdxNavigation)
                .Include(p => p.ParentIdxNavigation)
                .Include(p => p.UserIdxNavigation).ToListAsync();
        }
    }
}
