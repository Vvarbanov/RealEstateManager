using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using RealEstateManager.Models.Agent;
using RealEstateManager.Utils;

namespace RealEstateManager.Controllers
{
    [Authorize]
    public class AgentController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl = null)
        {
            if (Request.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(AgentLoginModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var isValidAgent = db.Agents
                    .IsValidAgent(model.EmailAddress, model.Password, out var username);

                if (isValidAgent)
                {
                    FormsAuthentication.SetAuthCookie(username, model.RememberMe);

                    return RedirectToReturnUrlOrHome(returnUrl);
                }

                ModelState.AddModelError(string.Empty, Localization.GetString("InvalidLoginDetailsError"));
            }

            return View(model);
        }

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

        [AllowAnonymous]
        public ActionResult Register()
        {
            if (Request.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(AgentRegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var isUsernameAvailable = db.Agents.IsUsernameAvailable(model.Username);
                var isEmailAddressAvailable = db.Agents.IsEmailAddressAvailable(model.EmailAddress);

                if (!isUsernameAvailable)
                    ModelState.AddModelError(nameof(model.Username),
                        Localization.GetString("AgentRegister_UsernameExists_Error"));

                if (!isEmailAddressAvailable)
                    ModelState.AddModelError(nameof(model.EmailAddress),
                        Localization.GetString("AgentRegister_EmailAddressExists_Error"));

                if (!isUsernameAvailable || !isEmailAddressAvailable)
                    return View(model);

                db.Agents.Insert(model.ToData());
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }
    }
}
