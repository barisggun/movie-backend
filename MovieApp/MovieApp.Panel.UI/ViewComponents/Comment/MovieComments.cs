using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieApp.BusinessLayer.Concrete;
using MovieApp.DataAccess.EntityFramework;
using MovieApp.EntityLayer.Entities;

namespace MovieApp.Panel.UI.ViewComponents.Comment
{
    public class MovieComments:ViewComponent
    {
        CommentManager commentManager = new CommentManager(new EfCommentRepository());
        private readonly UserManager<AppUser> _userManager;
        public MovieComments(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public IViewComponentResult Invoke(int id)
        {
            var values = commentManager.GetCommentById(id);

            foreach (var comment in values)
            {
                var user = Task.Run(() => _userManager.FindByNameAsync(comment.CommentUserName)).Result;
                if (user != null)
                {
                    comment.ProfilePictureUrl = user.ProfilePictureUrl;
                }
            }

            return View(values);
        }

    }
}
