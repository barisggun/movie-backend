using Microsoft.AspNetCore.Mvc;
using MovieApp.BusinessLayer.Concrete;

namespace MovieApp.Panel.UI.ViewComponents.Comment
{
    public class MovieComments:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
