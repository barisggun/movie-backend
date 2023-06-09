using Microsoft.AspNetCore.Mvc;

namespace MovieApp.Panel.UI.Controllers
{
    public class MovieController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
