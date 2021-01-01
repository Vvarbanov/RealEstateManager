using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using PagedList;
using RealEstateManager.Areas.Public.Models.Contact;
using RealEstateManager.Models.Data;
using RealEstateManager.Repository.Data;
using RealEstateManager.Utils;

namespace RealEstateManager.Areas.Public.Controllers
{
    public class ContactController : BasePublicController
    {
        public ActionResult Create(Guid? estateId)
        {
            if (!estateId.HasValue)
                return RedirectToAction("Index", "Home");

            db.TryGetCurrentIdentity(User, out var currentIdentity);
            if (!EstateAgentHelper.IsAccountPublicUser(currentIdentity))
                return RedirectToAction("Index", "Home");

            var model = new ContactCreationModel
            {
                EstateId = estateId.Value
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ContactCreationModel model)
        {
            if (ModelState.IsValid)
            {
                var contact = db.Contacts.Insert(model.ToData());
                db.ContactAccounts.Insert(new ContactAccountData
                {
                    AccountId = GetCurrentIdentity(db, User).Id,
                    ContactId = contact.Id
                });

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }
    }
}