using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Webpage.EFModel;
using Microsoft.EntityFrameworkCore;
using Webpage.Shared;
using Microsoft.Extensions.Logging;

namespace Webpage.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IDbContextFactory<cosc2650Context> _contextFactory;
        private readonly Webpage.EFModel.cosc2650Context _context;
        int userIdx;
        string userPassword;
        string userEmail;
        int userAdmin;
        string claimTypeName;

        public LoginModel(IDbContextFactory<cosc2650Context> contextFactory)
        {
            _contextFactory = contextFactory;
            _context = _contextFactory.CreateDbContext();
        }


        [BindProperty]
        public Credential Credential { get; set; }
        public void OnGet()
        {
            //pre populate username admin
            //this.Credential = new Credential { UserName = "admin" };

        }

        public async Task<IActionResult> OnPostAsync()
        {
           

            //watch item this.Credential
            if (!ModelState.IsValid) return Page();
            //Verify the credential
            try
            {
                userIdx = Helper.GetUserIndex(_contextFactory,Credential.UserName);
                userEmail = Helper.GetUserEmail(_contextFactory, userIdx);
                userPassword = Helper.GetUserPassword(_contextFactory, userIdx);
                userAdmin = Helper.GetIsAdmin(_contextFactory, userIdx);

            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex,
                    string.Concat("IndexModel:BuildPostModelComplexProperties: ", ex.Message), new object[0]);
            }

            if (userAdmin == 1)
                claimTypeName = "admin";
            else
                claimTypeName = "user";

                
            

            if (Credential.UserName == userEmail && Credential.Password == userPassword)
            {
                //Create security context 
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, claimTypeName),
                    new Claim(ClaimTypes.Email, userEmail)
                 };
                var identity = new ClaimsIdentity(claims, "GoldenCookieAuth");
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("GoldenCookieAuth", claimsPrincipal);

                return RedirectToPage("/Index");
            }
            return Page();
        }
            

    }
    public class Credential
    {
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
