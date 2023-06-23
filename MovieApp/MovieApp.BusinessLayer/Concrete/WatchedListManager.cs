using Microsoft.EntityFrameworkCore;
using MovieApp.BusinessLayer.Abstract;
using MovieApp.DataAccess.Abstract;
using MovieApp.DataAccess.Concrete;
using MovieApp.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.BusinessLayer.Concrete
{
    public class WatchedListManager: IWatchedListService
    {
        IWatchedListDal _watchedlistDal;
        Context c = new Context();

        public WatchedListManager(IWatchedListDal watchedlistDal)
        {
            _watchedlistDal = watchedlistDal;
        }

        public void Create(WatchedList watchedlist)
        {
            _watchedlistDal.Create(watchedlist);
        }

        public void Delete(WatchedList watchedlist)
        {
            _watchedlistDal.Delete(watchedlist);
        }

        public List<WatchedList> GetAll()
        {
            return _watchedlistDal.GetAll();
        }

        public WatchedList GetById(int id)
        {
            return _watchedlistDal.GetById(id);
        }

        public List<WatchedList> GetWatchedListListByUser(int id)
        {
            var watchedlist = c.WatchedLists.Include(b => b.Movie)
                .Include(b => b.AppUser)
                .ToList();
            return watchedlist;
        }
        public List<WatchedList> GetWatchedListWithUser(int Id)
        {


            var watchedList = _watchedlistDal.GetAll(x => x.AppUserId == Id);


            return watchedList;

        }

        public List<WatchedList> GetWatchListListByUser(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(WatchedList watchedlist)
        {
            _watchedlistDal.Update(watchedlist);
        }
    }
}
