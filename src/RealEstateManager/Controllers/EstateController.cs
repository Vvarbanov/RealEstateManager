using System;
using System.Linq;
using System.Web.Mvc;
using RealEstateManager.Models.Estate;
using RealEstateManager.Models.Data;
using RealEstateManager.Models.BuildingInfo;
using RealEstateManager.Models.EstateAccount;

namespace RealEstateManager.Controllers
{
    [Authorize]
    public class EstateController : BaseController
    {
        public ActionResult Index()
        {
            var existing = db.Estates.Get(null, x => x.OrderByDescending(y => y.UpdateDate), nameof(Estate.Account));

            var model = new EstateListGetModel(existing);

            return View(model);
        }

        public ActionResult Details(Guid? id)
        {
            if (!id.HasValue)
                return RedirectToAction("Index", "Home");

            var existing = db.Estates.GetById(id.Value, nameof(Estate.BuildingInfo) + "," + nameof(Estate.Account));

            if (existing == null)
                return RedirectToAction("Index", "Home");

            var buildingInfoModel = new BuildingInfoGetModel
            {
                Id = existing.BuildingInfo.Id,
                Act16 = existing.BuildingInfo.Act16,
                View = existing.BuildingInfo.View,
                Floors = existing.BuildingInfo.Floors,
                Bedrooms = existing.BuildingInfo.Bedrooms,
                Bathrooms = existing.BuildingInfo.Bathrooms,
                Balconies = existing.BuildingInfo.Balconies,
                Garages = existing.BuildingInfo.Garages
            };

            var estateModel = new EstateGetModel
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
                BuildingInfoGetModel = buildingInfoModel,
                EstateAgents = existing.Account
                        .Select(x => new EstateAccountModel
                        {
                            EstateId = x.EstateId,
                            UserId = x.AccountId
                        })
                        .ToList()
            };

            return View(estateModel);
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

                if (model.Type == EstateType.Apartment || model.Type == EstateType.House)
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

                if (model.Type == EstateType.Apartment || model.Type == EstateType.House)
                {
                    if (model.BuildingInfoId.HasValue)
                        return RedirectToAction("Update", "BuildingInfo", new { id = model.BuildingInfoId.Value, estateId = model.Id });

                    return RedirectToAction("Create", "BuildingInfo", new { estateId = model.Id });
                }

                if (model.Type == EstateType.Land && model.BuildingInfoId.HasValue)
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
