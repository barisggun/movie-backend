using Microsoft.EntityFrameworkCore;
using MovieApp.DataAccess.Abstract;
using MovieApp.DataAccess.Concrete;
using MovieApp.DataAccess.Repositories;
using MovieApp.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.DataAccess.EntityFramework
{
    public class EfWatchListRepository : GenericRepository<WatchList>, IWatchListDal
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
