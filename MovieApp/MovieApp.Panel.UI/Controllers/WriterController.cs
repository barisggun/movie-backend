using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApp.BusinessLayer.Concrete;
using MovieApp.DataAccess.EntityFramework;

namespace MovieApp.Panel.UI.Controllers
{
    public class WriterController : Controller
    {
        UserManager um = new UserManager(new EfUserRepository());

        public IActionResult Index()
        {
            return View();
        }
    }
}
