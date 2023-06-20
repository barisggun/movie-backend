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
        Comment GetById(int id);
        List<Comment> GetAll();
        void Create(Comment comment);
        void Update(Comment comment);
        void Delete(Comment comment);

        List<Comment> GetListByFilter(Expression<Func<Comment, bool>> filter);
    }
}
