﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieApp.BusinessLayer.Concrete;
using MovieApp.DataAccess.EntityFramework;
using MovieApp.EntityLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using MovieApp.DataAccess.Concrete;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using MovieApp.Panel.UI.Models;
using System.Security.Permissions;
using MovieApp.DataAccess.Migrations;
using X.PagedList;

namespace MovieApp.Panel.UI.Controllers
{
    [Authorize(Roles = "Admin,Writer")]
    public class BlogController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        //private readonly SignInManager<AppUser> _signInManager;

        BlogManager bm = new BlogManager(new EfBlogRepository());
        MovieManager mm = new MovieManager(new EfMovieRepository());
        Context c = new Context();
        private readonly UserManager<AppUser> _userManager;

        public BlogController(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var values = bm.GetBlogListWithMovieAndWriter();
            return View(values);
        }
        public IActionResult BlogListByWriter()
        {
            var username = User.Identity.Name;
            //ViewBag.v = usermail;
            var usermail = c.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();
            var userID = c.Users.Where(x => x.Email == usermail).Select(y => y.Id).FirstOrDefault();
            ViewBag.v = userID;
            //var userID = c.Users.Where(x=>x.Email == usermail).Select(y=>y.Id).FirstOrDefault();
            var values = bm.GetListWithMovieByWriterBm(userID);
            return View(values);
        }
        public IActionResult Create()
        {
            List<SelectListItem> movievalues = (from x in mm.GetList()
                                                select new SelectListItem
                                                {
                                                    Text = x.MovieTitle,
                                                    Value = x.ID.ToString()
                                                }).ToList();

            ViewBag.cv = movievalues;
            return View();
        }
        [HttpPost]
        public IActionResult Create(Blog b, IFormFile file)
        {
            b.BlogImage = "";
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string wwwrootPath = webHostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                    string extension = Path.GetExtension(file.FileName);

                    string yeniDosyaAdi = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwrootPath, "images", "blog", yeniDosyaAdi);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    b.BlogImage = yeniDosyaAdi;
                }

                var username = User.Identity.Name;
                var usermail = c.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();
                var userID = c.Users.Where(x => x.Email == usermail).Select(y => y.Id).FirstOrDefault();

                b.BlogStatus = true;
                b.BlogCreateDate = DateTime.Now; // Doğrudan şu anki zamanı atayabiliriz.
                b.AppUserId = userID;

                b.UpdateSlug();

                bm.Create(b);
                return RedirectToAction("BlogListByWriter", "Blog");
            }

            List<SelectListItem> movievalues = (from x in mm.GetList()
                                                select new SelectListItem
                                                {
                                                    Text = x.MovieTitle,
                                                    Value = x.ID.ToString()
                                                }).ToList();

            ViewBag.cv = movievalues;
            return View(b);
        }


        public IActionResult BlogDelete(int id)
        {
            var blogvalue = bm.GetById(id);
            bm.Delete(blogvalue);
            return RedirectToAction("BlogListByWriter");
        }

        public IActionResult BlogEdit(int id)
        {
            var blogvalue = bm.GetById(id);
            List<SelectListItem> movievalues = (from x in mm.GetList()
                                                select new SelectListItem
                                                {
                                                    Text = x.MovieTitle,
                                                    Value = x.ID.ToString()
                                                }).ToList();

            ViewBag.cv = movievalues;
            return View(blogvalue);
        }

        [HttpPost]
        public IActionResult BlogEdit(Blog b, IFormFile? file)
        {
            if (file != null)
            {
                string wwwrootPath = webHostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                string extension = Path.GetExtension(file.FileName);

                string yeniDosyaAdi = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwrootPath + "/images/blog/", yeniDosyaAdi);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                b.BlogImage = yeniDosyaAdi;
            }
            b.UpdateSlug();

            var username = User.Identity.Name;
            var usermail = c.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();
            var userID = c.Users.Where(x => x.Email == usermail).Select(y => y.Id).FirstOrDefault();
            b.AppUserId = userID;
            b.BlogCreateDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            b.BlogStatus = true;
            bm.Update(b);
            return RedirectToAction("BlogListByWriter");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AdminBlogListDelete(int id)
        {
            var blogvalue = bm.GetById(id);
            bm.Delete(blogvalue);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AdminBlogListDetail(int id)
        {
            var blog = c.Blogs.Include(x => x.Movies).Include(c => c.AppUsers).FirstOrDefault(y => y.ID == id);
            return View(blog);
        }

        [Authorize(Roles = "Writer")]
        public IActionResult Detail(int id)
        {
            var blog = c.Blogs.Include(x => x.Movies).Include(c => c.AppUsers).FirstOrDefault(y => y.ID == id);
            return View(blog);
        }

        //[AllowAnonymous]
        //public IActionResult Detail(int id)
        //{
        //    var blog = c.Blogs.Include(x => x.Movies).Include(c => c.AppUsers).FirstOrDefault(y => y.ID == id);
        //    return View(blog);
        //}

        [AllowAnonymous]
        public IActionResult BlogReadAll(string slug)
        {
            var blog = bm.GetBySlug(slug);
            if (blog == null)
            {
                return NotFound();
            }
            var userId = blog.AppUserId;
            var user = c.Users.FirstOrDefault(u => u.Id == userId);
            var userRoleId = c.UserRoles.Where(x => x.UserId == userId).Select(y => y.RoleId).FirstOrDefault();
            var userRoleName = c.Roles.Where(x => x.Id == userRoleId).Select(y => y.Name).FirstOrDefault();
            var userNameSurname = c.Users.Where(x => x.Id == userId).Select(y => y.NameSurname).FirstOrDefault();
            var userProfile = c.Users.Where(x => x.Id == userId).Select(y => y.ImageUrl).FirstOrDefault();

            var movieId = blog.MovieId;
            var movieName = c.Movies.Where(x => x.ID == movieId).Select(y => y.MovieTitle).FirstOrDefault();
            var movieSlug = c.Movies.Where(x => x.ID == movieId).Select(x => x.Slug).FirstOrDefault();

            var model = new BlogReadAllModel
            {
                UserID = userId,
                BlogID = blog.ID,
                Slug = blog.Slug,
                MovieSlug = movieSlug,
                BlogDate = blog.BlogCreateDate,
                BlogContent = blog.BlogContent,
                UserRole = userRoleName,
                NameSurname = userNameSurname,
                BlogImage = blog.BlogImage,
                BlogTitle = blog.BlogTitle,
                ProfilePicture = user.ProfilePictureUrl,
                MovieId = movieId,
                MovieName = movieName
            };

            return View(model);
        }

        [AllowAnonymous]
        public IActionResult BlogList(int page = 1)
        {

            var allBlogs = bm.GetAll().OrderByDescending(m=>m.BlogCreateDate).ToPagedList(page,6);

            return View(allBlogs);
        }




    }
}

