using System;
using System.Net;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;

namespace RealEstateManager.Utils
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ThrottleAttribute : ActionFilterAttribute
    {
        public string Name { get; set; }

        public int Seconds { get; set; }

        public string Message { get; set; }

        public override void OnActionExecuting(ActionExecutingContext c)
        {
            // Create a key based on Name property and request ip address
            var key = string.Concat(Name, "-", c.HttpContext.Request.UserHostAddress);
            var allowExecute = false;

            // If no key is found in cache, add object based on created key
            if (HttpRuntime.Cache[key] == null)
            {
                HttpRuntime.Cache.Add(key,
                    true, // smallest data we can have
                    null, // no dependencies
                    DateTime.Now.AddSeconds(Seconds),
                    Cache.NoSlidingExpiration,
                    CacheItemPriority.Low,
                    null); // no callback

                allowExecute = true;
            }

            // if key already exists, do not allow execution
            if (!allowExecute)
            {
                if (String.IsNullOrEmpty(Message))
                    Message = "You may only perform this action every {n} seconds.";

                c.Result = new ContentResult { Content = Message.Replace("{n}", Seconds.ToString()) };
                c.HttpContext.Response.StatusCode = (int)HttpStatusCode.Conflict;
            }
        }
    }
}