using MovieApp.EntityLayer.Entities;

namespace MovieApp.BusinessLayer.Abstract
{
    public interface IWatchListService
    {
        WatchList GetById(int id);
        List<WatchList> GetAll();
        void Create(WatchList watchlist);
        void Update(WatchList watchlist);
        void Delete(WatchList watchlist);
        List<WatchList> GetWatchListListByUser(int id);
    }
}
