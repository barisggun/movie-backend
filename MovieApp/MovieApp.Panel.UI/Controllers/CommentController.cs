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



        [HttpPost]
        public IActionResult AddComment(Comment p, MovieDetailModel m)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }
            var username = User.Identity.Name;
            var userID = c.Users.Where(x => x.UserName == username).Select(y => y.Id).FirstOrDefault();
            var profilePicture = c.Users.Where(x => x.UserName == username).Select(y => y.ProfilePictureUrl).FirstOrDefault();

            var values = userManager.GetById(userID);
            DateTime now = DateTime.Now;
            DateTime commentDate = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);


            p.CommentDate = commentDate;
            p.CommentStatus = true;
            p.CommentUserName = username;
            p.MovieId = m.MovieId;
            p.CommentUserNameId = userID;

            commentManager.Create(p);
            return RedirectToAction("Detail", "Movie", new { id = p.MovieId });
        }


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

        [HttpPost]
        public IActionResult DeleteFromProfile(int commentId, int movieId)
        {

            Comment comment = commentManager.GetById(commentId);

            if (comment != null)
            {

                var username = User.Identity.Name;
                var userID = c.Users.Where(x => x.UserName == username).Select(y => y.Id).FirstOrDefault();
                var userId = userID;

                if (comment.CommentUserName == username)
                {
                    commentManager.Delete(comment);
                    return RedirectToAction("Detail", "User", new { id = userId });
                }
                else
                {

                    return RedirectToAction("Unauthorized", "Error");
                }
            }


            return View();
        }

        [HttpGet]
        public IActionResult LoadMoreComments(int startIndex, int pageSize, int movieId)
        {
            var movie = c.Movies.FirstOrDefault(x => x.ID == movieId);

            if (movie == null)
            {
                
                return BadRequest("Hatalı film kimliği");
            }

            var comments = commentManager.GetCommentById(movieId)
                .Skip(startIndex)
                .Take(pageSize)
                .ToList();

            var userIds = comments.Select(comment => comment.CommentUserName);
            var users = Task.Run(() => c.Users.Where(user => userIds.Contains(user.UserName)).ToList()).Result;

            var userPhotos = users.ToDictionary(user => user.UserName, user => user.ProfilePictureUrl);

            foreach (var comment in comments)
            {
                if (userPhotos.ContainsKey(comment.CommentUserName))
                {
                    comment.ProfilePictureUrl = userPhotos[comment.CommentUserName];
                }
            }

            return PartialView("_CommentList", comments);
        }


        [HttpGet]
        public IActionResult LoadMoreProfileComments(int startIndex, int pageSize, int userId)
        {
            var user = c.Users.FirstOrDefault(x => x.Id == userId);

            if (user == null)
            {
                
                return BadRequest("Hatalı kullanıcı kimliği");
            }

            var comments = commentManager.GetCommentListWithUser(userId)
                .Skip(startIndex)
                .Take(pageSize)
                .ToList();

            return PartialView("_ProfileCommentList", comments);
        }
    }
}
