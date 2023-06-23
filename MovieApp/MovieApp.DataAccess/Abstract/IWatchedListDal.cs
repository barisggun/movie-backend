using MovieApp.EntityLayer.Entities;

namespace MovieApp.DataAccess.Abstract
{
    public interface IWatchedListDal : IGenericDal<WatchedList>
    {
        //List<WatchList> GetListWithMovie();
        List<WatchList> GetListWithMovieByUser(int id);
    }
}
