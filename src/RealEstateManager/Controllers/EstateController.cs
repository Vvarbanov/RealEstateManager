using System;
using System.Web.Mvc;
using RealEstateManager.Models.Estate;
using RealEstateManager.Utils;

namespace RealEstateManager.Controllers
{
    [Authorize]
    public class EstateController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EstateCreationModel model)
        {
            if (ModelState.IsValid)
            {
                var isNameAvailable = db.Estates.IsNameAvailable(model.Name);
                var isAddressAvailable = db.Estates.IsAddressAvailable(model.Address);

                if (!isNameAvailable)
                    ModelState.AddModelError(nameof(model.Name),
                        Localization.GetString("EstateCreation_NameExists_Error"));

                if (!isAddressAvailable)
                    ModelState.AddModelError(nameof(model.Address),
                        Localization.GetString("EstateCreation_AddressExists_Error"));

                if (!isNameAvailable || !isAddressAvailable)
                    return View(model);

                db.Estates.Insert(model.ToData());
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        [Authorize]
        [HttpPost, ActionName("Update")]
        [ValidateAntiForgeryToken]
        public ActionResult Update(Guid id, EstateCreationModel model)
        {
            if (ModelState.IsValid)
            {
                var isNameAvailable = db.Estates.IsNameAvailable(model.Name, id);
                var isAddressAvailable = db.Estates.IsAddressAvailable(model.Address, id);

                if (!isNameAvailable)
                    ModelState.AddModelError(nameof(model.Name),
                        Localization.GetString("EstateCreation_NameExists_Error"));

                if (!isAddressAvailable)
                    ModelState.AddModelError(nameof(model.Address),
                        Localization.GetString("EstateCreation_AddressExists_Error"));

                if (!isNameAvailable || !isAddressAvailable)
                    return View(model);

                db.Estates.Update(id, model.ToData());
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        [Authorize]
        [HttpGet]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id)
        {
            var exists = db.Estates.GetById(id);

            if (exists == null)
                return HttpNotFound();

            return View(exists);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            var exists = db.Estates.GetById(id);

            if (exists == null)
                return View(exists);

            db.Estates.Delete(id);
            return RedirectToAction("Index", "Home");
        }
    }
}
