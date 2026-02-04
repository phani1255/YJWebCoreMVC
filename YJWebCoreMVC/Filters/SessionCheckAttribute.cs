using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace YJWebCoreMVC.Filters
{
    public class SessionCheckAttribute: ActionFilterAttribute
    {
        private readonly string _sessionKey;

        public SessionCheckAttribute(string sessionKey)
        {
            _sessionKey = sessionKey;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var session = context.HttpContext.Session;

            if (session == null || string.IsNullOrEmpty(session.GetString(_sessionKey)))
            {
                context.Result = new RedirectToActionResult("Index", "Login", null);
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}
