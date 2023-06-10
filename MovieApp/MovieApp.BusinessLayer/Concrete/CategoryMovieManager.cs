using MovieApp.BusinessLayer.Abstract;
using MovieApp.DataAccess.Abstract;
using MovieApp.EntityLayer.Entities.ConnectionClasses;

namespace MovieApp.BusinessLayer.Concrete
{
    public class CategoryMovieManager : ICategoryMovieService
    {
        private ICategoryMovieDal _categoryMovieDal;

        public CategoryMovieManager(ICategoryMovieDal categoryMovieDal)
        {
            _categoryMovieDal = categoryMovieDal;
        }

        public void Create(CategoryMovie categoryMovie)
        {
            _categoryMovieDal.Create(categoryMovie);
        }

        public void Delete(CategoryMovie categoryMovie)
        {
            _categoryMovieDal.Delete(categoryMovie);
        }

        public List<CategoryMovie> GetAll()
        {
            return _categoryMovieDal.GetAll();
        }

        public CategoryMovie GetById(int id)
        {
            return _categoryMovieDal.GetById(id);
        }

        public void Update(CategoryMovie categoryMovie)
        {
            _categoryMovieDal.Update(categoryMovie);
        }
    }
}
