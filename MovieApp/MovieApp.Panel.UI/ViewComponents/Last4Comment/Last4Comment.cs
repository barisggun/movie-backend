using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApp.BusinessLayer.Concrete;
using MovieApp.DataAccess.Concrete;
using MovieApp.DataAccess.EntityFramework;
using MovieApp.EntityLayer.Entities;
using MovieApp.Panel.UI.Models;

namespace MovieApp.Panel.UI.ViewComponents.Last4Comment
{
    public class Last4Comment: ViewComponent
    {
        CommentManager cm = new CommentManager(new EfCommentRepository());
        Context c = new Context();

        public IViewComponentResult Invoke()
        {
            var values = cm.GetCommentListWithMovie();
            values.Reverse();
            return View(values);
        }
    }
}
