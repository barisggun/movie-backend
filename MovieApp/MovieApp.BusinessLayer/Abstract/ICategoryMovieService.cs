using MovieApp.EntityLayer.Entities.ConnectionClasses;

namespace MovieApp.BusinessLayer.Abstract
{
    public interface ICategoryMovieService
    {
        CategoryMovie GetById(int id);
        List<CategoryMovie> GetAll();
        void Create(CategoryMovie categoryMovie);
        void Update(CategoryMovie categoryMovie);
        void Delete(CategoryMovie categoryMovie);
    }
}
