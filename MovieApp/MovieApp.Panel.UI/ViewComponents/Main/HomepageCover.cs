using Microsoft.AspNetCore.Mvc;
using MovieApp.BusinessLayer.Concrete;
using MovieApp.DataAccess.EntityFramework;

namespace MovieApp.Panel.UI.ViewComponents.Main
{
    public class HomepageCover:ViewComponent
    {
        HomepageCoverManager homepageCoverManager = new HomepageCoverManager(new EfHomepageCoverRepository());
        public IViewComponentResult Invoke()
        {
            var values = homepageCoverManager.GetAll().FirstOrDefault();
            return View(values);
        }
    }
}
