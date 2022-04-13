using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Webpage.EFModel;

namespace Webpage.Pages.Admin.Administer_Users
{
    public class DetailsModel : PageModel
    {
        private readonly IDbContextFactory<cosc2650Context> _contextFactory;
        private readonly Webpage.EFModel.cosc2650Context _context;

        public DetailsModel(IDbContextFactory<cosc2650Context> contextFactory)
        {
            _contextFactory = contextFactory;
            _context = _contextFactory.CreateDbContext();
        }

        public Users Users { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Users = await _context.Users
                .Include(u => u.LocationIdxNavigation).FirstOrDefaultAsync(m => m.Idx == id);

            if (Users == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
