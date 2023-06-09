using MovieApp.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.BusinessLayer.Abstract
{
    public interface IMovieService
    {
        Movie GetById(int id);
        List<Movie> GetAll();
        void Create(Movie movie);
        void Update(Movie movie);
        void Delete(Movie movie);
    }
}
