using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieApp.BusinessLayer.Concrete;
using MovieApp.DataAccess.EntityFramework;

namespace MovieApp.Panel.UI.Controllers
{
    [Authorize(Roles ="Admin,Writer")]
    public class BlogController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        BlogManager bm = new BlogManager(new EfBlogRepository());
        MovieManager movieManager = new MovieManager(new EfMovieRepository());

        public BlogController(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }


        public IActionResult Index()
        {
            var values = bm.GetAll();
            return View(values);
        }


    }
}
