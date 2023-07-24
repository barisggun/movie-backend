using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore.Query.Internal;
using MimeKit;
using MovieApp.BusinessLayer.Concrete;
using MovieApp.DataAccess.Concrete;
using MovieApp.DataAccess.EntityFramework;
using MovieApp.EntityLayer.Entities;
using MovieApp.Panel.UI.Models;

namespace MovieApp.Panel.UI.Controllers
{
    [AllowAnonymous]
    public class MainController : Controller
    {
        private readonly IConfiguration _configuration;
        HomepageCoverManager homepageCoverManager = new HomepageCoverManager(new EfHomepageCoverRepository());
        MovieManager mm = new MovieManager(new EfMovieRepository());
        BlogManager bm = new BlogManager(new EfBlogRepository());
        Context c = new Context();

        public MainController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm) || searchTerm.Trim().Length < 3)
            {
                ViewBag.ErrorMessage = "Lütfen geçerli bir arama terimi girin.";
                
            }

            var filterMovies = c.Movies.Where(m=>m.MovieTitle.Contains(searchTerm) || m.Description.Contains(searchTerm)).ToList();
            var filterBlogs = c.Blogs.Where(m=>m.BlogContent.Contains(searchTerm) || m.BlogTitle.Contains(searchTerm)).ToList();
            var filterUsers = c.Users.Where(m => m.UserName.Contains(searchTerm) || m.NameSurname.Contains(searchTerm)).ToList();

            var searchResults = new SearchResultsModel
            {
                Movies = filterMovies,
                Blogs = filterBlogs,
                Users = filterUsers
            };

            return View(searchResults);
        }

        public IActionResult Contact()
        {


            return View();
        }

        [HttpPost]
        public IActionResult Contact(MailRequest mailRequest)
        {
            var email = _configuration["AppSettings:SmtpSettings:Email"];
            var password = _configuration["AppSettings:SmtpSettings:Password"];

            MimeMessage mimeMessage = new MimeMessage();

            MailboxAddress mailboxAddressFrom = new MailboxAddress("Admin", email);
            mimeMessage.From.Add(mailboxAddressFrom);

            MailboxAddress mailboxAddressTo = new MailboxAddress("User", email);
            mimeMessage.To.Add(mailboxAddressTo);

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = "Bu mesaj SineSözlük.com iletişim formundan gönderilmiştir." + "\n\nGönderen kişinin mail adresi: " + mailRequest.SenderMail + "\nGönderen kişinin adı: " +  mailRequest.Name + "\n\n\nGönderen kişinin mesajı: " + mailRequest.Body;
            mimeMessage.Body = bodyBuilder.ToMessageBody();
            mimeMessage.Subject = mailRequest.Subject;

            ISmtpClient client = new MailKit.Net.Smtp.SmtpClient();
            client.Connect("smtp.gmail.com", 587, false);
            client.Authenticate(email, password);
            client.Send(mimeMessage);
            client.Disconnect(true);


            return View();
        }

    }
}
