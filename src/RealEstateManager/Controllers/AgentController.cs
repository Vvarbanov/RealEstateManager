using System;
using System.Threading.Tasks;
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
        public ActionResult Index(Guid? id)
        {
            if (id.HasValue)
            {
                var agent = db.Agents.GetById(id.Value);

                if (agent != null)
                {
                    return View(new AgentInfoModel
                    {
                        Id = id.Value,
                        EmailAddress = agent.EmailAddress,
                        PhoneNumber = agent.PhoneNumber,
                        Username = agent.Username,
                    });
                }
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(AgentInfoModel model)
        {
            if (ModelState.IsValid)
            {
                db.Agents.Update(model.Id, model.ToUpdateData());

                db.Agents.ChangePassword(model.Id, model.Password);
            }

            return View(model);
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
                    .IsValidAgent(model.EmailAddress, model.Password, out var username, out var id);

                if (isValidAgent)
                {
                    db.SetCurrentIdentityAgent(id, username, model.RememberMe);

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

        [AllowAnonymous]
        public ActionResult ForgottenPassword()
        {
            if (Request.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgottenPassword(AgentForgottenPasswordModel model)
        {
            if (ModelState.IsValid && Request.Url != null)
            {
                var token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                var recoveryUrl =
                    $"{Request.Url.Scheme}://{Request.Url.Authority}{Url.Action("RecoverPassword", "Agent", new {token})}";

                if (await db.Agents.TrySendForgottenPasswordEmailAsync(model.EmailAddress, token, recoveryUrl))
                {
                    return RedirectToAction("OperationSuccessful", "Home");
                }
            }

            return View(model);
        }

        [AllowAnonymous]
        public ActionResult RecoverPassword(string token)
        {
            return View(new AgentRecoverPasswordModel
            {
                Token = token,
            });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult RecoverPassword(AgentRecoverPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                if (db.Agents.TryRecoverAccount(model.Token, model.NewPassword))
                    return RedirectToAction("Login", "Agent");

                ModelState.AddModelError(string.Empty,
                    Localization.GetString("AgentRecoverPasswordModel_InvalidToken_Error"));
            }

            return View(model);
        }
    }
}
