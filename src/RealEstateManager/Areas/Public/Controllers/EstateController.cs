using System;
using System.Linq;
using System.Web.Mvc;
using RealEstateManager.Areas.Public.Models.BuildingInfo;
using RealEstateManager.Areas.Public.Models.Estate;
using RealEstateManager.Models.Data;

namespace RealEstateManager.Areas.Public.Controllers
{
    public class EstateController : BasePublicController
    {
        public ActionResult Index()
        {
            var existing = db.Estates
                .Get(x => x.Status != EstateStatusType.Sold && x.Status != EstateStatusType.RentedOut,
                    x => x.OrderByDescending(y => y.UpdateDate));

            var model = existing
                .AsEnumerable()
                .Select(x => new EstateGetModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Type = x.Type,
                    Price = x.Price,
                    Status = x.Status,
                    Address = x.Address,
                    PublicDescription = x.PublicDescription,
                    Area = x.Area,
                    ExistingImagePaths = x.FilePathsCSV
                        ?.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(y => y.Replace(Request.ServerVariables["APPL_PHYSICAL_PATH"], "\\"))
                        .ToList(),
                })
                .ToList();

            return View(model);
        }

        public ActionResult Details(Guid? id)
        {
            if (!id.HasValue)
                return RedirectToAction("Index", "Home");

            var existing = db.Estates.GetById(id.Value, "BuildingInfo");

            if (existing == null)
                return RedirectToAction("Index", "Home");

            BuildingInfoGetModel buildingInfoModel = null;

            if (existing.Type == EstateType.House || existing.Type == EstateType.Apartment)
            {
                buildingInfoModel = new BuildingInfoGetModel
                {
                    Act16 = existing.BuildingInfo.Act16,
                    View = existing.BuildingInfo.View,
                    Floors = existing.BuildingInfo.Floors,
                    Bedrooms = existing.BuildingInfo.Bedrooms,
                    Bathrooms = existing.BuildingInfo.Bathrooms,
                    Balconies = existing.BuildingInfo.Balconies,
                    Garages = existing.BuildingInfo.Garages
                };
            }

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
                BuildingInfoGetModel = buildingInfoModel,
                ExistingImagePaths = existing.FilePathsCSV
                    ?.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(y => y.Replace(Request.ServerVariables["APPL_PHYSICAL_PATH"], "\\"))
                    .ToList()
            };

            return View(estateModel);
        }
    }
}
