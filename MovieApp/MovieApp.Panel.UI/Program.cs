using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using MovieApp.DataAccess.Concrete;
using MovieApp.EntityLayer.Entities;

namespace MovieApp.Panel.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Identity Start
            builder.Services.AddDbContext<Context>();
            builder.Services.AddIdentity<AppUser, AppRole>(x =>
            {
                x.Password.RequireNonAlphanumeric = false;
            }).AddEntityFrameworkStores<Context>();
            builder.Services.AddHttpContextAccessor();
            //Identity End


            //Cookie start
            builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
            builder.Services.AddSession();
            builder.Services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });

            builder.Services.ConfigureApplicationCookie(opts =>
            {
                //Cookie settings
                opts.Cookie.HttpOnly = true;
                opts.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                opts.AccessDeniedPath = new PathString("/Login/AccessDenied"); // eriþimin reddedildiði durumda gitmesi gerek yer
                opts.AccessDeniedPath = new PathString("/Login/AccessDenied/");
                opts.LoginPath = "/Login/Index/";
                opts.SlidingExpiration = true;
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication(); // üyeyi kontol eder
            app.UseAuthorization(); // yetkiyi konrol eder  

            app.UseSession();

         

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Admin}/{action=Index}/{id?}");

            app.Run();
        }
    }
}