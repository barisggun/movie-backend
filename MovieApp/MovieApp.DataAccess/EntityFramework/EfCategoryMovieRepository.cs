using MovieApp.DataAccess.Abstract;
using MovieApp.DataAccess.Repositories;
using MovieApp.EntityLayer.Entities.ConnectionClasses;

namespace MovieApp.DataAccess.EntityFramework
{
    public class EfCategoryMovieRepository : NoGenericRepository<CategoryMovie>, ICategoryMovieDal
    {

    }
}

