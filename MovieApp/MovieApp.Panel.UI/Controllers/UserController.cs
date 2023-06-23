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
            //var userId = c.Users.Where(x=>x.Id == id).FirstOrDefault();
            var userRole = c.UserRoles.Where(x=>x.UserId == id).Select(y=>y.RoleId).FirstOrDefault();
            var userRoleName = c.Roles.Where(x=>x.Id == userRole).Select(y=> y.Name).FirstOrDefault();
            var user = userManager.GetById(id);

            var model = new UserProfileModel
            {
                UserID = user.Id,
                UserName = user.UserName,
                NameSurname = user.NameSurname,
                About = user.About,
                UserRole = userRoleName,
                ImageUrl = user.ImageUrl
            };

            return View(model);
        }
    }
}
