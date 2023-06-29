using Microsoft.AspNetCore.Mvc;
using MovieApp.BusinessLayer.Concrete;
using MovieApp.DataAccess.Concrete;
using MovieApp.DataAccess.EntityFramework;
using MovieApp.Panel.UI.Models;

namespace MovieApp.Panel.UI.ViewComponents.ProfileComment
{
    public class WatchedList : ViewComponent
    {
        WatchedListManager wl = new WatchedListManager(new EfWatchedListRepository());
        Context c = new Context();

        public IViewComponentResult Invoke(int Id)
        {
            var watchedListIds = c.WatchedLists
                .Where(w => w.AppUserId == Id)
                .Select(w => w.MovieId)
                .ToList();

            var movies = c.Movies
                .Where(m => watchedListIds.Contains(m.ID))
                .ToList();

            var watchedList = new List<ProfileWatchedListModel>();

            foreach (var movie in movies)
            {
                var profileWatchedListModel = new ProfileWatchedListModel
                {
                    MovieId = movie.ID,
                    MoviePoster = movie.Poster,
                    WatchedListId = watchedListIds
                };

                watchedList.Add(profileWatchedListModel);
            }

            return View(watchedList);
        }


    }
}
