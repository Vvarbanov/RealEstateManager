using System.Web.Mvc;

namespace RealEstateManager.Areas.Public.Controllers
{
    public class BuildingInfoController : BaseController
    {
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}