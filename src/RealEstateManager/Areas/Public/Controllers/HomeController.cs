using System.Web.Mvc;

namespace RealEstateManager.Areas.Public.Controllers
{
    public class HomeController : BasePublicController
    {
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Estate");
        }
    }
}
