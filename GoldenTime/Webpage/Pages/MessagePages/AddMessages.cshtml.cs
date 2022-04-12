using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Webpage.EFModel;
using Webpage.POCO;
using Webpage.Shared;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;


namespace Webpage.Pages.MessagePages
{
    public class AddMessagesModel : PageModel
    {
        private readonly IDbContextFactory<cosc2650Context> _contextFactory;
        private readonly ILogger<IndexModel> _logger;

        public AddMessagesModel(ILogger<IndexModel> logger, IDbContextFactory<cosc2650Context> contextFactory)
        {
            _logger = logger;
            _contextFactory = contextFactory;
        }

        public IActionResult OnGet()
        {

            return Page();
        }

        [BindProperty]
        public Message Message { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();// TODO: Caitlen: CSS should be created so the failed items show the validation text;
            }

            try
            {
                // This is where we inspect the http post, bound properties on the model and save...
                using (var dbc = _contextFactory.CreateDbContext())
                {
                   
                    var m = new EFModel.Messages()
                    {
                        SenderIdx = Helper.GetUserIndex(_contextFactory, "s3739099@student.rmit.edu.au"),
                        ReceiverIdx = Helper.GetUserIndex(_contextFactory, "s3820255@student.rmit.edu.au"), 
                        CreatedOn = DateTime.Now,
                        //ModifiedOn = Message.ModifiedOn, TODO: Need to add this for edit message functionality
                        Subject = Message.Subject,
                        Content = Message.Content,
                        //ParentIdx = Message.ParentIdx, TODO: Need to add this for message collection
                        //so that when a message has a parent message they stack into a coinversation 
                    };

                    dbc.Messages.Add(m);
                    dbc.SaveChanges();
                }
            }

            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, string.Concat("PostModel:OnPost: ", ex.Message), new object[0]);
            }

            return Redirect("ViewMessages");
        }       

    }
   
    
}
