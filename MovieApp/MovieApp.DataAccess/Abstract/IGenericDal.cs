using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.DataAccess.Abstract
{
    public interface IGenericDal<T> where T : class
    {
        T GetById(int id);
        //T GetOne(Expression<Func<T, bool>> filter);
        List<T> GetAll();

        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
