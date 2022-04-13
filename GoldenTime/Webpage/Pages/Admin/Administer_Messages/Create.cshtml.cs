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
    public class CreateModel : PageModel
    {

        private readonly IDbContextFactory<cosc2650Context> _contextFactory;
        private readonly Webpage.EFModel.cosc2650Context _context;

        public CreateModel(IDbContextFactory<cosc2650Context> contextFactory)
        {
            _contextFactory = contextFactory;
            _context = _contextFactory.CreateDbContext();
        }

        public IActionResult OnGet()
        {
        ViewData["ParentIdx"] = new SelectList(_context.Messages, "Idx", "Subject");
        ViewData["ReceiverIdx"] = new SelectList(_context.Users, "Idx", "Email");
        ViewData["SenderIdx"] = new SelectList(_context.Users, "Idx", "Email");
            return Page();
        }

        [BindProperty]
        public Messages Messages { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Messages.Add(Messages);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
