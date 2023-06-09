using MovieApp.DataAccess.Abstract;
using MovieApp.DataAccess.Concrete;
using MovieApp.DataAccess.Repositories;
using MovieApp.EntityLayer.Entities;
using MovieApp.EntityLayer.Entities.ConnectionClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MovieApp.DataAccess.EntityFramework.EfActorMovieRepository;

namespace MovieApp.DataAccess.EntityFramework
{
    public class EfActorMovieRepository : NoGenericRepository<ActorMovie>, IActorMovieDal
    {

    }
}

