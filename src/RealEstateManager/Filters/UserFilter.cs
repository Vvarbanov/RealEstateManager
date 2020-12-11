using System;
using System.Web.Mvc;
using RealEstateManager.Controllers;

namespace RealEstateManager.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Interface)]
    public class UserFilter : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.Controller is BaseController baseController)
            {
                var user = filterContext.HttpContext?.User;
                var db = baseController.Context;
                var currentAgent = BaseController.GetCurrentAgent(db, user);

                if (currentAgent == null)
                    filterContext.Result = new HttpUnauthorizedResult();
            }
        }
    }
}
