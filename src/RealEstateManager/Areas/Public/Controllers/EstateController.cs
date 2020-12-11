using RealEstateManager.Areas.Public.Models.Estate;
using System;
using System.Linq;
using System.Web.Mvc;

namespace RealEstateManager.Areas.Public.Controllers
{
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
                Area = existing.Area,
                BuildingInfo = existing.BuildingInfo
            };

            return View(model);
        }
    }
}