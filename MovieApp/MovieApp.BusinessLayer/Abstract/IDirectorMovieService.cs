using MovieApp.EntityLayer.Entities;
using MovieApp.EntityLayer.Entities.ConnectionClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.BusinessLayer.Abstract
{
    public interface IDirectorMovieService
    {
        DirectorMovie GetById(int id);
        List<DirectorMovie> GetAll();
        void Create(DirectorMovie directorMovie);
        void Update(DirectorMovie directorMovie);
        void Delete(DirectorMovie directorMovie);
    }
}
