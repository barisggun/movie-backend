using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Query.Internal;
using MovieApp.BusinessLayer.Concrete;
using MovieApp.DataAccess.Concrete;
using MovieApp.DataAccess.EntityFramework;
using MovieApp.EntityLayer.Entities;
using MovieApp.Panel.UI.Models;

namespace MovieApp.Panel.UI.Controllers
{
    [AllowAnonymous]
    public class MainController : Controller
    {
        HomepageCoverManager homepageCoverManager = new HomepageCoverManager(new EfHomepageCoverRepository());
        MovieManager mm = new MovieManager(new EfMovieRepository());
        BlogManager bm = new BlogManager(new EfBlogRepository());
        Context c = new Context();


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm) || searchTerm.Trim().Length < 3)
            {
                ViewBag.ErrorMessage = "Lütfen geçerli bir arama terimi girin.";
                
            }

            var filterMovies = c.Movies.Where(m=>m.MovieTitle.Contains(searchTerm) || m.Description.Contains(searchTerm)).ToList();
            var filterBlogs = c.Blogs.Where(m=>m.BlogContent.Contains(searchTerm) || m.BlogTitle.Contains(searchTerm)).ToList();
            var filterUsers = c.Users.Where(m => m.UserName.Contains(searchTerm) || m.NameSurname.Contains(searchTerm)).ToList();

            var searchResults = new SearchResultsModel
            {
                Movies = filterMovies,
                Blogs = filterBlogs,
                Users = filterUsers
            };

            return View(searchResults);
        }

    }
}
