using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class MyTaskController : Controller
    {
        // GET: MyTaskController
        public ActionResult Index()
        {
            return View();
        }

        // GET: MyTaskController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MyTaskController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MyTaskController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MyTaskController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MyTaskController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MyTaskController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MyTaskController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
