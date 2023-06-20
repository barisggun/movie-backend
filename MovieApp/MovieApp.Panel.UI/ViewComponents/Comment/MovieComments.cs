using Microsoft.AspNetCore.Mvc;
using MovieApp.BusinessLayer.Concrete;
using MovieApp.DataAccess.EntityFramework;

namespace MovieApp.Panel.UI.ViewComponents.Comment
{
    public class MovieComments:ViewComponent
    {
        CommentManager commentManager = new CommentManager(new EfCommentRepository());
        public IViewComponentResult Invoke(int id)
        {
            var values = commentManager.GetCommentById(id);
            return View(values);
        }
    }
}
