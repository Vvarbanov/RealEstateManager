using System;
using System.Linq;
using System.Web.Mvc;
using RealEstateManager.Models.EstateAccount;

namespace RealEstateManager.Controllers
{
    public class EstateAccountController : BaseController
    {
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Update(Guid? estateId)
        {
            if (!estateId.HasValue)
                return RedirectToAction("Index", "Home");

            var estate = db.Estates.GetById(estateId.Value);

            if (estate != null)
                return RedirectToAction("Index", "Home");

            var model = new EstateAccountListModel();
            model.GetEstateAccounts(db, estateId.Value);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(EstateAccountListModel model)
        {
            if (ModelState.IsValid)
            {
                db.EstateAccounts.Update(model);
            }

            return View(model);
        }
    }
}