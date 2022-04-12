using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Webpage.EFModel;
using Webpage.Shared;

namespace Webpage.Pages.MessagePages
{
    public class ViewMessagesModel : PageModel
    {
        private readonly IDbContextFactory<cosc2650Context> _contextFactory;
        public string Identity;

        public List<POCO.Message> Messages;

        public ViewMessagesModel(IDbContextFactory<cosc2650Context> contextFactory)
        {
            _contextFactory = contextFactory;
        }
              

        public void OnGet()
        {
            Identity = User.Identity.Name;
            Messages = Helper.GetMessages(_contextFactory);
        }
    }
}
