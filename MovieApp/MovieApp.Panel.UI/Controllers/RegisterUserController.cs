using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MovieApp.EntityLayer.Entities;
using MovieApp.Panel.UI.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
            var captchaImage = HttpContext.Request.Form["g-recaptcha-response"];

            if (string.IsNullOrEmpty(captchaImage))
            {
                ModelState.AddModelError("recaptcha", "Lütfen Google Recaptcha'yı doldurunuz.");
                
            }

            var verified = await checkCaptcha();

            if (!verified)
            {
                ModelState.AddModelError("recaptcha", "Doğrulanamadı.");
                return View(p);
            }
            if (p.TermsOfUse == false)
            {
                ModelState.AddModelError("TermsOfUse", "Kullanım koşullarını kabul etmelisiniz.");
                return View(p);
            }
            //if (verified)
            //{
            //    ModelState.AddModelError("recaptcha", "Başarıyla doğrulandı.");
            //}
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
                    ConfirmCode = code,
                    ProfilePictureUrl = "userDefault.png",
                    ImageUrl= "coverPhotoDefault.jpg",
                    TermsOfUse = p.TermsOfUse
                };

                user.UpdateSlug();
                var  result = await _userManager.CreateAsync(user,p.Password);


                if(result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Member");

                    var email = _configuration["AppSettings:SmtpSettings:Email"];
                    var password = _configuration["AppSettings:SmtpSettings:Password"];

                    MimeMessage mimeMessage = new MimeMessage();
                    MailboxAddress mailboxAddressFrom = new MailboxAddress("SineSözlük Kayıt", "sinesozluk.info@gmail.com");
                    MailboxAddress mailboxAddressTo = new MailboxAddress("User", user.Email);

                    mimeMessage.From.Add(mailboxAddressFrom);
                    mimeMessage.To.Add(mailboxAddressTo);

                    var bodyBuilder = new BodyBuilder();
                    bodyBuilder.TextBody = p.UserName +  " SineSözlüğe hoş geldin! " + "Kayıt işlemini gerçekleştirmek için onay kodunuz: " + code;
                    mimeMessage.Body = bodyBuilder.ToMessageBody();

                    mimeMessage.Subject = "SineSözlük Onay Kodu";
                    
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

        public async Task<bool> checkCaptcha()
        {
            var postData =new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("secret", "6LesIDonAAAAAL9BAdJejEropGEixXYFY2kVNzzz"), 
                new KeyValuePair<string, string>("response", HttpContext.Request.Form["g-recaptcha-response"]) 

            };

            var client = new HttpClient();

            var response = await client.PostAsync("https://www.google.com/recaptcha/api/siteverify", new FormUrlEncodedContent(postData));

            var o = (JObject)JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());

            return (bool)o["success"];
        }
    }
}
