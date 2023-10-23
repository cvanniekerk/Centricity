using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Centricity.Data
{
    public class CORS : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {                        
            context.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            context.HttpContext.Response.Headers.Add("Access-Control-Allow-Methods", "GET,POST,OPTIONS,DELETE,PUT,OPTIONS");            
            base.OnActionExecuting(context);
        }
    }
}
