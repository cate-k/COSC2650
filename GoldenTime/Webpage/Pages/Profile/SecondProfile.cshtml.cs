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
    public class SecondProfileModel : PageModel
    {

        //context factory creation
        private readonly IDbContextFactory<cosc2650Context> _contextFactory;
        public string Identity;
        string loggedIn = "no";

        //lists to store the pulls from DB
        public List<POCO.User> Users;
        public List<POCO.Message> Messages;

        //setting context factory
        public SecondProfileModel(IDbContextFactory<cosc2650Context> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public void OnGet()
        {
           
        }

        public void OnPost()
        {

            //Checking if the users current location exists in the DB
            string v = Request.Form["location"].ToString();
            var connection = _contextFactory.CreateDbContext();
            var locationObject = connection.Location
                .Where(i => i.AreaCode == v)
                .FirstOrDefault();

            //v
            var userObj = connection.Users
                .Where(i => i.Email == User.GetUserEmail()) //this is where the session loggedin token goes
                .FirstOrDefault();

            try
            {
                //Checking if the users current location exists in the DB
                if (locationObject == null)
                {
                    //if it doesnt create new location and add it to user
                    var newLocation = new EFModel.Location
                    {
                        AreaCode = Request.Form["location"],
                        Country = Request.Form["country"],
                        Caption = Request.Form["locationCaption"],
                        Description = Request.Form["locationDescription"],
                        State = Request.Form["locationState"]


                    };

                    connection.Location.Add(newLocation);
                    userObj.LocationIdxNavigation = newLocation;
                }

                try
                {
                    int x = Convert.ToInt32(Request.Form["age"]);
                    userObj.Age = x;
                }
                catch (Exception)
                {
                }

                try
                {
                    userObj.FullName = Request.Form["fullName"];
                }
                catch (Exception)
                {
                }

                try
                {
                    userObj.Mobile = Request.Form["mobile"];
                }
                catch (Exception)
                {
                }

                //connection.Update(userObj);
                connection.SaveChanges();

            }
            catch (Exception ex)
            {
            }
       }
    }
}
