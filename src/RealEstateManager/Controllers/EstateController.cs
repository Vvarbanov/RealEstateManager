using System.Web.Mvc;

namespace RealEstateManager.Controllers
{
    [Authorize]
    public class EstateController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
