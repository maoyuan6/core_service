using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Webapi.Controllers.Base
{
    [Authorize(policy: "CoreAuth")]
    public class BaseApiController : Controller
    {
        protected async Task<IActionResult> PackageResultAsync<TResponse>(TResponse? response = default)
        {
            return await Task.FromResult(Json(new ResponseResult<TResponse?>
            {
                Success = true,
                Code = 200,
                Message = "成功",
                Data = response 
            }));
        }
    }
}
