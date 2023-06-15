using MovieApp.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.BusinessLayer.Abstract
{
    public interface ICommentService
    {
        void Create(Comment comment);

    }
}
