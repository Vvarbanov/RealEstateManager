using System.Web.Mvc;

namespace RealEstateManager.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return Request.IsAuthenticated 
                ? RedirectToAction("Index", "Estate") 
                : RedirectToAction("Login", "Agent");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
