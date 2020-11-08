using System.Web.Mvc;

namespace RealEstateManager.Areas.Public.Controllers
{
    public class HomeController : Controller
    {
        // GET: Public/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}
