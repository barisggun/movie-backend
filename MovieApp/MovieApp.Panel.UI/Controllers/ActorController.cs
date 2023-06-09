using Microsoft.AspNetCore.Mvc;
using MovieApp.BusinessLayer.Concrete;
using MovieApp.DataAccess.EntityFramework;
using MovieApp.EntityLayer.Entities;

namespace MovieApp.Panel.UI.Controllers
{
    public class ActorController : Controller
    {
        ActorManager actorManager = new ActorManager(new EfActorRepository());

        public IActionResult Index()
        {
            var values = actorManager.GetAll();
            return View(values);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Actor actor)
        {
            actorManager.Create(actor);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            Actor actor = actorManager.GetById(id);
            return View(actor);
        }

        [HttpPost]
        public IActionResult DeleteCategory(Actor actor)
        {
            actorManager.Delete(actor);
            return RedirectToAction("Index");
        }
    }
}
