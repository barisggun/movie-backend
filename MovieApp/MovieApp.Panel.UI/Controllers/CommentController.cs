using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieApp.BusinessLayer.Concrete;
using MovieApp.DataAccess.Concrete;
using MovieApp.DataAccess.EntityFramework;
using MovieApp.EntityLayer.Entities;
using MovieApp.Panel.UI.Models;

namespace MovieApp.Panel.UI.Controllers
{
    [AllowAnonymous]
    public class CommentController : Controller
    {
        Context c = new Context();
        CommentManager commentManager = new CommentManager(new EfCommentRepository());
        UserManager userManager = new UserManager(new EfUserRepository());

        //[HttpGet]
        //public PartialViewResult AddComment(int id)
        //{
           
        //    //var movie = c.Movies.FirstOrDefault(x => x.ID == id);
        //    //ViewBag.Movie = movie;

        //    //ViewBag.Movie = id;
        //    return PartialView(id);
        //}

        [HttpPost]
        public IActionResult AddComment(Comment p, MovieDetailModel m)
        {
            var username = User.Identity.Name;
            var userID = c.Users.Where(x => x.UserName == username).Select(y => y.Id).FirstOrDefault();
            var values = userManager.GetById(userID);

            p.CommentDate=Convert.ToDateTime(DateTime.Now.ToShortDateString());
            p.CommentStatus = true;
            p.CommentUserName=username;
            p.MovieId = m.MovieId;
            commentManager.Create(p);
            return RedirectToAction("Detail", "Movie", new { id = p.MovieId });
        }


        //[HttpPost]
        //public IActionResult Delete(int commentId, MovieDetailModel m)
        //{

        //    Comment comment = commentManager.GetById(commentId);

        //    if (comment != null)
        //    {

        //        commentManager.Delete(comment);
        //    }


        //    return RedirectToAction("Detail", "Movie",new { id = m.MovieId }); 
        //}

        [HttpPost]
        public IActionResult Delete(int commentId, int movieId)
        {
        
            Comment comment = commentManager.GetById(commentId);

            if (comment != null)
            {
                
                var username = User.Identity.Name;

                if (comment.CommentUserName == username)
                {
                    commentManager.Delete(comment);
                }
                else
                {
                   
                    return RedirectToAction("Unauthorized", "Error");
                }
            }


            return RedirectToAction("Detail", "Movie", new { id = movieId }); 
        }

    }
}
