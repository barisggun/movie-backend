using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieApp.DataAccess.Concrete;
using MovieApp.EntityLayer.Entities;
using MovieApp.Panel.UI.Models;

namespace MovieApp.Panel.UI.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        Context c = new Context();

        public AccountController(SignInManager<AppUser> signInManager)
        {
            this._signInManager = signInManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserSignInViewModel p)
        {
            if(ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(p.username, p.password, false, true);
                if (result.Succeeded)
                {
                    var username = User.Identity.Name;
                    var userId = c.Users.Where(x => x.UserName == username).Select(y => y.Id).FirstOrDefault();
                    var userRole = c.RoleClaims.Where(x => x.Id == userId).Select(y => y.RoleId).FirstOrDefault();
                    if (userRole == 1)
                    {
                        return RedirectToAction("Index", "Movie");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Main");
                    }
                    
                }
                else
                {
                    ModelState.AddModelError("password", "Kullanıcı adı veya şifre hatalı tekrar deneyiniz."); // Add error message  
                    return View(p); // Return the view with the updated model containing the error

                    //return RedirectToAction("Login", "Account");
                }
            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

    }
}
