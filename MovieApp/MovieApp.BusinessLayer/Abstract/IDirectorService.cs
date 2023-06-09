using MovieApp.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.BusinessLayer.Abstract
{
    public interface IDirectorService
    {
        Director GetById(int id);
        List<Director> GetAll();
        void Create(Director director);
        void Update(Director director);
        void Delete(Director director);
    }
}
