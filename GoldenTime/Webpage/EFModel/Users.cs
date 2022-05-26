using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Webpage.EFModel
{
    public partial class Users
    {
        public Users()
        {
            MessagesReceiverIdxNavigation = new HashSet<Messages>();
            MessagesSenderIdxNavigation = new HashSet<Messages>();
            PostReqResponses = new HashSet<PostReqResponses>();
            Posts = new HashSet<Posts>();
            Preferences = new HashSet<Preferences>();
        }

        

        public int Idx { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Must be a valid email")]
        public string Email { get; set; }

        [Required]
        
        public string FullName { get; set; }

        [Required]
        [Range(60, 150,
        ErrorMessage = "You must be older than 60 to join golden times")]
        public int? Age { get; set; }

       
        public int? LocationIdx { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]{10}$",
         ErrorMessage = "Please enter a 10 digit mobile number.")]
    
        public string Mobile { get; set; }


        public int IsAdmin { get; set; }

        [Required]
        public string Password { get; set; }



        public virtual Location LocationIdxNavigation { get; set; }
        public virtual ICollection<Messages> MessagesReceiverIdxNavigation { get; set; }
        public virtual ICollection<Messages> MessagesSenderIdxNavigation { get; set; }
        public virtual ICollection<PostReqResponses> PostReqResponses { get; set; }
        public virtual ICollection<Posts> Posts { get; set; }
        public virtual ICollection<Preferences> Preferences { get; set; }
    }
}
