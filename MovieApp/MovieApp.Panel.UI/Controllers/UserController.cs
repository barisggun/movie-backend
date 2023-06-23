using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieApp.BusinessLayer.Concrete;
using MovieApp.DataAccess.Concrete;
using MovieApp.DataAccess.EntityFramework;
using MovieApp.Panel.UI.Models;

namespace MovieApp.Panel.UI.Controllers
{
    [AllowAnonymous]
    public class UserController : Controller
    {
        UserManager userManager = new UserManager(new EfUserRepository());
        Context c = new Context();

        public IActionResult Detail(int id)
        {
            var userRole = c.UserRoles.Where(x=>x.UserId == id).Select(y=>y.RoleId).FirstOrDefault();
            var userRoleName = c.Roles.Where(x=>x.Id == userRole).Select(y=> y.Name).FirstOrDefault();
            var username = c.Users.Select(x=>x.UserName).FirstOrDefault();
            var commentID = c.Comments.Where(x=>x.CommentUserName == username).Select(y=>y.ID).FirstOrDefault();
            var commentDate = c.Comments.Where(x=>x.ID == commentID).Select(y=>y.CommentDate).FirstOrDefault();
            var commentContent = c.Comments.Where(x=>x.ID == commentID).Select(y=>y.CommentContent).FirstOrDefault();
            var movieId = c.Comments.Where(x=>x.ID == commentID).Select(y=>y.MovieId).FirstOrDefault();
            var user = userManager.GetById(id);

            var model = new UserProfileModel
            {
                UserID = user.Id,
                UserName = user.UserName,
                NameSurname = user.NameSurname,
                About = user.About,
                UserRole = userRoleName,
                ImageUrl = user.ImageUrl,
                CommentID = commentID,
                CommentDate = commentDate,
                CommentContent = commentContent,
                MovieID = movieId
            };

            return View(model);
        }
    }
}
