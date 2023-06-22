using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieApp.BusinessLayer.Concrete;
using MovieApp.DataAccess.Concrete;
using MovieApp.DataAccess.EntityFramework;
using MovieApp.EntityLayer.Entities;
using MovieApp.EntityLayer.Entities.ConnectionClasses;
using MovieApp.Panel.UI.Models;
using static System.Formats.Asn1.AsnWriter;

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
        WatchListManager WatchListManager = new WatchListManager(new EfWatchListRepository());
        Context c = new Context();


        public List<DirectorMovie> DirectorMovies { get; set; } = new List<DirectorMovie>();
        public List<ActorMovie> ActorMovies { get; set; } = new List<ActorMovie>();


        public MovieController(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            List<Movie> list = movieManager.GetAll();
            return View(list);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Directors = directorManager.GetAll()
        .Select(s => new SelectListItem { Value = s.ID.ToString(), Text = s.DirectorName }).ToList();

            List<Actor> actors = actorManager.GetAll();
            ViewBag.Actors = new SelectList(actors, "ID", "ActorName");

            List<Category> categories = categoryManager.GetAll();
            ViewBag.Categories = new SelectList(categories, "ID", "CategoryName");

            return View(new Movie());
        }
        [Authorize(Roles = "Admin")]
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
            movie.AverageRating = 0;
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

        [AllowAnonymous]
        public IActionResult Detail(int id)
        {
            ViewBag.i = id;
            var movieValue = movieManager.GetById(id);

            var model = new MovieDetailModel
            {
                MovieId = movieValue.ID,
                DirectorNames = movieValue.Directors.Select(d => d.DirectorName).ToList(),
                ActorNames = movieValue.Actors.Select(a => a.ActorName).ToList(),
                MovieTitle = movieValue.MovieTitle,
                MoviePoster = movieValue.Poster,
                MovieDetailPoster = movieValue.DetailPoster,
                MovieDescription = movieValue.Description,
                ReleaseDate = movieValue.ReleaseDate,
                AverageRating = (float)movieValue.AverageRating
            };


            var username = User.Identity.Name;
            var userId = c.Users.Where(x => x.UserName == username).Select(y => y.Id).FirstOrDefault();

            var userRating = c.Ratings.FirstOrDefault(x => x.MovieId == id && x.AppUserId == userId);
            if (userRating != null)
            {
                model.UserRating = userRating.Score;
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult SaveRating(int movieId, int score)
        {

            var username = User.Identity.Name;
            var userId = c.Users.Where(x => x.UserName == username).Select(y => y.Id).FirstOrDefault();


            bool hasRated = c.Ratings.Any(x => x.MovieId == movieId && x.AppUserId == userId);

            if (hasRated)
            {
                TempData["NotificationMessage"] = "Zaten bu filme oy verdiniz.";
                TempData["NotificationType"] = "error";
                return Json(new { success = false });
            }
            else
            {

                Rating newRating = new Rating
                {
                    MovieId = movieId,
                    AppUserId = userId,
                    Score = score
                };


                c.Ratings.Add(newRating);
                c.SaveChanges();


                var movie = c.Movies.FirstOrDefault(x => x.ID == movieId);
                var ratings = c.Ratings.Where(x => x.MovieId == movieId).Select(x => x.Score).ToList();
                var averageRating = ratings.Count > 0 ? ratings.Average() : 0;

                movie.AverageRating = (float?)averageRating;
                c.SaveChanges();

                TempData["NotificationMessage"] = "Oyunuz başarıyla kaydedildi.";
                TempData["NotificationType"] = "success";

                return Json(new { success = true, averageRating });
            }
        }

        [HttpPost]
        public IActionResult AddWatchList(int movieId)
        {
            var username = User.Identity.Name;
            var userId = c.Users.Where(x => x.UserName == username).Select(y => y.Id).FirstOrDefault();

            bool hasAdded = c.WatchLists.Any(x => x.MovieId == movieId && x.AppUserId == userId);
            if (hasAdded)
            {
                TempData["NotificationMessage"] = "Zaten bu film izleme listenizde";
                TempData["NotificationType"] = "error";
                return Json(new { success = false });
            }
            else
            {

                WatchList newwatchList = new WatchList
                {
                    MovieId = movieId,
                    AppUserId = userId
                };


                c.WatchLists.Add(newwatchList);
                c.SaveChanges();

                TempData["NotificationMessage"] = "Film, izleme listenize eklendi.";
                TempData["NotificationType"] = "success";

                return Json(new { success = true });

            }


        }

    }

}

