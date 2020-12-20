using System.Web;

namespace RealEstateManager.Models.Estate
{
    public class EstateImageModel
    {
        public HttpPostedFileBase File { get; set; }

        public string SaveLocation { get; set; }
    }
}
