using System.Web.Mvc;

namespace RealEstateManager.Areas.Public.Controllers
{
    public class HomeController : BasePublicController
    {
        // GET: Public/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}
