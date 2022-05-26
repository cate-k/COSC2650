using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Webpage.EFModel;
using Webpage.Shared;

namespace Webpage.Pages.Account.Register
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
            
            return Page();
        }

        [BindProperty]
        public Users Users { get; set; }

        [BindProperty]
        public Location Locations { get; set; }



        
        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }
            else
            {
                if (!Helper.GetUser(_contextFactory, Users.Email))
                {


                    if (Helper.GetLocationIndexString(_contextFactory, Locations.AreaCode)
                        <= 1)
                    {
                        _context.Location.Add(Locations);
                        await _context.SaveChangesAsync();
                    }


                    Users.LocationIdx =
                    Helper.GetLocationIndexString(_contextFactory, Locations.AreaCode);
                     
                   
                    _context.Users.Add(Users);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "User already registered");
                    return Page();
                }
            }



            return Redirect("/Account/Login");
        }
    }
}
