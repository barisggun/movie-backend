using Microsoft.EntityFrameworkCore;
using MovieApp.DataAccess.Abstract;
using MovieApp.DataAccess.Concrete;
using MovieApp.DataAccess.Repositories;
using MovieApp.EntityLayer.Entities;

namespace MovieApp.DataAccess.EntityFramework
{
    public class EfWatchedListRepository : GenericRepository<WatchedList>, IWatchedListDal
    {
        public List<WatchList> GetListWithMovieByUser(int id)
        {
            using (var c = new Context())
            {
                return c.WatchLists.Include(x => x.Movie).Where(x => x.AppUserId == id).ToList();
            }
        }
    }
}
