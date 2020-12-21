using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using PagedList;
using RealEstateManager.Areas.Public.Models.BuildingInfo;
using RealEstateManager.Areas.Public.Models.Estate;
using RealEstateManager.Models.Data;
using RealEstateManager.Utils;

namespace RealEstateManager.Areas.Public.Controllers
{
    public class EstateController : BasePublicController
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

            Expression<Func<Estate, bool>> filter = x =>
                x.Status != EstateStatusType.Sold &&
                x.Status != EstateStatusType.RentedOut &&
                (currentFilter == null || 
                    x.Name.Contains(currentFilter) || 
                    x.Address.Contains(currentFilter) || 
                    x.PublicDescription.Contains(currentFilter));

            var pageSize = ConfigReader.Pagination_PageSize;
            var pageNumber = page ?? 1;

            var model = db.Estates
                .Get(filter, orderFunc)
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
