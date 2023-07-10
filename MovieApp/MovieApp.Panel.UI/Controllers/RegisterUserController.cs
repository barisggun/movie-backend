using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MovieApp.EntityLayer.Entities;
using MovieApp.Panel.UI.Models;
using System.Configuration;
using System.Net.Mail;

namespace MovieApp.Panel.UI.Controllers
{
    [AllowAnonymous]
    public class RegisterUserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;
        public RegisterUserController(UserManager<AppUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(UserSignUpViewModel p)
        {   
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByNameAsync(p.UserName);
                if (existingUser != null)
                {
                    ModelState.AddModelError("signupError", "Kullanıcı adı zaten kayıtlı.");
                    return View(p);
                }

                existingUser = await _userManager.FindByEmailAsync(p.Mail);
                if (existingUser != null)
                {
                    ModelState.AddModelError("signupError", "E-posta adresi zaten kayıtlı.");
                    return View(p);
                }

                Random random = new Random();
                int code;
                code = random.Next(100000, 1000000);

                AppUser user = new AppUser()
                {
                    Email = p.Mail,
                    UserName = p.UserName,
                    NameSurname = p.NameSurname,
                    ConfirmCode = code
                };

                var  result = await _userManager.CreateAsync(user,p.Password);


                if(result.Succeeded)
                {
                    var email = _configuration["AppSettings:SmtpSettings:Email"];
                    var password = _configuration["AppSettings:SmtpSettings:Password"];

                    MimeMessage mimeMessage = new MimeMessage();
                    MailboxAddress mailboxAddressFrom = new MailboxAddress("Film Küresi Admin", "filmcapella@gmail.com");
                    MailboxAddress mailboxAddressTo = new MailboxAddress("User", user.Email);

                    mimeMessage.From.Add(mailboxAddressFrom);
                    mimeMessage.To.Add(mailboxAddressTo);

                    var bodyBuilder = new BodyBuilder();
                    bodyBuilder.TextBody = p.UserName +  " SineSözlüğe hoş geldin! " + "Kayıt işlemini gerçekleştirmek için onay kodunuz: " + code;
                    mimeMessage.Body = bodyBuilder.ToMessageBody();

                    mimeMessage.Subject = "Film Küresi Onay Kodu";
                    
                    ISmtpClient client  = new MailKit.Net.Smtp.SmtpClient();
                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate(email, password);
                    client.Send(mimeMessage);
                    client.Disconnect(true);

                    TempData["UserName"] = p.UserName;


                    await _userManager.AddToRoleAsync(user, "Member");
                    return RedirectToAction("Index", "ConfirmMail");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("signupError", item.Description);
                    }
                }
            }
            return View(p);
        }
    }
}
