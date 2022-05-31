using HPCL.Common.Models.CommonEntity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
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
            char flag = 'N';
            var descriptor = context.ActionDescriptor as ControllerActionDescriptor;
            var actionName = descriptor.ActionName;
            var conttollerName = descriptor.ControllerName;
            var typeOfAction = descriptor.MethodInfo.ReturnType.FullName;

            IEnumerable<MenuDetailModel> menu = new List<MenuDetailModel>();

            if (typeOfAction.Contains("JsonResult"))
            {
                flag = 'Y';
            }

            for (var i = 0; i < SessionMenuModel.menuList.Count; i++)
            {
                if (SessionMenuModel.menuList[i].Controller == conttollerName && SessionMenuModel.menuList[i].UserID == _httpContextAccessor.HttpContext.Session.GetString("UserId"))
                {
                    if (SessionMenuModel.menuList[i].Action == actionName && SessionMenuModel.menuList[i].UserID == _httpContextAccessor.HttpContext.Session.GetString("UserId"))
                    {
                        flag = 'Y';
                        SessionMenuModel.menuList[0].CalledMenuId = SessionMenuModel.menuList[i].MenuId;
                        menu = SessionMenuModel.menuList.Where(x => x.UserID == _httpContextAccessor.HttpContext.Session.GetString("UserId") && x.MenuId == SessionMenuModel.menuList[i].ParentMenuId);

                        _httpContextAccessor.HttpContext.Session.SetString("BreadCrumbsController", menu.FirstOrDefault().Controller);
                        _httpContextAccessor.HttpContext.Session.SetString("BreadCrumbsAction", menu.FirstOrDefault().Action);
                        _httpContextAccessor.HttpContext.Session.SetString("CurrentAction", SessionMenuModel.menuList[i].MenuName);

                        _httpContextAccessor.HttpContext.Session.SetString("CalledMenuAllowedAction", SessionMenuModel.menuList[i].AllowedAction.ToString());
                    }
                }
            }



            //To do : before the action executes
            if (_httpContextAccessor.HttpContext.Session.GetString("UserId") == null || flag == 'Y')
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary
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

        //public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        //{
        //    //To do : before the action executes
        //    if (_httpContextAccessor.HttpContext.Session.GetString("UserId") == null)
        //    {
        //        context.Result =new RedirectToRouteResult(new RouteValueDictionary
        //                            {
        //                                { "action", "Index" },
        //                                { "controller", "Home" },
        //                                { "returnUrl", "~/Home/Index"}
        //                            });

        //        return;
        //    }
        //    await next();
        //    //To do : after the action executes  
        //}
    }
}
