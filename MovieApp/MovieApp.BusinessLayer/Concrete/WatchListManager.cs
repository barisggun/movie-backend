using Microsoft.EntityFrameworkCore;
using MovieApp.BusinessLayer.Abstract;
using MovieApp.DataAccess.Abstract;
using MovieApp.DataAccess.Concrete;
using MovieApp.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.BusinessLayer.Concrete
{
    public class WatchListManager: IWatchListService
    {
        IWatchListDal _watchlistDal;
        Context c = new Context();

        public WatchListManager(IWatchListDal watchlistDal)
        {
            _watchlistDal = watchlistDal;
        }

        public void Create(WatchList watchlist)
        {
            _watchlistDal.Create(watchlist);
        }

        public void Delete(WatchList watchlist)
        {
            _watchlistDal.Delete(watchlist);
        }

        public List<WatchList> GetAll()
        {
            return _watchlistDal.GetAll();
        }

        public WatchList GetById(int id)
        {
            return _watchlistDal.GetById(id);
        }

        public List<WatchList> GetWatchListListByUser(int id)
        {
            var watchlist = c.WatchLists.Include(b => b.Movie)
                .Include(b => b.AppUser)
                .ToList();
            return watchlist;
        }

        public void Update(WatchList watchlist)
        {
            _watchlistDal.Update(watchlist);
        }
    }
}
