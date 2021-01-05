using System;
using System.Web.Mvc;
using RealEstateManager.Models.BuildingInfo;
using RealEstateManager.Models.Data;
using RealEstateManager.Utils;

namespace RealEstateManager.Controllers
{
    public class BuildingInfoController : BaseController
    {
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Create(Guid? estateId)
        {
            var currentAccount = GetCurrentAgent(db, User);

            if (currentAccount == null ||
                !estateId.HasValue ||
                !EstateAgentHelper.IsAccountAuthorized(currentAccount, estateId.Value, db))
                return RedirectToAction("Index", "Home");

            var estate = db.Estates.GetById(estateId.Value, $"{nameof(Estate.BuildingInfo)}");

            if (estate == null || estate.BuildingInfo != null)
                return RedirectToAction("Index", "Home");

            var model = new BuildingInfoCreationModel
            {
                EstateId = estateId.Value,
            };

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

        public ActionResult Update(Guid? estateId)
        {
            var currentAccount = GetCurrentAgent(db, User);

            if (currentAccount == null ||
                !estateId.HasValue ||
                !EstateAgentHelper.IsAccountAuthorized(currentAccount, estateId.Value, db))
                return RedirectToAction("Index", "Home");

            var estate = db.Estates.GetById(estateId.Value, $"{nameof(Estate.BuildingInfo)}");

            if (estate?.BuildingInfo == null)
                return RedirectToAction("Index", "Home");

            var model = new BuildingInfoUpdateModel
            {
                Id = estate.BuildingInfo.Id,
                View = estate.BuildingInfo.View,
                Act16 = estate.BuildingInfo.Act16,
                Floors = estate.BuildingInfo.Floors,
                Bedrooms = estate.BuildingInfo.Bedrooms,
                Bathrooms = estate.BuildingInfo.Bathrooms,
                Balconies = estate.BuildingInfo.Balconies,
                Garages = estate.BuildingInfo.Garages
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

        public ActionResult Delete(Guid? estateId)
        {
            var currentAccount = GetCurrentAgent(db, User);

            if (currentAccount == null ||
                !estateId.HasValue ||
                !EstateAgentHelper.IsAccountAuthorized(currentAccount, estateId.Value, db))
                return RedirectToAction("Index", "Home");

            var estate = db.Estates.GetById(estateId.Value, $"{nameof(Estate.BuildingInfo)}");

            if (estate?.BuildingInfo == null)
                return RedirectToAction("Index", "Home");

            var model = new BuildingInfoDeletionModel
            {
                Id = estate.BuildingInfo.Id
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
