using MovieApp.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.DataAccess.Abstract
{
    public interface ICommentDal:IGenericDal<Comment>
    {

        List<Comment> GetListByFilter(Expression<Func<Comment, bool>> filter);
    }
}
