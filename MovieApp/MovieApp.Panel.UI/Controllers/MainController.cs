﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MovieApp.BusinessLayer.Concrete;
using MovieApp.DataAccess.EntityFramework;
using MovieApp.EntityLayer.Entities;

namespace MovieApp.Panel.UI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MainController : Controller
    {
        HomepageCoverManager homepageCoverManager = new HomepageCoverManager(new EfHomepageCoverRepository());
        MovieManager mm = new MovieManager(new EfMovieRepository());
        BlogManager bm = new BlogManager(new EfBlogRepository());


        public IActionResult Index()
        {
            return View();
        }



    }
}
