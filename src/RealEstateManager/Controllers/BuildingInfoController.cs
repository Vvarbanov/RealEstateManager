using System;
using System.Linq;
using System.Web.Mvc;
using RealEstateManager.Models.BuildingInfo;

namespace RealEstateManager.Controllers
{
    [Authorize]
    public class BuildingInfoController : BaseController
    {
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Create(Guid? estateId)
        {
            if (!estateId.HasValue)
                return RedirectToAction("Index", "Home");

            var model = new BuildingInfoCreationModel();
            model.EstateId = estateId.Value;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BuildingInfoCreationModel model)
        {
            if (ModelState.IsValid)
            {
                db.BuildingInfoes.Insert(model.ToData());
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        public ActionResult Update(Guid? id)
        {
            if (!id.HasValue)
                return RedirectToAction("Index", "Home");

            var existing = db.BuildingInfoes.GetById(id.Value);

            if (existing == null)
                return RedirectToAction("Index", "Home");

            var model = new BuildingInfoUpdateModel
            {
                Id = existing.Id,
                View = existing.View,
                Act16 = existing.Act16,
                Floors = existing.Floors,
                Bedrooms = existing.Bedrooms,
                Bathrooms = existing.Bathrooms,
                Balconies = existing.Balconies,
                Garages = existing.Garages
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(BuildingInfoUpdateModel model)
        {
            if (ModelState.IsValid)
            {
                db.BuildingInfoes.Update(model.Id, model.ToData());
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        public ActionResult Delete(Guid? id)
        {
            if (!id.HasValue)
                return RedirectToAction("Index", "Home");

            var existing = db.BuildingInfoes.GetById(id.Value);

            if (existing == null)
                return RedirectToAction("Index", "Home");

            var model = new BuildingInfoDeletionModel
            {
                Id = existing.Id
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(BuildingInfoDeletionModel model)
        {
            if (ModelState.IsValid)
            {
                db.BuildingInfoes.Delete(model.Id);
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }
    }
}