using MovieApp.EntityLayer.Entities;

namespace MovieApp.BusinessLayer.Abstract
{
    public interface IWatchedListService
    {
        WatchedList GetById(int id);
        List<WatchedList> GetAll();
        void Create(WatchedList watchedlist);
        void Update(WatchedList watchedlist);
        void Delete(WatchedList watchedlist);
        List<WatchedList> GetWatchListListByUser(int id);
    }
}
