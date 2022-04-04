using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Webpage.EFModel;

namespace Webpage.Pages.Messages
{
    public class Messages_CreateModel : PageModel
    {
        private readonly Webpage.EFModel.cosc2650Context _context;

        public Messages_CreateModel(Webpage.EFModel.cosc2650Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["PostIdx"] = new SelectList(_context.Posts, "Idx", "Subject");
        ViewData["ResponderIdx"] = new SelectList(_context.Users, "Idx", "Email");
        ViewData["ResponseTypeIdx"] = new SelectList(_context.Response, "Idx", "Caption");
            return Page();
        }

        [BindProperty]
        public PostReqResponses PostReqResponses { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.PostReqResponses.Add(PostReqResponses);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
