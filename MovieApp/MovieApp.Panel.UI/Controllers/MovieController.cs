using Microsoft.AspNetCore.Authorization;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using MovieApp.BusinessLayer.Concrete;
using MovieApp.DataAccess.Concrete;
using MovieApp.DataAccess.EntityFramework;
using MovieApp.EntityLayer.Entities;
using MovieApp.EntityLayer.Entities.ConnectionClasses;
using MovieApp.Panel.UI.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

        private readonly IConfiguration configuration;
        private readonly AppSetting _appSettings;

        public MovieController(IConfiguration configuration, IOptions<AppSetting> giphyApiOptions, IWebHostEnvironment webHostEnvironment)
        {
            this.configuration = configuration;
            _appSettings = giphyApiOptions.Value;
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
        public async Task<IActionResult> Detail(int id)
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
                AverageRating = (float)movieValue.AverageRating,
                IsMovieAdded = false
            };

            var username = User.Identity.Name;
            var userId = c.Users.Where(x => x.UserName == username).Select(y => y.Id).FirstOrDefault();
            bool hasAdded = c.WatchLists.Any(x => x.MovieId == id && x.AppUserId == userId);

            var userRating = c.Ratings.FirstOrDefault(x => x.MovieId == id && x.AppUserId == userId);
            if (userRating != null)
            {
                model.UserRating = userRating.Score;
            }
            if (hasAdded)
            {
                model.IsMovieAdded = true;
            }

            string apiKey = configuration.GetValue<string>("AppSettings:GiphyApiKey");
            string movieTitle = model.MovieTitle;
            string apiUrl = $"https://api.giphy.com/v1/gifs/search?q={Uri.EscapeDataString(movieTitle)}&api_key={apiKey}";

            // API'den gelen verileri almak için HTTP isteği yap
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    dynamic data = JObject.Parse(json);

                    // İlk gifi al
                    string gifUrl = data.data[0].images.original.url;

                    // Gifi görüntülemek için CSS stilini güncelle
                    string gifStyle = $"background-image: linear-gradient( 33deg, #13171D 24.97%, #13171D 38.3%, rgba(26, 26, 26, 0.0409746) 97.47%, #13171D 100% ), url({gifUrl});";
                    ViewBag.GifStyle = gifStyle;
                }
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
        public IActionResult AddWatchList(int movieId, MovieDetailModel mdv)
        {
            var username = User.Identity.Name;
            var userId = c.Users.Where(x => x.UserName == username).Select(y => y.Id).FirstOrDefault();

            bool hasAdded = c.WatchLists.Any(x => x.MovieId == movieId && x.AppUserId == userId);
            if (hasAdded)
            {
                TempData["NotificationMessage"] = "Zaten bu film izleme listenizde";
                TempData["NotificationType"] = "error";
                mdv.IsMovieAdded = true;
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
                mdv.IsMovieAdded = false;
                return Json(new { success = true });

            }


        }
        [HttpPost]
        public IActionResult RemoveFromWatchList(int movieId, MovieDetailModel mdv)
        {
            var username = User.Identity.Name;
            var userId = c.Users.Where(x => x.UserName == username).Select(y => y.Id).FirstOrDefault();

            var watchList = c.WatchLists.FirstOrDefault(x => x.MovieId == movieId && x.AppUserId == userId);
            if (watchList != null)
            {
                c.WatchLists.Remove(watchList);
                c.SaveChanges();

                TempData["NotificationMessage"] = "Film, izleme listenizden kaldırıldı.";
                TempData["NotificationType"] = "success";
                mdv.IsMovieAdded = true;
                return Json(new { success = true });
            }
            else
            {
                TempData["NotificationMessage"] = "Film, izleme listenizde bulunamadı.";
                TempData["NotificationType"] = "error";
                mdv.IsMovieAdded = false;
                return Json(new { success = false });
            }
        }

        [HttpPost]
        public IActionResult AddWatchedList(int movieId, MovieDetailModel mdv)
        {
            var username = User.Identity.Name;
            var userId = c.Users.Where(x => x.UserName == username).Select(y => y.Id).FirstOrDefault();

            bool hasAdded = c.WatchedLists.Any(x => x.MovieId == movieId && x.AppUserId == userId);
            if (hasAdded)
            {
                TempData["NotificationMessage"] = "Zaten bu film izledikleriniz arasında";
                TempData["NotificationType"] = "error";
                mdv.IsMovieAddedToWatchedList = true;
                return Json(new { success = false });
            }
            else
            {

                WatchedList newwatchedList = new WatchedList
                {
                    MovieId = movieId,
                    AppUserId = userId
                };


                c.WatchedLists.Add(newwatchedList);
                c.SaveChanges();

                TempData["NotificationMessage"] = "Film, izledikleriniz arasına eklendi.";
                TempData["NotificationType"] = "success";
                mdv.IsMovieAddedToWatchedList = false;
                return Json(new { success = true });

            }


        }
        [HttpPost]
        public IActionResult RemoveFromWatchedList(int movieId, MovieDetailModel mdv)
        {
            var username = User.Identity.Name;
            var userId = c.Users.Where(x => x.UserName == username).Select(y => y.Id).FirstOrDefault();

            var watchedList = c.WatchedLists.FirstOrDefault(x => x.MovieId == movieId && x.AppUserId == userId);
            if (watchedList != null)
            {
                c.WatchedLists.Remove(watchedList);
                c.SaveChanges();

                TempData["NotificationMessage"] = "Film, izledikleriniz arasından kaldırıldı.";
                TempData["NotificationType"] = "success";
                mdv.IsMovieAddedToWatchedList = true;
                return Json(new { success = true });
            }
            else
            {
                TempData["NotificationMessage"] = "Film, izledikleriniz arasında bulunamadı.";
                TempData["NotificationType"] = "error";
                mdv.IsMovieAddedToWatchedList = false;
                return Json(new { success = false });
            }
        }

    }

}

