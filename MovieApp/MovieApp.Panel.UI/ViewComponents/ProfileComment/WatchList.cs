using Microsoft.AspNetCore.Mvc;
using MovieApp.BusinessLayer.Concrete;
using MovieApp.DataAccess.Concrete;
using MovieApp.DataAccess.EntityFramework;
using MovieApp.Panel.UI.Models;

namespace MovieApp.Panel.UI.ViewComponents.ProfileComment
{
    public class WatchList : ViewComponent
    {
        WatchListManager wl = new WatchListManager(new EfWatchListRepository());
        Context c = new Context();

        public IViewComponentResult Invoke(int Id)
        {
            var watchListIds = c.WatchLists
                            .Where(w => w.AppUserId == Id)
                            .Select(w => w.ID)
                            .ToList();

            var movies = c.WatchLists
                            .Where(w => watchListIds.Contains(w.ID))
                            .Select(w => w.Movie)
                            .ToList();

            var watchList = new List<ProfileWatchListModel>();

            foreach (var movie in movies)
            {
                var profileWatchListModel = new ProfileWatchListModel
                {
                    MovieId = movie.ID,
                    MoviePoster = movie.Poster,
                    WatchListId = watchListIds
                };

                watchList.Add(profileWatchListModel);
            }

            return View(watchList);
        }
    }
}
