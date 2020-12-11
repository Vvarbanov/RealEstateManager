using System.Security.Principal;
using System.Web.Mvc;
using RealEstateManager.Filters;
using RealEstateManager.Models.Data;
using RealEstateManager.Repository;

namespace RealEstateManager.Controllers
{
    [UserFilter]
    public abstract class BaseController : Controller
    {
        private EstatesContext _db;

        protected EstatesContext db => _db ?? (_db = new EstatesContext());

        public EstatesContext Context => db;

        public ActionResult RedirectToReturnUrlOrHome(string returnUrl)
        {
            if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Index", "Home");
        }

        public static BaseController GetController(ViewContext viewContext)
        {
            return (BaseController)viewContext.Controller;
        }

        public static CurrentIdentity GetCurrentAgent(EstatesContext db, IPrincipal user)
        {
            return db.TryGetCurrentIdentity(user, out var identity) && identity.Type != UserType.PublicUser
                ? identity
                : null;
        }

        protected override void Dispose(bool disposing)
        {
            if (_db != null)
            {
                _db.Dispose();
                _db = null;
            }

            base.Dispose(disposing);
        }
    }
}
