using MovieApp.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.BusinessLayer.Abstract
{
    public interface IActorService
    {
        Actor GetById(int id);
        List<Actor> GetAll();
        void Create(Actor actor);
        void Update(Actor actor);
        void Delete(Actor actor);
    }
}
