using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Webpage.POCO;

namespace Webpage.PageObjects.Post
{
    public class Post
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Id { get; set; }
        [Range(1000, 9999)]
        public int PostCode { get; set; }
        [ValidateNever]
        public DateTime? StartsOn { get; set; }
        [ValidateNever]
        public DateTime? EndsOn { get; set; }
        [ValidateNever]
        public IFormFile AttachedFile { get; set; }

        public Post()
        {
        }
    }
}

