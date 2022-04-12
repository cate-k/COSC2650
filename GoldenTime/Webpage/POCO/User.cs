using System;

namespace Webpage.POCO
{
    public class User
    {
        public int Idx { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public int? Age { get; set; }
        public int? LocationIdx { get; set; }
        public string Mobile { get; set; }

        public User()
        {

        }

        public static User ToPOCO(EFModel.Users User)
        {
            return new User()
            {
                 Idx = User.Idx,
                 Email = User.Email,
                 FullName = User.FullName,
                 Age = User.Age,
                 LocationIdx = User.LocationIdx,
                 Mobile = User.Mobile
            };
        }       
    }
}