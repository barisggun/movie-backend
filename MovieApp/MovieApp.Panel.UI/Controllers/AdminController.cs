using Microsoft.AspNetCore.Mvc;

namespace MovieApp.Panel.UI.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
