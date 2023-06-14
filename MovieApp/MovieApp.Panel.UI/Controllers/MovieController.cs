using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieApp.BusinessLayer.Concrete;
using MovieApp.DataAccess.Concrete;
using MovieApp.DataAccess.EntityFramework;
using MovieApp.EntityLayer.Entities;
using MovieApp.EntityLayer.Entities.ConnectionClasses;

namespace MovieApp.Panel.UI.Controllers
{

    public class MovieController : Controller
    {

        private readonly IWebHostEnvironment webHostEnvironment;

        MovieManager movieManager = new MovieManager(new EfMovieRepository());
        DirectorManager directorManager = new DirectorManager(new EfDirectorRepository());
        CategoryManager categoryManager = new CategoryManager(new EfCategoryRepository());
        ActorManager actorManager = new ActorManager(new EfActorRepository());
        ActorMovieManager actorMovieManager = new ActorMovieManager(new EfActorMovieRepository());
        DirectorMovieManager directorMovieManager = new DirectorMovieManager(new EfDirectorMovieRepository());
        CategoryMovieManager categoryMovieManager = new CategoryMovieManager(new EfCategoryMovieRepository());

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

        //    ViewBag.Categories = categoryManager.GetAll()
        //.Select(s => new SelectListItem { Value = s.ID.ToString(), Text = s.CategoryName }).ToList();

            List<Actor> actors = actorManager.GetAll();
            ViewBag.Actors = new SelectList(actors, "ID", "ActorName");

            List<Category> categories = categoryManager.GetAll();
            ViewBag.Categories = new SelectList(categories, "ID", "CategoryName");

            return View(new Movie());
        }

        [HttpPost]
        public IActionResult Create(Movie movie, IFormFile file, List<int> selectedActorIds, List<int> selectedDirectorIds, List<int> selectedCategoryIds)
        {

            // Dosya adlarını atamadan önce Poster ve DetailPoster'ı kontrol edin
            if (file != null)
            {
                string wwwrootPath = webHostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                string extension = Path.GetExtension(file.FileName);

                string yeniDosyaAdi = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string yeniDosyaAdiDetay = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwrootPath + "/images/movie/", yeniDosyaAdi);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                movie.Poster = yeniDosyaAdi;
                movie.DetailPoster = yeniDosyaAdiDetay;
            }

            movieManager.Create(movie);

            int movieId = movie.ID;

            foreach (int actorId in selectedActorIds)
            { 
                ActorMovie actorMovie = new ActorMovie { ActorId = actorId, MovieId = movieId };
                actorMovieManager.Create(actorMovie);
            }

            foreach (int categoryId in selectedCategoryIds)
            {
                CategoryMovie categoryMovie = new CategoryMovie { CategoryId = categoryId, MovieId = movieId };
                categoryMovieManager.Create(categoryMovie);
            }

            foreach (int directorId in selectedDirectorIds)
            {
                DirectorMovie directorMovie = new DirectorMovie { DirectorId = directorId, MovieId = movieId };
                directorMovieManager.Create(directorMovie);
            }
            return RedirectToAction("Index");
        }





    }
}
