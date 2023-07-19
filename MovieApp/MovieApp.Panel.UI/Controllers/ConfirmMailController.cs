using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MovieApp.BusinessLayer.Concrete;
using MovieApp.EntityLayer.Entities;
using MovieApp.Panel.UI.Models;

namespace MovieApp.Panel.UI.Controllers
{
    [AllowAnonymous]
    public class ConfirmMailController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public ConfirmMailController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var value = TempData["UserName"];
            ViewBag.v = value;
            //confirmMailViewModel.Mail = value.ToString();
            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> Index(ConfirmMailViewModel confirmMailViewModel)
        //{
        //    var user = await _userManager.FindByNameAsync(confirmMailViewModel.UserName);
        //  if(user.ConfirmCode==confirmMailViewModel.ConfirmCode)
        //    {
        //        user.EmailConfirmed = true;
        //        await _userManager.UpdateAsync(user);
        //        return RedirectToAction("Login", "Account");
        //    }
        //    return View(confirmMailViewModel.UserName);
        //}



        [HttpPost]
        public async Task<IActionResult> Index(ConfirmMailViewModel confirmMailViewModel)
        {
            var user = await _userManager.FindByNameAsync(confirmMailViewModel.UserName);

            if (user != null && user.ConfirmCode == confirmMailViewModel.ConfirmCode)
            {
                user.EmailConfirmed = true;
                await _userManager.UpdateAsync(user);

                TempData["AccountActivated"] = "Hesabınız başarıyla aktifleştirildi.";
                return RedirectToAction("Login", "Account");
                //return RedirectToAction("VerificationSuccessful", "ConfirmMail");
            }
            else
            {
                ModelState.AddModelError("ConfirmCode", "Yanlış doğrulama kodu girdiniz.");

                ViewBag.v = confirmMailViewModel.UserName;

                return View(confirmMailViewModel);
            }
 
        }

        public IActionResult VerificationSuccessful()
        {
            return View();
        }


    }
}
