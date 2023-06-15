using Microsoft.EntityFrameworkCore;
using MovieApp.DataAccess.Abstract;
using MovieApp.DataAccess.Concrete;
using MovieApp.DataAccess.Repositories;
using MovieApp.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.DataAccess.EntityFramework
{
    public class EfBlogRepository : GenericRepository<Blog>, IBlogDal
    {
        public List<Blog> GetListWithMovie()
        {
            using (var c = new Context())
            {
                return c.Blogs.Include(x => x.Movies).ToList();
            }
        }

        public List<Blog> GetListWithMovieByWriter(int id)
        {
            using (var c = new Context())
            {
                return c.Blogs.Include(x => x.Movies).Where(x => x.AppUserId == id).ToList();
            }
        }
    }
}
