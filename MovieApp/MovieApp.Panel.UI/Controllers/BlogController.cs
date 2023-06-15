using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieApp.BusinessLayer.Concrete;
using MovieApp.DataAccess.EntityFramework;
using MovieApp.EntityLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using MovieApp.DataAccess.Concrete;

namespace MovieApp.Panel.UI.Controllers
{
    [Authorize(Roles = "Admin,Writer")]
    public class BlogController : Controller
    {
        //private readonly IWebHostEnvironment webHostEnvironment;
        //private readonly SignInManager<AppUser> _signInManager;

        BlogManager bm = new BlogManager(new EfBlogRepository());
        MovieManager mm = new MovieManager(new EfMovieRepository());
        Context c = new Context();
        private readonly UserManager<AppUser> _userManager;

        public IActionResult Index()
        {
            var values = bm.GetBlogListWithMovie();
            return View(values);
        }
        public IActionResult BlogListByWriter()
        {
            var username = User.Identity.Name;
            //ViewBag.v = usermail;
            var usermail = c.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();
            var userID = c.Users.Where(x => x.Email == usermail).Select(y => y.Id).FirstOrDefault();
            ViewBag.v = userID;
            //var userID = c.Users.Where(x=>x.Email == usermail).Select(y=>y.Id).FirstOrDefault();
            var values = bm.GetListWithMovieByWriterBm(userID);
            return View(values);
        }
        public IActionResult Create()
        {
            List<SelectListItem> movievalues = (from x in mm.GetList()
                                                select new SelectListItem
                                                {
                                                    Text = x.MovieTitle,
                                                    Value = x.ID.ToString()
                                                }).ToList();

            ViewBag.cv = movievalues;
            return View();
        }
        [HttpPost]
        public IActionResult Create(Blog b)
        {
            //BlogValidator bv = new BlogValidator();
            //ValidationResult result = bv.Validate(b);           
            var username = User.Identity.Name;
            var usermail = c.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();
            var userID = c.Users.Where(x => x.Email == usermail).Select(y => y.Id).FirstOrDefault();

            b.BlogStatus = true;
            b.BlogCreateDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            b.AppUserId = userID;
            bm.Create(b);
            return RedirectToAction("BlogListByWriter", "Blog");
        }

        public IActionResult BlogDelete(int id)
        {
            var blogvalue = bm.GetById(id);
            bm.Delete(blogvalue);
            return RedirectToAction("BlogListByWriter");
        }

        public IActionResult BlogEdit(int id)
        {
            var blogvalue = bm.GetById(id);
            List<SelectListItem> movievalues = (from x in mm.GetList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.MovieTitle,
                                                       Value = x.ID.ToString()
                                                   }).ToList();

            ViewBag.cv = movievalues;
            return View(blogvalue);
        }

        [HttpPost]
        public IActionResult BlogEdit(Blog b)
        {
            var username = User.Identity.Name;
            var usermail = c.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();
            var userID = c.Users.Where(x => x.Email == usermail).Select(y => y.Id).FirstOrDefault();
            b.AppUserId = userID;
            b.BlogCreateDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            b.BlogStatus = true;
            bm.Update(b);
            return RedirectToAction("BlogListByWriter");
        }
    }
}
