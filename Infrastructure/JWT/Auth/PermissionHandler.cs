using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.JWT.Auth
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {

        /// <summary>
        /// 授权
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requirement"></param>
        /// <returns></returns>
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
            PermissionRequirement requirement)
        {

            if (context.Resource is AuthorizationFilterContext filterContext && filterContext.ActionDescriptor is ControllerActionDescriptor descriptor)
            {
                if (descriptor != null)
                {
                    var actionAttributes = descriptor.MethodInfo.GetCustomAttributes(inherit: true);
                    bool isAnonymous = actionAttributes.Any(a => a is AllowAnonymousAttribute);
                    //运行匿名的方法,不校验
                    if (isAnonymous)
                    {
                        context.Succeed(requirement);
                    }
                }
            }

            var isResAuth = true;
            if (isResAuth)
            {
                context.Succeed(requirement);
            }
            else
            { 

            }
        }
    }
}
