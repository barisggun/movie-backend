using Microsoft.EntityFrameworkCore;
using MovieApp.BusinessLayer.Abstract;
using MovieApp.DataAccess.Abstract;
using MovieApp.DataAccess.Concrete;
using MovieApp.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MovieApp.BusinessLayer.Concrete
{
    public class CommentManager:ICommentService
    {
        ICommentDal _commentDal;
        Context c = new Context();


        public CommentManager(ICommentDal commentDal)
        {
            _commentDal = commentDal;
        }

        public void Create(Comment comment)
        {
            _commentDal.Create(comment);
        }

        public void Delete(Comment comment)
        {
            _commentDal.Delete(comment);
        }

        public List<Comment> GetAll()
        {
            return _commentDal.GetAll();
        }

        public Comment GetById(int id)
        {
            return _commentDal.GetById(id);
        }

        public List<Comment> GetCommentById(int id)
        {
            var comments= _commentDal.GetListByFilter(x=> x.MovieId == id);
            comments = comments.OrderBy(c => c.CommentDate).ToList();
            comments.Reverse();
            return comments;
        }

        public List<Comment> GetCommentListWithMovie()
        {
            var comments = c.Comments.Include(b => b.Movie).OrderByDescending(c=>c.CommentDate).Take(4)
                .ToList();
            return comments;
        }

        public void Update(Comment comment)
        {
            _commentDal.Update(comment);
        }
    }
    }
