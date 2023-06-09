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
    public class ActorMovieManager : IActorMovieService
    {
        private IActorMovieDal _actorMovieDal;

        public ActorMovieManager(IActorMovieDal actorMovieDal)
        {
            _actorMovieDal = actorMovieDal;
        }

        public void Create(ActorMovie actorMovie)
        {
            _actorMovieDal.Create(actorMovie);
        }

        public void Delete(ActorMovie actorMovie)
        {
            _actorMovieDal.Delete(actorMovie);
        }

        public List<ActorMovie> GetAll()
        {
            return _actorMovieDal.GetAll();
        }

        public ActorMovie GetById(int id)
        {
            return _actorMovieDal.GetById(id);
        }

        public void Update(ActorMovie actorMovie)
        {
            _actorMovieDal.Update(actorMovie);
        }
    }
}
