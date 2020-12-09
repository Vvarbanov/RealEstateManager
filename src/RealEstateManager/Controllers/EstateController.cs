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

        public ActionResult Details(Guid? id)
        {
            if (!id.HasValue)
                return RedirectToAction("Index", "Home");

            var existing = db.Estates.GetById(id.Value, "BuildingInfo");

            if (existing == null)
                return RedirectToAction("Index", "Home");

            var model = new EstateGetModel
            {
                Id = existing.Id,
                Name = existing.Name,
                Type = existing.Type,
                Price = existing.Price,
                Status = existing.Status,
                Address = existing.Address,
                PublicDescription = existing.PublicDescription,
                PrivateDescription = existing.PrivateDescription,
                Area = existing.Area,
                BuildingInfo = existing.BuildingInfo
            };

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

            var model = new EstateUpdateModel
            {
                Id = existing.Id,
                Name = existing.Name,
                Type = existing.Type,
                Address = existing.Address,
                Price = existing.Price,
                Status = existing.Status,
                PublicDescription = existing.PublicDescription,
                PrivateDescription = existing.PrivateDescription,
                Area = existing.Area
            };

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

            var model = new EstateDeletionModel
            {
                Id = existing.Id,
                BuildingInfoId = existing.BuildingInfoId
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(EstateDeletionModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.BuildingInfoId.HasValue)
                    db.BuildingInfoes.Delete(model.BuildingInfoId.Value);

                db.Estates.Delete(model.Id);
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }
    }
}
