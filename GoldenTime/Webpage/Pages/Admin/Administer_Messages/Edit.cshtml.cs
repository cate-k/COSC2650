using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Webpage.EFModel;

namespace Webpage.Pages.Admin.Administer_Messages
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
        public Messages Messages { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var dbc = _contextFactory.CreateDbContext();
            Messages = await _context.Messages
                .Include(m => m.ParentIdxNavigation)
                .Include(m => m.ReceiverIdxNavigation)
                .Include(m => m.SenderIdxNavigation).FirstOrDefaultAsync(m => m.Idx == id);

            if (Messages == null)
            {
                return NotFound();
            }
           ViewData["ParentIdx"] = new SelectList(_context.Messages, "Idx", "Subject");
           ViewData["ReceiverIdx"] = new SelectList(_context.Users, "Idx", "Email");
           ViewData["SenderIdx"] = new SelectList(_context.Users, "Idx", "Email");
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

            _context.Attach(Messages).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessagesExists(Messages.Idx))
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

        private bool MessagesExists(int id)
        {
            return _context.Messages.Any(e => e.Idx == id);
        }
    }
}
