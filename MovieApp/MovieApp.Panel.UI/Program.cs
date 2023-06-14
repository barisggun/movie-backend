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
         
            //Identity
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<Context>();
            builder.Services.AddIdentity<AppUser, AppRole>(x =>
            {
                x.Password.RequireNonAlphanumeric = false;
            }).AddEntityFrameworkStores<Context>();
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

            builder.Services.AddMvc();
            builder.Services.AddAuthentication(
                CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(x => {
                    x.LoginPath = "/Login/Index/";
                }
                );
            //Cookie end

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

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Admin}/{action=Index}/{id?}");

            app.Run();
        }
    }
}