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

        public ActionResult Update()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(EstateUpdateModel model)
        {
            if (ModelState.IsValid)
            {
                db.Estates.Update(model.id, model.ToData());
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        public ActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(EstateDeletionModel model)
        {
            if (ModelState.IsValid)
            {
                var exists = db.Estates.GetById(model.id);

                if (exists == null)
                    return View(exists);

                db.Estates.Delete(model.id);
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }
    }
}
