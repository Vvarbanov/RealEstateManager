using System;
using System.Linq;
using System.Web.Mvc;
using RealEstateManager.Models.EstateAccount;
using RealEstateManager.Models.Data;
using RealEstateManager.Repository.Data;

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

            if (estate == null)
                return RedirectToAction("Index", "Home");

            var accounts = db.Accounts.Get(x => x.Type == UserType.Agent, null, nameof(Account.EstateAccounts));

            var model = new EstateAccountListModel
            {
                EstateId = estateId.Value,
                EstateAccounts = accounts
                    .Select(x => new EstateAccountModel
                    {
                        EstateId = estateId.Value,
                        AccountId = x.Id,
                        Username = x.Username,
                        HasRights = x.EstateAccounts.Any(y => y.EstateId == estateId)
                    })
                    .ToList(),
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(EstateAccountListModel model)
        {
            if (ModelState.IsValid)
            {
                db.Estates.UpdateRights(
                    model.EstateId,
                    model.EstateAccounts
                        .Select(x => x.ToData())
                        .ToList());

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }
    }
}
