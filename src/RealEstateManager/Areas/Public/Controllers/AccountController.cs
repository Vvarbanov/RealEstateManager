using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PagedList;
using RealEstateManager.Areas.Public.Models.Account;
using RealEstateManager.Models.Data;
using RealEstateManager.Utils;

namespace RealEstateManager.Areas.Public.Controllers
{
    [AllowAnonymous]
    public class AccountController : BasePublicController
    {
        public ActionResult Index(string sortOrder = null, string currentFilter = null, int? page = null)
        {
            if (db.TryGetCurrentIdentity(User, out var currentIdentity) && currentIdentity.Type != UserType.Admin)
                return RedirectToAction("Index", "Home");

            ViewBag.CurrentFilter = currentFilter;
            ViewBag.CurrentSort = sortOrder;
            ViewBag.SortByType = "type";

            Func<IQueryable<Account>, IOrderedQueryable<Account>> orderFunc;

            switch (sortOrder)
            {
                case "type":
                    {
                        orderFunc = x => x.OrderBy(y => y.Type);

                        ViewBag.SortByType = "type_desc";

                        break;
                    }
                case "type_desc":
                    {
                        orderFunc = x => x.OrderByDescending(y => y.Type);

                        break;
                    }
                default:
                    {
                        orderFunc = null;

                        break;
                    }
            }

            Expression<Func<Account, bool>> filter = null;

            if (!string.IsNullOrWhiteSpace(currentFilter))
            {
                filter = x => x.Username.Contains(currentFilter) ||
                              x.EmailAddress.Contains(currentFilter);
            }

            var pageSize = ConfigReader.Pagination_PageSize;
            var pageNumber = page ?? 1;

            var model = db.Accounts
                .Get(filter, orderFunc)
                .Select(x => new AccountItemModel
                {
                    Id = x.Id,
                    Type = x.Type,
                    EmailAddress = x.EmailAddress,
                    Username = x.Username,
                })
                .ToPagedList(pageNumber, pageSize);

            return View(model);
        }

