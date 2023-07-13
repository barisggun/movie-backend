using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieApp.BusinessLayer.Concrete;
using MovieApp.DataAccess.Concrete;
using MovieApp.DataAccess.EntityFramework;
using MovieApp.EntityLayer.Entities;
using MovieApp.Panel.UI.Models;
using System.Security.Claims;

namespace MovieApp.Panel.UI.Controllers
{
    [AllowAnonymous]
    public class UserController : Controller
    {
        UserManager userManager = new UserManager(new EfUserRepository());
        Context c = new Context();
        private readonly UserManager<AppUser> _userManager;
        private readonly IWebHostEnvironment webHostEnvironment;

        public UserController(UserManager<AppUser> userManager, IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            this.webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Detail(int id)
        {
            var userRole = c.UserRoles.Where(x => x.UserId == id).Select(y => y.RoleId).FirstOrDefault();
            var userRoleName = c.Roles.Where(x => x.Id == userRole).Select(y => y.Name).FirstOrDefault();
            var username = c.Users.Select(x => x.UserName).FirstOrDefault();
            var commentID = c.Comments.Where(x => x.CommentUserName == username).Select(y => y.ID).FirstOrDefault();
            var commentDate = c.Comments.Where(x => x.ID == commentID).Select(y => y.CommentDate).FirstOrDefault();
            var commentContent = c.Comments.Where(x => x.ID == commentID).Select(y => y.CommentContent).FirstOrDefault();
            var movieId = c.Comments.Where(x => x.ID == commentID).Select(y => y.MovieId).FirstOrDefault();
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
                MovieID = movieId,
                ProfileImageUrl = user.ProfilePictureUrl
            };

            return View(model);
        }


        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser == null || currentUser.Id != id)
            {
                return RedirectToAction("Login", "Account");
            }

            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user == null)
            {
                // 
            }

            var model = new UserProfileEditModel
            {
                UserID = user.Id,
                NameSurname = user.NameSurname,
                About = user.About,
                EMail = user.Email,
                ImageUrl = user.ImageUrl,
                ProfilePictureUrl = user.ProfilePictureUrl
            };

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(int id, UserProfileEditModel model, [FromForm(Name = "ImageFile")] IFormFile? imageFile, [FromForm(Name = "ProfileImageFile")] IFormFile? profileImageFile)
        {
            if (ModelState.IsValid)
            {
                //var username = User.Identity.Name;
                //var userId  = c.Users.Where(x => x.UserName == username).Select(y => y.Id).FirstOrDefault();
                var userId = c.Users.Where(x => x.Id == id);
                var user = await _userManager.FindByIdAsync(id.ToString());

                if (user == null)
                {

                }

                user.NameSurname = model.NameSurname;
                user.About = model.About;

                if (!string.IsNullOrEmpty(model.EMail))
                {
                    var existingUser = await _userManager.FindByEmailAsync(model.EMail);
                    if (existingUser != null)
                    {
                        ModelState.AddModelError(String.Empty, "Bu mail sistemde kayıtlı, lütfen geçerli bir mail adresi giriniz.");
                        return View(model);
                    }

                    user.Email = model.EMail;
                }


                if (imageFile != null)
                {
                    string wwwrootPath = webHostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(imageFile.FileName);
                    string extension = Path.GetExtension(imageFile.FileName);
                    string newFileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    string imagePath = Path.Combine(wwwrootPath, "images", "profile", newFileName);

                    using (var fileStream = new FileStream(imagePath, FileMode.Create))
                    {
                        imageFile.CopyTo(fileStream);
                    }

                    user.ImageUrl = newFileName;
                }
                if (profileImageFile != null)
                {
                    string wwwrootPath = webHostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(profileImageFile.FileName);
                    string extension = Path.GetExtension(profileImageFile.FileName);
                    string newFileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    string imagePath = Path.Combine(wwwrootPath, "images", "profile", newFileName);

                    using (var fileStream = new FileStream(imagePath, FileMode.Create))
                    {
                        profileImageFile.CopyTo(fileStream);
                    }

                    user.ProfilePictureUrl = newFileName;
                }


                if (!string.IsNullOrEmpty(model.OldPassword) || !string.IsNullOrEmpty(model.NewPassword))
                {
                    if (string.IsNullOrEmpty(model.OldPassword) || string.IsNullOrEmpty(model.NewPassword))
                    {
                        ModelState.AddModelError("", "Please enter both the old and new passwords.");
                        
                        if (!string.IsNullOrEmpty(user.ImageUrl))
                        {
                            model.ImageUrl = user.ImageUrl;
                        }
                        if (!string.IsNullOrEmpty(user.ProfilePictureUrl))
                        {
                            model.ProfilePictureUrl = user.ProfilePictureUrl;
                        }
                        return View(model);
                    }

                    var isOldPasswordCorrect = await _userManager.CheckPasswordAsync(user, model.OldPassword);
                    if (!isOldPasswordCorrect)
                    {
                        ModelState.AddModelError("OldPassword", "The old password is incorrect.");
                        ModelState.Remove("NewPassword");
                       
                        if (!string.IsNullOrEmpty(user.ImageUrl))
                        {
                            model.ImageUrl = user.ImageUrl;
                        }
                        if (!string.IsNullOrEmpty(user.ProfilePictureUrl))
                        {
                            model.ProfilePictureUrl = user.ProfilePictureUrl;
                        }
                        return View(model);
                    }

                 
                    if (model.NewPassword.Length < 6)
                    {
                        ModelState.AddModelError("NewPassword", "The new password must be at least 6 characters long.");
                        
                        if (!string.IsNullOrEmpty(user.ImageUrl))
                        {
                            model.ImageUrl = user.ImageUrl;
                        }
                        if (!string.IsNullOrEmpty(user.ProfilePictureUrl))
                        {
                            model.ProfilePictureUrl = user.ProfilePictureUrl;
                        }
                        return View(model);
                    }

                  
                    var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

                    if (!changePasswordResult.Succeeded)
                    {
                        
                    }
                }


                await _userManager.UpdateAsync(user);

                return RedirectToAction("Detail", new { id = id });
            }

            return View(model);
        }
    }
}
