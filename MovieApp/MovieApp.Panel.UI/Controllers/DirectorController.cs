using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieApp.BusinessLayer.Concrete;
using MovieApp.DataAccess.EntityFramework;
using MovieApp.EntityLayer.Entities;

namespace MovieApp.Panel.UI.Controllers
{
    [Authorize(Roles = "Admin")]
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
            if (ModelState.IsValid)
            {
                directorManager.Create(director);
                return RedirectToAction("Index");
            }
            return View(director);
        }

        [HttpPost]
        public IActionResult Delete(int directorId)
        {
            var directorToDelete = directorManager.GetById(directorId);
            if (directorToDelete == null)
            {
                return NotFound();
            }

            directorManager.Delete(directorToDelete);

            return RedirectToAction("Index");
        }
    }
}