        public ActionResult Details()
        {
            if (!db.TryGetCurrentIdentity(User, out var identity))
                return RedirectToAction("Index", "Home");

            var account = db.Accounts.GetById(identity.Id);

            return View(new AccountInfoModel
            {
                Id = account.Id,
                EmailAddress = account.EmailAddress,
                PhoneNumber = account.PhoneNumber,
                Username = account.Username,
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(AccountInfoModel model)
        {
            if (ModelState.IsValid)
            {
                db.Accounts.Update(model.Id, model.ToData());

                db.Accounts.ChangePassword(model.Id, model.Password);
            }

            return View(model);
        }

        public ActionResult Register()
        {
            if (Request.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(AccountRegisterPublicUserModel model)
        {
            if (ModelState.IsValid)
            {
                var isUsernameAvailable = db.Accounts.IsUsernameAvailable(model.Username);
                var isEmailAddressAvailable = db.Accounts.IsEmailAddressAvailable(model.EmailAddress);

                if (!isUsernameAvailable)
                    ModelState.AddModelError(nameof(model.Username),
                        Localization.GetString("Public_AccountRegister_UsernameExists_Error"));

                if (!isEmailAddressAvailable)
                    ModelState.AddModelError(nameof(model.EmailAddress),
                        Localization.GetString("Public_AccountRegister_EmailAddressExists_Error"));

                if (!isUsernameAvailable || !isEmailAddressAvailable)
                    return View(model);

                db.Accounts.Insert(model.ToData());
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        public ActionResult RegisterAgent()
        {
            if (Request.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterAgent(AccountRegisterAgentModel model)
        {
            if (ModelState.IsValid)
            {
                var isUsernameAvailable = db.Accounts.IsUsernameAvailable(model.Username);
                var isEmailAddressAvailable = db.Accounts.IsEmailAddressAvailable(model.EmailAddress);

                if (!isUsernameAvailable)
                    ModelState.AddModelError(nameof(model.Username),
                        Localization.GetString("Public_AccountRegister_UsernameExists_Error"));

                if (!isEmailAddressAvailable)
                    ModelState.AddModelError(nameof(model.EmailAddress),
                        Localization.GetString("Public_AccountRegister_EmailAddressExists_Error"));

                if (!isUsernameAvailable || !isEmailAddressAvailable)
                    return View(model);

                db.Accounts.Insert(model.ToData());
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        public ActionResult RegisterAdmin()
        {
            if (Request.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterAdmin(AccountRegisterAdminModel model)
        {
            if (ModelState.IsValid)
            {
                var isUsernameAvailable = db.Accounts.IsUsernameAvailable(model.Username);
                var isEmailAddressAvailable = db.Accounts.IsEmailAddressAvailable(model.EmailAddress);

                if (!isUsernameAvailable)
                    ModelState.AddModelError(nameof(model.Username),
                        Localization.GetString("Public_AccountRegister_UsernameExists_Error"));

                if (!isEmailAddressAvailable)
                    ModelState.AddModelError(nameof(model.EmailAddress),
                        Localization.GetString("Public_AccountRegister_EmailAddressExists_Error"));

                if (!isUsernameAvailable || !isEmailAddressAvailable)
                    return View(model);

                db.Accounts.Insert(model.ToData());
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        public ActionResult Login(string returnUrl = null)
        {
            if (Request.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(AccountLoginModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var isValidUser = db.Accounts
                    .IsValidUser(model.EmailAddress, model.Password, out var userId);

                if (isValidUser)
                {
                    db.SetCurrentIdentity(userId, model.RememberMe);

                    return RedirectToReturnUrlOrHome(returnUrl);
                }

                ModelState.AddModelError(string.Empty, Localization.GetString("InvalidLoginDetails_Error"));
            }

            return View(model);
        }

        [Authorize]
        public ActionResult Logout()
        {
            if (Request.Cookies[FormsAuthentication.FormsCookieName] == null)
                return RedirectToAction("Index", "Home");

            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName)
            {
                Path = Url.Content("~/"),
                Expires = DateTime.Now.AddDays(-1d),
                Secure = FormsAuthentication.RequireSSL
            };

            if (FormsAuthentication.CookieDomain != null)
            {
                cookie.Domain = FormsAuthentication.CookieDomain;
            }

            Response.Cookies.Remove(FormsAuthentication.FormsCookieName);
            Response.Cookies.Add(cookie);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult ForgottenPassword()
        {
            if (Request.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgottenPassword(AccountForgottenPasswordModel model)
        {
            if (ModelState.IsValid && Request.Url != null)
            {
                var token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                var recoveryUrl =
                    $"{Request.Url.Scheme}://{Request.Url.Authority}{Url.Action("RecoverPassword", "Account", new { token })}";

                if (await db.Accounts.TrySendForgottenPasswordEmailAsync(model.EmailAddress, token, recoveryUrl))
                {
                    return RedirectToAction("OperationSuccessful", "Home");
                }
            }

            return View(model);
        }

        public ActionResult RecoverPassword(string token)
        {
            return View(new AccountRecoverPasswordModel
            {
                Token = token,
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RecoverPassword(AccountRecoverPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                if (db.Accounts.TryRecoverAccount(model.Token, model.NewPassword))
                    return RedirectToAction("Login", "Account");

                ModelState.AddModelError(string.Empty,
                    Localization.GetString("Public_AccountRecoverPasswordModel_InvalidToken_Error"));
            }

            return View(model);
        }

        public ActionResult Ban(Guid? accountId)
        {
            if (!accountId.HasValue)
                return RedirectToAction("Index", "Home");

            var currentIdentity = GetCurrentIdentity(db, User);

            if (currentIdentity == null || currentIdentity.Type != UserType.Admin)
                return RedirectToAction("Index", "Home");

            var model = new AccountDeleteModel
            {
                Id = accountId.Value,
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Ban(AccountDeleteModel model)
        {
            if (ModelState.IsValid)
            {
                var account = db.Accounts.GetById(model.Id);

                if (account == null || account.Type == UserType.Admin)
                    return RedirectToAction("Index", "Home");

                db.Accounts.Delete(account.Id);

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }
    }
}
