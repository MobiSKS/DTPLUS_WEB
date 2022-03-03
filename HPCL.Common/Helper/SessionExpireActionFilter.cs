using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Helper
{
    public class SessionExpireActionFilter : IAsyncActionFilter
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public SessionExpireActionFilter(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //To do : before the action executes
            if (_httpContextAccessor.HttpContext.Session.GetString("UserId") == null)
            {
                context.Result =new RedirectToRouteResult(new RouteValueDictionary
                                    {
                                        { "action", "Index" },
                                        { "controller", "Home" },
                                        { "returnUrl", "~/Home/Index"}
                                    });

                return;
            }
            await next();
            //To do : after the action executes  
        }
    }
}
