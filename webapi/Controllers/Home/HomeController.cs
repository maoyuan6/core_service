using Infrastructure.Helpers;
using Infrastructure.JWT.Check;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Entities;
using Service.Contracts;
using Service.Model.User;
using Webapi.Controllers.Base;

namespace Webapi.Controllers.Home
{
    /// <summary>
    /// 首页
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Basic")]
    public class HomeController : BaseApiController
    {
        private readonly IAuthenticationService _authorizationService;
        private readonly IUserService _userService;
        public HomeController(IAuthenticationService authorizationService, IUserService userService)
        {
            _authorizationService = authorizationService;
            _userService = userService;
        }
        /// <summary>
        /// 登录接口
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> LoginAsync(string name, string password)
        {
            return await PackageResultAsync(await _userService.CheckLoginAsync(name, password));
        }
       
        /// <summary>
        /// 首页
        /// </summary> 
        /// <returns></returns>
        [HttpPost]
        public async Task<string> Index()
        {
            return "这里将会显示用户主页，或者系统首页";
        }
        /// <summary>
        /// 实体同步到数据库
        /// </summary>
        /// <param name="fsql"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public JsonResult SynchronizeEntitiesAsync([FromServices] IFreeSql fsql)
        {
            fsql.CodeFirst.SyncStructure(AsyncEntity.GetTypesByTableAttribute());
            return Json("同步成功");
        }
        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        [HttpPost]

        public async Task<IActionResult> SendEmailAsync(SmsModel arg)
        {
            SmsHelper.SendSms(arg);
            return await PackageResultAsync(true);
        }
    }
}
