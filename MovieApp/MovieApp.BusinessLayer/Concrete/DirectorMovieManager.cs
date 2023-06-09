using MovieApp.BusinessLayer.Abstract;
using MovieApp.DataAccess.Abstract;
using MovieApp.EntityLayer.Entities;
using MovieApp.EntityLayer.Entities.ConnectionClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.BusinessLayer.Concrete
{
    public class DirectorMovieManager:IDirectorMovieService
    {
        private IDirectorMovieDal _directorMovieDal;

        public DirectorMovieManager(IDirectorMovieDal directorMovieDal)
        {
            _directorMovieDal = directorMovieDal;
        }

        public void Create(DirectorMovie directorMovie)
        {
            _directorMovieDal.Create(directorMovie);
        }

        public void Delete(DirectorMovie directorMovie)
        {
            _directorMovieDal.Delete(directorMovie);
        }
        public List<DirectorMovie> GetAll()
        {
            return _directorMovieDal.GetAll();
        }

        public DirectorMovie GetById(int id)
        {
            return _directorMovieDal.GetById(id);
        }

        public void Update(DirectorMovie directorMovie)
        {
            _directorMovieDal.Update(directorMovie);
        }

    }
}
