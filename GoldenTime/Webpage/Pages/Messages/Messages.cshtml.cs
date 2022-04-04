using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Webpage.EFModel;

namespace Webpage.Pages
{
    public class MessagesModel : PageModel
    {
        private readonly Webpage.EFModel.cosc2650Context _context;

        public MessagesModel(Webpage.EFModel.cosc2650Context context)
        {
            _context = context;
        }

        public IList<PostReqResponses> PostReqResponses { get;set; }

        public async Task OnGetAsync()
        {
            PostReqResponses = await _context.PostReqResponses
                .Include(p => p.PostIdxNavigation)
                .Include(p => p.ResponderIdxNavigation)
                .Include(p => p.ResponseTypeIdxNavigation).ToListAsync();
        }
    }
}
