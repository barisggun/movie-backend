using MovieApp.DataAccess.Abstract;
using MovieApp.DataAccess.Concrete;
using MovieApp.EntityLayer.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.DataAccess.Repositories
{
    public class GenericRepository<T> : IGenericDal<T> where T : BaseEntity
    {
        public void Delete(T t)
        {
            using (var context = new Context())
            {
                context.Remove(t);
                context.SaveChanges();
            }
        }

        public T GetById(int id)
        {
            using var c = new Context();
            return c.Set<T>().Find(id);
        }

        public void Create(T t)
        {
            using var c = new Context();
            c.Add(t);
            c.SaveChanges();
        }

        public void Update(T t)
        {
            using var c = new Context();
            c.Update(t);
            c.SaveChanges();
        }

        //public T GetOne(Expression<Func<T, bool>> filter)
        //{
        //    return _context.Set<T>().Where(filter).SingleOrDefault();
        //}

        public List<T> GetAll()
        {
            using var c = new Context();
            return c.Set<T>().ToList();
        }
    }
}
