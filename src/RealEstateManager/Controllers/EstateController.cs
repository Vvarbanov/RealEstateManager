using System;
using System.Linq;
using System.Web.Mvc;
using RealEstateManager.Models.Estate;
using RealEstateManager.Models.Data;

namespace RealEstateManager.Controllers
{
    [Authorize]
    public class EstateController : BaseController
    {
        public ActionResult Index()
        {
            var existing = db.Estates.Get(null, x => x.OrderByDescending(y => y.UpdateDate));

            var model = new EstateListGetModel(existing);

            return View(model);
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
                var estate = db.Estates.Insert(model.ToData());

                if (model.Type.Equals(EstateType.Apartment) || model.Type.Equals(EstateType.House))
                    return RedirectToAction("Create", "BuildingInfo", new { estateId = estate.Id });

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        public ActionResult Update(Guid? id)
        {
            if (!id.HasValue)
                return RedirectToAction("Index", "Home");

            var existing = db.Estates.GetById(id.Value);

            if (existing == null)
                return RedirectToAction("Index", "Home");

            var model = new EstateUpdateModel(
                existing.Id,
                existing.Name,
                existing.Type,
                existing.Address,
                existing.Price,
                existing.Status,
                existing.PublicDescription,
                existing.PrivateDescription,
                existing.Area
                );

            model.BuildingInfoId = existing.BuildingInfoId;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(EstateUpdateModel model)
        {
            if (ModelState.IsValid)
            {
                db.Estates.Update(model.Id, model.ToData());

                if (model.Type.Equals(EstateType.Apartment) || model.Type.Equals(EstateType.House))
                {
                    if (model.BuildingInfoId.HasValue)
                        return RedirectToAction("Update", "BuildingInfo", new { id = model.BuildingInfoId.Value, estateId = model.Id });

                    return RedirectToAction("Create", "BuildingInfo", new { estateId = model.Id });
                }

                if (model.Type.Equals(EstateType.Land) && model.BuildingInfoId.HasValue)
                    db.BuildingInfoes.Delete(model.BuildingInfoId.Value);

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        public ActionResult Delete(Guid? id)
        {
            if (!id.HasValue)
                return RedirectToAction("Index", "Home");

            var existing = db.Estates.GetById(id.Value);

            if (existing == null)
                return RedirectToAction("Index", "Home");

            var model = new EstateDeletionModel(existing.Id);

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
