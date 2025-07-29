using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HyperMsg.Scada.WebApi.Controllers
{
    public class DeviceController : Controller
    {
        // GET: DeviceController
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        // GET: DeviceController/5
        [HttpGet]
        public ActionResult Details(int id)
        {
            return View();
        }

        // POST: DeviceController
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

        // POST: DeviceController/5
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

        // DELETE: DeviceController/5
        public ActionResult Delete(int id)
        {
            return View();
        }
    }
}
