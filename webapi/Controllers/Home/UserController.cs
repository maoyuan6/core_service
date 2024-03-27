using Infrastructure.Model.RequestModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Service.Model.User;
using Webapi.Controllers.Base;

namespace Webapi.Controllers.Home
{
    /// <summary>
    /// 用户管理
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Basic")]
    public class UserController : BaseApiController
    {
        private readonly IUserService _userService;
        public UserController(  IUserService userService)
        { 
            _userService = userService;
        }
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="arg"></param> 
        /// <returns></returns> 
        [HttpPost]
        public async Task<IActionResult> GetUserListAsync(QueryPageListModel arg)
        {
            return await PackageResultAsync(await _userService.GetUserListAsync(arg));
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="arg"></param> 
        /// <returns></returns> 
        [HttpPost]
        public async Task<IActionResult> AddUserAsync(UserModel arg)
        {
            return await PackageResultAsync(await _userService.AddUserAsync(arg));
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="id"></param> 
        /// <returns></returns> 
        [HttpPost]
        public async Task<IActionResult> GetUserInfoAsync(string code)
        {
            return await PackageResultAsync(await _userService.GetUserInfoAsync(code));
        }
    }
}
