using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieApp.BusinessLayer.Concrete;
using MovieApp.DataAccess.EntityFramework;
using MovieApp.EntityLayer.Entities;

namespace MovieApp.Panel.UI.Controllers
{
    [AllowAnonymous]
    public class AdminController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        HomepageCoverManager homepageCoverManager = new HomepageCoverManager(new EfHomepageCoverRepository());

        public AdminController(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public PartialViewResult AdminNavbarPartial()
        {
            return PartialView();
        }

        [HttpGet]
        public IActionResult CreateCover()
        {
            var homepageCover = homepageCoverManager.GetAll().FirstOrDefault();
            return View(homepageCover);  
        }

        [HttpPost]
        public IActionResult CreateCover(HomepageCover homepageCover, IFormFile file)
        {
            homepageCover.ImageUrl = "";
            if (file != null)
            {
                string wwwrootPath = webHostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                string extension = Path.GetExtension(file.FileName);

                string yeniDosyaAdi = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwrootPath + "/images/homepage/", yeniDosyaAdi);

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
