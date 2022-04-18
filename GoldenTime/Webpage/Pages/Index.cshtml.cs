using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Webpage.EFModel;
using Webpage.Shared;

namespace Webpage.Pages
{
    public class IndexModel : PageModel
    {

        private readonly ILogger<IndexModel> _logger;
        private readonly IDbContextFactory<cosc2650Context> _contextFactory;
        public string Identity;
        
        
        // This is definitely not best approach, but not going into full binding
        // of the complex models back from model, so just query and regenerate each
        // time.
        private void BuildPostModelComplexProperties()
        {
            try
            {
                // Categories
                Helper.BuildCategories(_contextFactory).ToList();
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex,
                    string.Concat("IndexModel:BuildPostModelComplexProperties: ", ex.Message), new object[0]);
            }
        }
        
        public IndexModel(ILogger<IndexModel> logger, IDbContextFactory<cosc2650Context> contextFactory)
        {
            _logger = logger;
            _contextFactory = contextFactory;

            BuildPostModelComplexProperties();
        }
    }
}
