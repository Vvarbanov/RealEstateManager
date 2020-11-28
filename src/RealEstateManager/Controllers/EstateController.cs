using System;
using System.Web.Mvc;
using RealEstateManager.Models.Estate;

namespace RealEstateManager.Controllers
{
    [Authorize]
    public class EstateController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EstateCreationModel model)
        {
            if (ModelState.IsValid)
            {
                db.Estates.Insert(model.ToData());
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        public ActionResult Update(Guid id)
        {
            var existing = db.Estates.GetById(id);

            if (existing == null)
                return HttpNotFound();

            var model = new EstateUpdateModel();
            model.Id = existing.Id;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(EstateUpdateModel model)
        {
            if (ModelState.IsValid)
            {
                db.Estates.Update(model.Id, model.ToData());
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        public ActionResult Delete(Guid id)
        {
            var existing = db.Estates.GetById(id);

            if (existing == null)
                return HttpNotFound();

            var model = new EstateDeletionModel();
            model.Id = existing.Id;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(EstateDeletionModel model)
        {
            if (ModelState.IsValid)
            {
                db.Estates.Delete(model.Id);
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }
    }
}
