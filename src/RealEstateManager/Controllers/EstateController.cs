using System;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using PagedList;
using RealEstateManager.Models.Estate;
using RealEstateManager.Models.Data;
using RealEstateManager.Models.BuildingInfo;
using RealEstateManager.Utils;

namespace RealEstateManager.Controllers
{
    [Authorize]
    public class EstateController : BaseController
    {
        public ActionResult Index(string sortOrder = null, string currentFilter = null, int? page = null)
        {
            ViewBag.CurrentFilter = currentFilter;
            ViewBag.CurrentSort = sortOrder;
            ViewBag.SortByName = "name";
            ViewBag.SortByType = "type";
            ViewBag.SortByStatus = "status";
            ViewBag.SortByArea = "area";
            ViewBag.SortByPrice = "price";

            Func<IQueryable<Estate>, IOrderedQueryable<Estate>> orderFunc;

            #region A large fucking switch statement
            switch (sortOrder)
            {
                case "name":
                {
                    orderFunc = x => x
                        .OrderBy(y => y.Name)
                        .ThenByDescending(y => y.UpdateDate);

                    ViewBag.SortByName = "name_desc";

                    break;
                }
                case "name_desc":
                {
                    orderFunc = x => x
                        .OrderByDescending(y => y.Name)
                        .ThenByDescending(y => y.UpdateDate);

                    break;
                }
                case "type":
                {
                    orderFunc = x => x
                        .OrderBy(y => y.Type)
                        .ThenByDescending(y => y.UpdateDate);

                    ViewBag.SortByType = "type_desc";

                    break;
                }
                case "type_desc":
                {
                    orderFunc = x => x
                        .OrderByDescending(y => y.Type)
                        .ThenByDescending(y => y.UpdateDate);
                    
                    break;
                }
                case "status":
                {
                    orderFunc = x => x
                        .OrderBy(y => y.Status)
                        .ThenByDescending(y => y.UpdateDate);

                    ViewBag.SortByStatus = "status_desc";

                    break;
                }
                case "status_desc":
                {
                    orderFunc = x => x
                        .OrderByDescending(y => y.Status)
                        .ThenByDescending(y => y.UpdateDate);
                    
                    break;
                }
                case "area":
                {
                    orderFunc = x => x
                        .OrderBy(y => y.Area)
                        .ThenByDescending(y => y.UpdateDate);

                    ViewBag.SortByArea = "area_desc";

                    break;
                }
                case "area_desc":
                {
                    orderFunc = x => x
                        .OrderByDescending(y => y.Area)
                        .ThenByDescending(y => y.UpdateDate);

                    break;
                }
                case "price":
                {
                    orderFunc = x => x
                        .OrderBy(y => y.Price)
                        .ThenByDescending(y => y.UpdateDate);

                    ViewBag.SortByPrice = "price_desc";

                    break;
                }
                case "price_desc":
                {
                    orderFunc = x => x
                        .OrderByDescending(y => y.Price)
                        .ThenByDescending(y => y.UpdateDate);

                    break;
                }
                default:
                {
                    orderFunc = x => x.OrderByDescending(y => y.UpdateDate);
                    break;
                }
            }
            #endregion

            Expression<Func<Estate, bool>> filter = null;

            if (!string.IsNullOrWhiteSpace(currentFilter))
            {
                filter = x =>
                    x.Name.Contains(currentFilter) || 
                    x.Address.Contains(currentFilter) ||
                    x.PublicDescription.Contains(currentFilter);
            }

            var pageSize = ConfigReader.Pagination_PageSize;
            var pageNumber = page ?? 1;

            var model = db.Estates
                .Get(filter, orderFunc)
                .Select(x => new EstateGetModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Type = x.Type,
                    Price = x.Price,
                    Status = x.Status,
                    Address = x.Address,
                    PublicDescription = x.PublicDescription,
                    PrivateDescription = x.PrivateDescription,
                    Area = x.Area,
                })
                .ToPagedList(pageNumber, pageSize);

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
                    Id = existing.BuildingInfo.Id,
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
                PrivateDescription = existing.PrivateDescription,
                Area = existing.Area,
                BuildingInfoGetModel = buildingInfoModel,
                ImagePaths = existing.FilePathsCSV
                    ?.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Replace(Request.ServerVariables["APPL_PHYSICAL_PATH"], "\\"))
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
                Directory.CreateDirectory(Server.MapPath(ConfigReader.ImageUploadDirectory));

                string filesPathCSV = null;

                if (model.Images != null)
                {
                    var safeImages = model.GetSafeImages(Server);

                    filesPathCSV = string.Join(",", safeImages.Select(x => x.SaveLocation));

                    foreach (var item in safeImages)
                    {
                        item.File.SaveAs(item.SaveLocation);
                    }
                }

                var estate = db.Estates.Insert(model.ToData(filesPathCSV));

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
                Area = existing.Area,
                BuildingInfoId = existing.BuildingInfoId,
                ExistingImagePaths = existing.FilePathsCSV
                    ?.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Replace(Request.ServerVariables["APPL_PHYSICAL_PATH"], "\\"))
                    .ToList(),
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(EstateUpdateModel model)
        {
            if (ModelState.IsValid)
            {
                Directory.CreateDirectory(Server.MapPath(ConfigReader.ImageUploadDirectory));

                var filesPathCSV = model.ExistingImagePathsAsCSV(Server);

                if (model.Images != null)
                {
                    var safeImages = model.GetSafeImages(Server);

                    var newFilesPathsCSV = string.Join(",", safeImages.Select(x => x.SaveLocation));

                    if (!string.IsNullOrWhiteSpace(newFilesPathsCSV))
                        filesPathCSV = string.Join(",", filesPathCSV, newFilesPathsCSV);

                    foreach (var item in safeImages)
                    {
                        item.File.SaveAs(item.SaveLocation);
                    }
                }

                db.Estates.Update(model.Id, model.ToData(filesPathCSV));

                switch (model.Type)
                {
                    case EstateType.Apartment:
                    case EstateType.House:
                    {
                        return model.BuildingInfoId.HasValue
                            ? RedirectToAction("Update", "BuildingInfo",
                                new {id = model.BuildingInfoId.Value, estateId = model.Id})
                            : RedirectToAction("Create", "BuildingInfo", new {estateId = model.Id});
                    }
                    case EstateType.Land when model.BuildingInfoId.HasValue:
                    {
                        db.BuildingInfoes.Delete(model.BuildingInfoId.Value);
                        break;
                    }
                }

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
