using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MovieApp.Panel.UI.Controllers
{
    [AllowAnonymous]
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }



    }
}
