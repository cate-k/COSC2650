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
        public string Identity;

        //lists to store the pulls from DB
        public List<POCO.User> Users;
        public List<POCO.Message> Messages;

        //setting context factory
        public ProfileModel(IDbContextFactory<cosc2650Context> contextFactory)
        {
            _contextFactory = contextFactory;
        }


        //on load sending to view
        public void OnGet()
        {
            Identity = User.Identity.Name;
            Users = Helper.GetUsers(_contextFactory);
            Messages = Helper.GetMessages(_contextFactory);
        }





    }
}
