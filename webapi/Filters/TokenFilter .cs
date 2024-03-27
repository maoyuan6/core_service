using Infrastructure.JWT.Check;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Respository.Global;

namespace Webapi.Filters
{
    public class TokenFilter : IAsyncActionFilter, IFilterMetadata
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            bool isAnonymous = false;
            //忽略认证的接口
            if (context.ActionDescriptor is ControllerActionDescriptor descriptor)
            {
                if (descriptor != null)
                {
                    var actionAttributes = descriptor.MethodInfo.GetCustomAttributes(inherit: true);
                    isAnonymous = actionAttributes.Any(a => a is AllowAnonymousAttribute);
                }
            }

            if (!isAnonymous)
            {
                var request = context.HttpContext.Request;
                var token = request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                if (string.IsNullOrEmpty(token))
                {
                    context.Result = new StatusCodeResult(401); // 未授权状态码
                    return;
                }

                if (!IsValidToken(token))
                {
                    context.Result = new StatusCodeResult(401); // 未授权状态码
                    return;
                }
            }
            await next();
        }
        private bool IsValidToken(string token)
        {
            IAuthenticationService? authenticationService = new Infrastructure.JWT.Check.AuthenticationService();
            authenticationService.IsAuthenticated(out var isValid, token);
            return isValid;
        }
    }
}
