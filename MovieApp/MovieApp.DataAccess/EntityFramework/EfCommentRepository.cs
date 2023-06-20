using MovieApp.DataAccess.Abstract;
using MovieApp.DataAccess.Concrete;
using MovieApp.DataAccess.Repositories;
using MovieApp.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.DataAccess.EntityFramework
{
    public class EfCommentRepository : GenericRepository<Comment>, ICommentDal
    {
        public List<Comment> GetListByFilter(Expression<Func<Comment, bool>> filter)
        {
            using var c = new Context();
            return c.Set<Comment>().Where(filter).ToList();
        }
    }
}
