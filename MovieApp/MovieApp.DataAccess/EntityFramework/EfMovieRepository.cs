using MovieApp.DataAccess.Abstract;
using MovieApp.DataAccess.Repositories;
using MovieApp.EntityLayer.Entities;

namespace MovieApp.DataAccess.EntityFramework
{
    public class EfMovieRepository : GenericRepository<Movie>, IMovieDal
    {
    }
}
