using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MvcSn.Validation
{
    public class AdminValidationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var isAdmin = context.HttpContext.Request.Cookies["role"];
            if (isAdmin == "user")
                context.Result = new RedirectToActionResult("Index", "Home", null);
            base.OnActionExecuting(context);
        }
    }
}
