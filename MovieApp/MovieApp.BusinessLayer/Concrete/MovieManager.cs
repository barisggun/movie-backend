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
    public class MovieManager : IMovieService
    {
        private IMovieDal _movieDal;
        Context c = new Context();


        public MovieManager(IMovieDal movieDal)
        {
            _movieDal = movieDal;
        }
        public void Create(Movie movie)
        {
            _movieDal.Create(movie);
        }

        public void Delete(Movie movie)
        {
            _movieDal.Delete(movie);
        }
        public List<Movie> GetAll()
        {
            return _movieDal.GetAll();
        }

        public Movie GetById(int id)
        {
            var movie = c.Movies
        .Include(m => m.Directors)
        .Include(m => m.Actors)
        .FirstOrDefault(m => m.ID == id);

            return movie;
        }

        public void Update(Movie movie)
        {
            _movieDal.Update(movie);
        }
        public List<Movie> GetList()
        {
            return _movieDal.GetAll();
        }
    }
}
