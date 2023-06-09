﻿using MovieApp.BusinessLayer.Abstract;
using MovieApp.DataAccess.Abstract;
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

        public ActorManager(IActorDal actorDal)
        {
            _actorDal = actorDal;
        }
        public void Create(Actor actor)
        {
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

        public void Update(Actor actor)
        {
            _actorDal.Update(actor);
        }
    }
}