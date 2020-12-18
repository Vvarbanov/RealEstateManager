using System;
using System.Linq;
using System.Web.Mvc;
using RealEstateManager.Models.EstateAccount;
using RealEstateManager.Models.Data;
using RealEstateManager.Repository.Data;
using System.Collections.Generic;

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

            var agents = db.Accounts.Get(x => x.Type == UserType.Agent, null, nameof(Account.Estates));

            var model = agents
                .Select(x => new EstateAccountModel
                {
                    EstateId = estateId.Value,
                    UserId = x.Id,
                    Username = x.Username,
                    HasRights = x.Estates.Any(y => y.EstateId == estateId)
                })
                .ToList();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(List<EstateAccountModel> model)
        {
            if (ModelState.IsValid)
            {
                db.EstateAccounts.Update(model.
                    Select(x => new EstateAccountData
                    {
                        EstateId = x.EstateId,
                        AccountId = x.UserId,
                        HasRights = x.HasRights
                    }).ToList());

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }
    }
}