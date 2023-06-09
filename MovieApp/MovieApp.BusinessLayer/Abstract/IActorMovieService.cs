using MovieApp.EntityLayer.Entities;
using MovieApp.EntityLayer.Entities.ConnectionClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.BusinessLayer.Abstract
{
    public interface IActorMovieService
    {
        ActorMovie GetById(int id);
        List<ActorMovie> GetAll();
        void Create(ActorMovie actorMovie);
        void Update(ActorMovie actorMovie);
        void Delete(ActorMovie actorMovie);
    }
}
