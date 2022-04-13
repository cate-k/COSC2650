using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Webpage.EFModel;

namespace Webpage.Pages.Admin.Administer_Messages
{
    public class IndexModel : PageModel
    {
        private readonly IDbContextFactory<cosc2650Context> _contextFactory;


        public IndexModel(IDbContextFactory<cosc2650Context> contextFactory)
        {
     
            _contextFactory = contextFactory;
     
        }
        
        public IList<Messages> Messages { get;set; }

        public async Task OnGetAsync()
        {
            using (var dbc = _contextFactory.CreateDbContext())
                Messages = await dbc.Messages
                .Include(m => m.ParentIdxNavigation)
                .Include(m => m.ReceiverIdxNavigation)
                .Include(m => m.SenderIdxNavigation).ToListAsync();
        }
    }
}
