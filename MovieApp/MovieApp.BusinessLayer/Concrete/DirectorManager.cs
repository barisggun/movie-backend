using Microsoft.EntityFrameworkCore;
using MovieApp.BusinessLayer.Abstract;
using MovieApp.DataAccess.Abstract;
using MovieApp.DataAccess.Concrete;
using MovieApp.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.BusinessLayer.Concrete
{
    public class DirectorManager : IDirectorService
    {
        private IDirectorDal _directorDal;
        Context c = new Context();

        public DirectorManager(IDirectorDal directorDal)
        {
            _directorDal = directorDal;
        }
        public void Create(Director director)
        {
            director.UpdateSlug();
            _directorDal.Create(director);
        }

        public void Delete(Director director)
        {
            _directorDal.Delete(director);
        }
        public List<Director> GetAll()
        {
            return _directorDal.GetAll();
        }

        public Director GetById(int id)
        {
            return _directorDal.GetById(id);
        }
        public Director GetBySlug(string slug)
        {
            var director = c.Directors.Include(x => x.Movies)
            .FirstOrDefault(m => m.Slug == slug);

            return director;
        }

        public void Update(Director director)
        {
            _directorDal.Update(director);
        }
    }
}
