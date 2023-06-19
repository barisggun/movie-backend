using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieApp.BusinessLayer.Concrete;
using MovieApp.DataAccess.Concrete;
using MovieApp.DataAccess.EntityFramework;
using MovieApp.EntityLayer.Entities;

namespace MovieApp.Panel.UI.Controllers
{
    [Authorize(Roles = "Admin,Writer")]
    public class AdminController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        HomepageCoverManager homepageCoverManager = new HomepageCoverManager(new EfHomepageCoverRepository());
        UserManager userManager = new UserManager(new EfUserRepository());

        public AdminController(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }

        [Authorize(Roles = "Admin,Writer")]
        public IActionResult Index()
        {
            Context c = new Context();

            var username = User.Identity.Name;
            var userID = c.Users.Where(x => x.UserName == username).Select(y => y.Id).FirstOrDefault();
            var user = userManager.GetById(userID);

            var blogCount = c.Blogs.Where(x => x.AppUserId == user.Id).Count();

            ViewBag.v2 = blogCount.ToString();
            ViewBag.v1 = c.Blogs.Count().ToString();
            ViewBag.v3 = c.Movies.Count().ToString();
            ViewBag.v4 = c.Users.Count().ToString();

            return View();
        }


        public PartialViewResult AdminNavbarPartial()
        {
            return PartialView();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult CreateCover(int id)
        {
            var values = homepageCoverManager.GetById(1);
            return View(values);  
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult CreateCover(HomepageCover homepageCover, IFormFile file)
        {

                homepageCover.ID = 1;
                homepageCover.ImageUrl = "";
                if (file != null)
                {
                    string wwwrootPath = webHostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                    string extension = Path.GetExtension(file.FileName);

                    string yeniDosyaAdi = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwrootPath + "/images/movie/", yeniDosyaAdi);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    homepageCover.ImageUrl = yeniDosyaAdi;
                }


                homepageCoverManager.Update(homepageCover);

                return RedirectToAction("Index");

        }

        


    }
}
