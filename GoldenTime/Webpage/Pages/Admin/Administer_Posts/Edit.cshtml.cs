using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Webpage.EFModel;

namespace Webpage.Pages.Admin.Administer_Posts
{
    public class EditModel : PageModel
    {
        private readonly IDbContextFactory<cosc2650Context> _contextFactory;
        private readonly Webpage.EFModel.cosc2650Context _context;

        public EditModel(IDbContextFactory<cosc2650Context> contextFactory)
        {
            _contextFactory = contextFactory;
            _context = _contextFactory.CreateDbContext();
        }

        [BindProperty]
        public Posts Posts { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Posts = await _context.Posts
                .Include(p => p.LocationIdxNavigation)
                .Include(p => p.ParentIdxNavigation)
                .Include(p => p.UserIdxNavigation).FirstOrDefaultAsync(m => m.Idx == id);

            if (Posts == null)
            {
                return NotFound();
            }
           ViewData["LocationIdx"] = new SelectList(_context.Location, "Idx", "AreaCode");
           ViewData["ParentIdx"] = new SelectList(_context.Posts, "Idx", "Subject");
           ViewData["UserIdx"] = new SelectList(_context.Users, "Idx", "Email");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Posts).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostsExists(Posts.Idx))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PostsExists(int id)
        {
            return _context.Posts.Any(e => e.Idx == id);
        }
    }
}
