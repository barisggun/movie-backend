using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApp.BusinessLayer.Concrete;
using MovieApp.DataAccess.Concrete;
using MovieApp.DataAccess.EntityFramework;

namespace MovieApp.Panel.UI.ViewComponents.Main
{
    public class MostLiked:ViewComponent
    {
        MovieManager mm = new MovieManager(new EfMovieRepository());
        Context c = new Context();
        public IViewComponentResult Invoke()
        {
            var values = c.Movies.Include(m => m.Directors).OrderByDescending(m => m.AverageRating).Take(3).ToList();
            return View(values);
        }
    }
}
