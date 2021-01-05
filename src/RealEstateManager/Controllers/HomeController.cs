using System.Web.Mvc;

namespace RealEstateManager.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Estate");
        }
    }
}
