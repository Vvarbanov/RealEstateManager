using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using PagedList;
using RealEstateManager.Models.Contact;
using RealEstateManager.Models.Data;
using RealEstateManager.Utils;
using RealEstateManager.Repository.Data;

namespace RealEstateManager.Controllers
{
    public class ContactController : BaseController
    {
        public ActionResult Index(string sortOrder = null, string currentFilter = null, int? page = null, Guid? estateId = null, Guid? accountId = null)
        {
            ViewBag.CurrentFilter = currentFilter;
            ViewBag.CurrentSort = sortOrder;
            ViewBag.SortByDateTime = "date_time";
            ViewBag.SortByNumber = "number";

            Func<IQueryable<Contact>, IOrderedQueryable<Contact>> orderFunc;

            #region A large fucking switch statement
            switch (sortOrder)
            {
                case "date_time":
                    {
                        orderFunc = x => x
                            .OrderBy(y => y.DateTime)
                            .ThenByDescending(y => y.Id);

                        ViewBag.SortByDateTime = "date_time_desc";

                        break;
                    }
                case "date_time_desc":
                    {
                        orderFunc = x => x
                            .OrderByDescending(y => y.DateTime)
                            .ThenByDescending(y => y.Id);

                        break;
                    }
                case "number":
                    {
                        orderFunc = x => x
                            .OrderBy(y => y.Number)
                            .ThenByDescending(y => y.Id);

                        ViewBag.SortByNumber = "number_desc";

                        break;
                    }
                case "number_desc":
                    {
                        orderFunc = x => x
                            .OrderByDescending(y => y.Number)
                            .ThenByDescending(y => y.Id);

                        break;
                    }
                default:
                    {
                        orderFunc = x => x.OrderByDescending(y => y.Id);
                        break;
                    }
            }
            #endregion

            Expression<Func<Contact, bool>> filter = null;

            filter = x => (!estateId.HasValue ||
                            x.EstateId == estateId.Value) &&
                          (!accountId.HasValue ||
                            x.ContactAccounts.Any(y => y.AccountId == accountId.Value)) &&
                          (currentFilter == null ||
                            x.Outcome.Contains(currentFilter) ||
                            x.Number.Contains(currentFilter));

            var pageSize = ConfigReader.Pagination_PageSize;
            var pageNumber = page ?? 1;

            var model = db.Contacts
                .Get(filter, orderFunc)
                .Select(x => new ContactGetModel
                {
                    Id = x.Id,
                    EstateId = x.EstateId,
                    DateTime = x.DateTime,
                    Number = x.Number,
                    Outcome = x.Outcome
                })
                .ToPagedList(pageNumber, pageSize);

            return View(model);
        }

        public ActionResult Details(Guid? id)
        {
            if (!id.HasValue)
                return RedirectToAction("Index", "Home");

            var existing = db.Contacts.GetById(id.Value);

            if (existing == null)
                return RedirectToAction("Index", "Home");

            var estateModel = new ContactGetModel
            {
                Id = existing.Id,
                EstateId = existing.EstateId,
                DateTime = existing.DateTime,
                Number = existing.Number,
                Outcome = existing.Outcome,
                ImagePaths = existing.FilePathsCSV
                    ?.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Replace(Request.ServerVariables["APPL_PHYSICAL_PATH"], "\\"))
                    .ToList(),
            };

            return View(estateModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(ContactGetModel model)
        {
            db.ContactAccounts.Insert(new ContactAccountData
            {
                ContactId = model.Id,
                AccountId = GetCurrentAgent(db, User).Id
            });
            return RedirectToAction("Index");
        }

        public ActionResult Update(Guid? id)
        {
            if (!id.HasValue)
                return RedirectToAction("Index", "Home");

            var existing = db.Contacts.GetById(id.Value);

            if (existing == null)
                return RedirectToAction("Index", "Home");

            var model = new ContactUpdateModel
            {
                Id = existing.Id,
                EstateId = existing.EstateId,
                DateTime = existing.DateTime,
                Outcome = existing.Outcome,
                Number = existing.Number,
                ExistingImagePaths = existing.FilePathsCSV
                    ?.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Replace(Request.ServerVariables["APPL_PHYSICAL_PATH"], "\\"))
                    .ToList(),
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(ContactUpdateModel model)
        {
            if (ModelState.IsValid)
            {
                var currentAgent = GetCurrentAgent(db, User);
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

                db.Contacts.Update(model.Id, model.ToData(filesPathCSV));

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }
    }
}
