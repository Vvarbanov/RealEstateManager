using System.Web.Mvc;

namespace RealEstateManager.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Estate");
        }
    }
}
