using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MovieApp.DataAccess.Concrete;
using MovieApp.EntityLayer.Entities;
using MovieApp.Panel.UI.Models;

namespace MovieApp.Panel.UI.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _configuration;
        Context c = new Context();

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserSignInViewModel p)
        {
            if (ModelState.IsValid)
            {
                Random random = new Random();
                int code;
                code = random.Next(100000, 1000000);

                var user = await _userManager.FindByNameAsync(p.username);
                if (user != null)
                {
                    // Kontrol edilen kullanıcıya ait email doğrulama durumunu kontrol edin
                    if (!user.EmailConfirmed)
                    {

                        user.ConfirmCode = code;

                        var email = _configuration["AppSettings:SmtpSettings:Email"];
                        var password = _configuration["AppSettings:SmtpSettings:Password"];

                        MimeMessage mimeMessage = new MimeMessage();
                        MailboxAddress mailboxAddressFrom = new MailboxAddress("Film Küresi Admin", "filmcapella@gmail.com");
                        MailboxAddress mailboxAddressTo = new MailboxAddress("User", user.Email);

                        mimeMessage.From.Add(mailboxAddressFrom);
                        mimeMessage.To.Add(mailboxAddressTo);

                        var bodyBuilder = new BodyBuilder();
                        bodyBuilder.TextBody = "Kayıt işlemini gerçekleştirmek için onay kodunuz: " + code;
                        mimeMessage.Body = bodyBuilder.ToMessageBody();

                        mimeMessage.Subject = "Film Küresi Onay Kodu";

                        ISmtpClient client = new MailKit.Net.Smtp.SmtpClient();
                        client.Connect("smtp.gmail.com", 587, false);
                        client.Authenticate(email, password);
                        client.Send(mimeMessage);
                        client.Disconnect(true);

                        await _userManager.UpdateAsync(user);

                        TempData["UserName"] = p.username;
                        return RedirectToAction("Index", "ConfirmMail");
                    }

                    var result = await _signInManager.PasswordSignInAsync(p.username, p.password, false, true);
                    if (result.Succeeded)
                    {
                        var roles = await _userManager.GetRolesAsync(user);
                        if (roles.Contains("Admin"))
                        {
                            return RedirectToAction("Index", "Admin");
                        }
                        else if (roles.Contains("Member"))
                        {
                            return RedirectToAction("Index", "Main");
                        }
                        else if (roles.Contains("Writer"))
                        {
                            return RedirectToAction("Index", "Admin");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("password", "Kullanıcı adı veya şifre hatalı tekrar deneyiniz.");
                        return View(p);
                    }
                }
                else
                {
                    ModelState.AddModelError("noFoundUser", "Kullanıcı adı veya e-posta adresi bulunamadı.");
                    return View(p);
                }
            }


            return View();
        }


        public async Task<IActionResult> Logout()
        {
            //HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Main");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}
