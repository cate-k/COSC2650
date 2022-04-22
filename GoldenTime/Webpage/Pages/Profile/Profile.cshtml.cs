using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Webpage.EFModel;
using Webpage.Shared;

namespace Webpage.Pages.Profile
{
    public class ProfileModel : PageModel
    {
        //context factory creation
        private readonly IDbContextFactory<cosc2650Context> _contextFactory;

        //lists to store the pulls from DB
        public List<POCO.User> Users;
        public List<POCO.Message> Messages;

        //setting context factory
        public ProfileModel(IDbContextFactory<cosc2650Context> contextFactory)
        {
            _contextFactory = contextFactory;
        }


        //on load sending to view
        public IActionResult OnGet()
        {
            var userId = User.GetUserEmail();
            if (!string.IsNullOrEmpty(userId))
            {
                Users = Helper.GetUsers(_contextFactory, userId );
                Messages = Helper.GetMessages(_contextFactory, userId);
                return Page();
            }
           
            return RedirectToPage("/Account/Login");
        }

        public void OnPost()
        { }



        





    }
}
