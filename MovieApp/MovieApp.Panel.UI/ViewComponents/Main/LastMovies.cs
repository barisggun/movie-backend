using Microsoft.AspNetCore.Mvc;
using MovieApp.BusinessLayer.Concrete;
using MovieApp.DataAccess.EntityFramework;

namespace MovieApp.Panel.UI.ViewComponents.Main
{
    public class LastMovies: ViewComponent
    {
        MovieManager mm = new MovieManager(new EfMovieRepository());
        public IViewComponentResult Invoke()
        {
            var values = mm.GetAll().Take(6).ToList();
            values.Reverse();
            return View(values);
        }
    }
}
