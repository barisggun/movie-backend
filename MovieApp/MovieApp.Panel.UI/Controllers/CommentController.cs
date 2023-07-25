using CrmUygulamasi.UI.Services;
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
        MovieManager mm = new MovieManager(new EfMovieRepository());

        private readonly INotificationService notificationService;

        public CommentController(INotificationService notificationService)
        {
            this.notificationService = notificationService;
        }

        [HttpPost]
        public IActionResult AddComment(Comment p, int movieId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                notificationService.Notification(NotifyType.Error, "Yorum yapmak için giriş yapmalısınız.");
                return RedirectToAction("Detail", "Movie", new { id = p.MovieId });
            }
            var username = User.Identity.Name;
            var userID = c.Users.Where(x => x.UserName == username).Select(y => y.Id).FirstOrDefault();
            var profilePicture = c.Users.Where(x => x.UserName == username).Select(y => y.ProfilePictureUrl).FirstOrDefault();

            var values = userManager.GetById(userID);
            DateTime now = DateTime.Now;
            DateTime commentDate = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);

            var movie = c.Movies.SingleOrDefault(m => m.ID == movieId);

            if (movie == null)
            {
                return NotFound();
            }

            p.CommentDate = commentDate;
            p.CommentStatus = true;
            p.CommentUserName = username;
            p.MovieId = movie.ID;
            p.CommentUserNameId = userID;
            p.MovieSlug = movie.Slug;


            var lastComment = commentManager.GetLastCommentByUser(userID.ToString());

            if (lastComment != null)
            {

                var timeDifference = commentDate - lastComment.CommentDate;

                if (timeDifference.TotalSeconds < 10)
                {
                    //ModelState.AddModelError(string.Empty, "Çok hızlı yorum ekliyorsunuz. Lütfen 10 saniye bekleyin.");
                    notificationService.Notification(NotifyType.Warning, "Çok hızlı bir şekilde yorum eklemeye çalıştınız!");
                    return RedirectToAction("Detail", "Movie", new { id = p.MovieId });
                }
            }

            commentManager.Create(p);
            notificationService.Notification(NotifyType.Success, $"{p.CommentUserName} Yorumunuz başarıyla eklendi!");
            return RedirectToAction("Detail", "Movie", new { slug = movie.Slug });
        }


        [HttpPost]
        public IActionResult Delete(int commentId, string slug)
        {

            Comment comment = commentManager.GetById(commentId);

            if (comment != null)
            {

                var username = User.Identity.Name;

                if (comment.CommentUserName == username || User.IsInRole("Admin"))
                {
                    commentManager.Delete(comment);
                }
                else
                {

                    return RedirectToAction("Unauthorized", "Error");
                }
            }

            Movie movie = mm.GetBySlug(slug);

            return RedirectToAction("Detail", "Movie", new { slug = slug });
        }

        [HttpPost]
        public IActionResult DeleteFromProfile(int commentId, string slug)
        {

            Comment comment = commentManager.GetById(commentId);

            if (comment != null)
            {

                var username = User.Identity.Name;
                var userID = c.Users.Where(x => x.UserName == username).Select(y => y.Id).FirstOrDefault();
                var userId = userID;

                if (comment.CommentUserName == username)
                {
                    Movie movie = mm.GetBySlug(slug);
                    commentManager.Delete(comment);
                    var userSlug = userManager.GetBySlug(username);
                    return RedirectToAction("Detail", "User", new { slug = userSlug });
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
