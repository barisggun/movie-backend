using Microsoft.AspNetCore.Mvc;
using MovieApp.BusinessLayer.Concrete;
using MovieApp.DataAccess.EntityFramework;
using MovieApp.EntityLayer.Entities;

namespace MovieApp.Panel.UI.Controllers
{
    public class DirectorController : Controller
    {
        DirectorManager directorManager = new DirectorManager(new EfDirectorRepository());
        public IActionResult Index()
        {
            var values = directorManager.GetAll();
            return View(values);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Director director)
        {
            directorManager.Create(director);
            return RedirectToAction("Index");
        }
    }
}
