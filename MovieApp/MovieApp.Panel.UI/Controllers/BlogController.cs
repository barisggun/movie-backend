using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieApp.BusinessLayer.Concrete;
using MovieApp.DataAccess.EntityFramework;
using MovieApp.EntityLayer.Entities;
using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace MovieApp.Panel.UI.Controllers
{
    [Authorize(Roles = "Admin,Writer")]
    public class BlogController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly SignInManager<AppUser> _signInManager;

        BlogManager bm = new BlogManager(new EfBlogRepository());
        MovieManager movieManager = new MovieManager(new EfMovieRepository());
        
        UserManager userManager = new UserManager(new EfUserRepository());



        public IActionResult Index()
        {
           var values = bm.GetAll();
            return View(values);    
        }


        [HttpGet]
        public IActionResult Create()
        { 
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Blog p)
        {
            p.BlogStatus = true;
            p.BlogCreateDate = DateTime.Parse(DateTime.Now.ToShortDateString());

            var user = await _signInManager.UserManager.GetUserAsync(User);
            p.AppUserId = user.Id;

            bm.Create(p);
            return RedirectToAction("Index", "Blog");
        }


    }
}
