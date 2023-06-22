using MovieApp.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.DataAccess.Abstract
{
    public interface IWatchListDal: IGenericDal<WatchList>
    {
        //List<WatchList> GetListWithMovie();
        List<WatchList> GetListWithMovieByUser(int id);
    }
}
