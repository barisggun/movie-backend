using Microsoft.AspNetCore.Mvc;

namespace MovieApp.Panel.UI.Controllers
{
    public class ActorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}
