using MovieApp.DataAccess.Abstract;
using MovieApp.DataAccess.Repositories;
using MovieApp.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.DataAccess.EntityFramework
{
    public class EfCommentRepository:GenericRepository<Comment>,ICommentDal
    {

    }
}
