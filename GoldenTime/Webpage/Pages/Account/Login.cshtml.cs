using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Webpage.Pages.Account
{
    public class LoginModel : PageModel
    {
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
            //Verify the credential    //to do hook up to database 
            if (Credential.UserName == "admin" && Credential.Password == "password")
            {
                //Create security context 
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, "admin"),
                    new Claim(ClaimTypes.Email, "admin@golden.com")
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
