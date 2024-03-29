﻿using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Options;
using MovieApp.DataAccess.Concrete;
using MovieApp.EntityLayer.Entities;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Humanizer.Configuration;
using CrmUygulamasi.UI.Services;
using Microsoft.AspNetCore.Identity;

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
            //Identity End


            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 4;
                options.Lockout.AllowedForNewUsers = true;
            });




            //Toastr için
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<INotificationService, NotificationService>();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // GiphyApiKey değerini yapılandırmadan alın
            var giphyApiKey = configuration.GetValue<string>("AppSettings:GiphyApiKey");

            //Cookie start
            builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

            builder.Services.AddSession();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                                .RequireAuthenticatedUser()
                                .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });

            builder.Services.AddMvc();
            builder.Services.AddAuthentication(
                CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(x =>
                {
                    x.LoginPath = "/Login/Index/";
                }
                );
            //Cookie end

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = new PathString("/Account/AccessDenied");
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

            app.UseAuthentication();
            app.UseSession();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
               
                endpoints.MapControllerRoute(
                    name: "movieDetail",
                    pattern: "Movie/Detail/{slug}",
                    defaults: new { controller = "Movie", action = "Detail" });
                endpoints.MapControllerRoute(
                   name: "movieMoviesByActors",
                   pattern: "Movie/MoviesByActors/{slug}",
                   defaults: new { controller = "Movie", action = "MoviesByActors" });
                endpoints.MapControllerRoute(
                   name: "movieMoviesByDirectors",
                   pattern: "Movie/MoviesByDirectors/{slug}",
                   defaults: new { controller = "Movie", action = "MoviesByDirectors" });
                endpoints.MapControllerRoute(
                   name: "blogBlogReadAll",
                   pattern: "Blog/BlogReadAll/{slug}",
                   defaults: new { controller = "Blog", action = "BlogReadAll" });
                endpoints.MapControllerRoute(
                   name: "userDetail",
                   pattern: "User/Detail/{slug}",
                   defaults: new { controller = "User", action = "Detail" });
                endpoints.MapControllerRoute(
                   name: "userEdit",
                   pattern: "User/Edit/{slug}",
                   defaults: new { controller = "User", action = "Edit" });

                endpoints.MapControllerRoute(
                   name: "default",
                   pattern: "{controller=Main}/{action=Index}/{id?}");


            });

            app.Run();
        }
    }
}