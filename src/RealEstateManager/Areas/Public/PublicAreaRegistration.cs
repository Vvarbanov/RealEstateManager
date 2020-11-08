using System.Web.Mvc;

namespace RealEstateManager.Areas.Public
{
    public class PublicAreaRegistration : AreaRegistration 
    {
        public override string AreaName => "Public";

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                name: "Public_default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new []{ "RealEstateManager.Areas.Public.Controllers" }
            );
        }
    }
}
