using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieApp.BusinessLayer.Concrete;
using MovieApp.DataAccess.Concrete;
using MovieApp.DataAccess.EntityFramework;
using MovieApp.EntityLayer.Entities;

namespace MovieApp.Panel.UI.Controllers
{
    public class MovieController : Controller
    {

        private readonly IWebHostEnvironment webHostEnvironment;

        MovieManager movieManager = new MovieManager(new EfMovieRepository());
        DirectorManager directorManager = new DirectorManager(new EfDirectorRepository());
        CategoryManager categoryManager = new CategoryManager(new EfCategoryRepository());

        public MovieController(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<Movie> list = movieManager.GetAll();
            return View(list);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Directors = directorManager.GetAll()
        .Select(s => new SelectListItem { Value = s.ID.ToString(), Text = s.DirectorName }).ToList();

            ViewBag.Categories = categoryManager.GetAll()
        .Select(s => new SelectListItem { Value = s.ID.ToString(), Text = s.CategoryName }).ToList();


            return View(new Movie());
        }

        [HttpPost]
        public IActionResult Create(Movie movie, IFormFile file)
        {
            movie.Poster = "";
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

                movie.Poster = yeniDosyaAdi;
            }

            movie.DetailPoster = "";
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

                movie.DetailPoster = yeniDosyaAdi;
            }

            
            movieManager.Create(movie);
            return RedirectToAction("Index");
        }
    }
}
