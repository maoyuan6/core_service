using Infrastructure.Model.RequestModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Webapi.Controllers.Base;

namespace Webapi.Controllers.Home
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LogController : BaseApiController
    {
        //定义日志服务
        private readonly ILogService _logService;

        public LogController(ILogService logService)
        {
            _logService = logService;
        }
         
        /// <summary>
        /// 新增一个查看日志的接口
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetLogList(QueryPageListModel arg)
        {
            return await PackageResultAsync(_logService.GetLogList(arg)); 
        }
    }
}
