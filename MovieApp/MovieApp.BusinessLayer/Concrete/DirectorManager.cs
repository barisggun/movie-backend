using MovieApp.BusinessLayer.Abstract;
using MovieApp.DataAccess.Abstract;
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

        public DirectorManager(IDirectorDal directorDal)
        {
            _directorDal = directorDal;
        }
        public void Create(Director director)
        {
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

        public void Update(Director director)
        {
            _directorDal.Update(director);
        }
    }
}
