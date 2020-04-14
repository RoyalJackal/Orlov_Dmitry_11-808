using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MvcSn.Models;
using MvcSn.Data;
using Microsoft.EntityFrameworkCore;
using MvcSn.Validation;
using Microsoft.AspNetCore.Authorization;
using MvcSn.Sender;

namespace MvcSn
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
            /*services.AddAuthentication("CookieAuthentication")
                 .AddCookie("CookieAuthentication", config =>
                 {
                     config.Cookie.Name = "UserLoginCookie";
                     config.LoginPath = "/Authentication/UserLogin";
                 });*/
           services.AddDbContext<SNContext>(options =>
           options.UseNpgsql(Configuration.GetConnectionString("SNDatabase")));

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<SNContext>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("EditTime", policy =>
                    policy.Requirements.Add(new TimeAccessRequirement()));
            });
            services.AddSingleton<IAuthorizationHandler, PostAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, CommentAuthorizationHandler>();
            bool isDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";
            services.AddSingleton<IMessageSender>(provider =>
            {
                if (isDevelopment) return new EmailSender();
                else return new SmsSender();
            });

            services.AddControllersWithViews();
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
                app.UseExceptionHandler("/Home/Error");
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
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
