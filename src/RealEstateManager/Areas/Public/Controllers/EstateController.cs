using RealEstateManager.Areas.Public.Models.Estate;
using RealEstateManager.Areas.Public.Models.BuildingInfo;
using System;
using System.Linq;
using System.Web.Mvc;

namespace RealEstateManager.Areas.Public.Controllers
{
    public class EstateController : BasePublicController
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

            if (existing == null || existing.BuildingInfo == null)
                return RedirectToAction("Index", "Home");

            var buildingInfoModel = new BuildingInfoGetModel
            {
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
                Area = existing.Area,
                BuildingInfoGetModel = buildingInfoModel
            };

            return View(estateModel);
        }
    }
}