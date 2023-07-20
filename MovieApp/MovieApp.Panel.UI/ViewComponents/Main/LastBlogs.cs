using Microsoft.AspNetCore.Mvc;
using MovieApp.BusinessLayer.Concrete;
using MovieApp.DataAccess.EntityFramework;

namespace MovieApp.Panel.UI.ViewComponents.Main
{
    public class LastBlogs: ViewComponent
    {
        BlogManager bm = new BlogManager(new EfBlogRepository());
        public IViewComponentResult Invoke()
        {
            var values = bm.GetAll().OrderByDescending(x=>x.BlogCreateDate).Take(6).ToList();
            return View(values);
        }
    }
}
