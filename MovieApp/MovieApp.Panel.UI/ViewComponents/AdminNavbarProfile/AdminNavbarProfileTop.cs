﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieApp.BusinessLayer.Concrete;
using MovieApp.DataAccess.Concrete;
using MovieApp.DataAccess.EntityFramework;
using MovieApp.EntityLayer.Entities;

namespace MovieApp.Panel.UI.ViewComponents.AdminNavbarProfile
{
    public class AdminNavbarProfileTop:ViewComponent
    {
        UserManager userManager = new UserManager(new EfUserRepository());
        Context c = new Context();
        public IViewComponentResult Invoke()
        {
            var username = User.Identity.Name;
            var userID = c.Users.Where(x => x.UserName == username).Select(y => y.Id).FirstOrDefault();
            var values = userManager.GetById(userID);
            return View(values);
        }
    }
}
