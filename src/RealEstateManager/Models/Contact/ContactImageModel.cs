using System.Web;

namespace RealEstateManager.Models.Contact
{
    public class ContactImageModel
    {
        public HttpPostedFileBase File { get; set; }

        public string SaveLocation { get; set; }
    }
}