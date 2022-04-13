using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using Webpage.EFModel;

namespace Webpage
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // This is here so the DI with parameter works. Factory is used due to the
            // dbContext scope requirements
            services.AddDbContextFactory<cosc2650Context>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            string[] initialScopes = Configuration.GetValue<string>("UserApiOne:ScopeForAccessToken")?.Split(' ');
/*
            //added by sam 11/04/2022
            services.AddMicrosoftIdentityWebAppAuthentication(Configuration, "AzureAd")
        .EnableTokenAcquisitionToCallDownstreamApi(initialScopes)
        .AddInMemoryTokenCaches();
*/

            services.AddAuthentication("GoldenCookieAuth").AddCookie("GoldenCookieAuth", options=> 
            {
                options.Cookie.Name = "GoldenCookieAuth";
            });
           services.AddRazorPages();
            /*
            //added by sam 11/04/2022
            services.AddRazorPages().AddMvcOptions(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    //.RequireAuthenticatedUser()
                    .Require
                    .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            }).AddMicrosoftIdentityUI();
            */

        } 

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
