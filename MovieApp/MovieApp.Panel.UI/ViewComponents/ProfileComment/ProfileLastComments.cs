using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApp.BusinessLayer.Concrete;
using MovieApp.DataAccess.Concrete;
using MovieApp.DataAccess.EntityFramework;

namespace MovieApp.Panel.UI.ViewComponents.ProfileComment
{
    public class ProfileLastComments : ViewComponent
    {
        CommentManager cm = new CommentManager(new EfCommentRepository());
        Context c = new Context();
        public IViewComponentResult Invoke(int Id)
        {
            var values = cm.GetCommentListWithUser(Id);

            return View(values);
        }
    }
}
