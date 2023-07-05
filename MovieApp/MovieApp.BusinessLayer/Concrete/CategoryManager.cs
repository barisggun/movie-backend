using MovieApp.BusinessLayer.Abstract;
using MovieApp.DataAccess.Abstract;
using MovieApp.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.BusinessLayer.Concrete
{
    public class CategoryManager:ICategoryService
    {
        private ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public void Create(Category category)
        {
            _categoryDal.Create(category);
        }

        public void Delete(Category category)
        {
            _categoryDal.Delete(category);
        }

        public List<Category> GetAll()
        {
            return _categoryDal.GetAll().ToList();
        }

        public Category GetById(int id)
        {
            return _categoryDal.GetById(id);
        }

        public void Update(Category category)
        {
            _categoryDal.Update(category);
        }
    }
}
