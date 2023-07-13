using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

            var userIds = values.Select(comment => comment.CommentUserName);
            var users = Task.Run(() => _userManager.Users.Where(user => userIds.Contains(user.UserName)).ToListAsync()).Result;

            var userPhotos = users.ToDictionary(user => user.UserName, user => user.ProfilePictureUrl);

            foreach (var comment in values)
            {
                if (userPhotos.ContainsKey(comment.CommentUserName))
                {
                    comment.ProfilePictureUrl = userPhotos[comment.CommentUserName];
                }
            }

            return View(values);
        }

    }
}
