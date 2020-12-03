using System.Web.Mvc;
using RealEstateManager.Repository;

namespace RealEstateManager.Controllers
{
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
