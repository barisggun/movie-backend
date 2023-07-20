using Microsoft.EntityFrameworkCore;
using MovieApp.BusinessLayer.Abstract;
using MovieApp.DataAccess.Abstract;
using MovieApp.DataAccess.Concrete;
using MovieApp.DataAccess.Migrations;
using MovieApp.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.BusinessLayer.Concrete
{
    public class ActorManager : IActorService
    {
        private IActorDal _actorDal;
        Context c = new Context();

        public ActorManager(IActorDal actorDal)
        {
            _actorDal = actorDal;
        }
        public void Create(Actor actor)
        {
            actor.UpdateSlug();
            _actorDal.Create(actor);
        }

        public void Delete(Actor actor)
        {
            _actorDal.Delete(actor);
        }
        public List<Actor> GetAll()
        {
            return _actorDal.GetAll();
        }

        public Actor GetById(int id)
        {
            return _actorDal.GetById(id);
        }
        public Actor GetBySlug(string slug)
        {
            var actor = c.Actors
            .Include(m => m.Movies)
            .FirstOrDefault(m => m.Slug == slug);

            return actor;
        }

        public void Update(Actor actor)
        {
            _actorDal.Update(actor);
        }
    }
}
