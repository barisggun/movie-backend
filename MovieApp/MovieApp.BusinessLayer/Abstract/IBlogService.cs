using MovieApp.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.BusinessLayer.Abstract
{
    public interface IBlogService
    {
        Blog GetById(int id);
        List<Blog> GetAll();
        void Create(Blog blog);
        void Update(Blog blog);
        void Delete(Blog blog);
        List<Blog> GetBlogListWithMovie();
        List<Blog> GetBlogListByWriter(int id);
    }
}
