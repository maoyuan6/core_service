using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreeSql;
using Infrastructure.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Logging;
using Newtonsoft.Json;
using Repository.Entities.Log;
using Webapi.Controllers.Base;

namespace Infrastructure.Filters
{
    public class GlobalExceptionFilter : HttpGlobalExceptionLogFilter
    { 
        public GlobalExceptionFilter(ILogger<HttpGlobalExceptionLogFilter> logger, IFreeSql freeSql) : base(logger, freeSql)
        {
        }
        public Task OnExceptionAsync(ExceptionContext context)
        {
            OnException(context);
            return Task.CompletedTask;
        }

        public void OnException(ExceptionContext context)
        {
            base.OnException(context);
            if (context.Exception is BusinessException customException)
            {
                var json = new JsonResult(
                    new
                    {
                        Success = false,
                        Code = customException.HResult,
                        customException.Message
                    });
                context.Result = json;
            }
            else
            {
                var json = new JsonResult(
                    new
                    {
                        Success = false,
                        Code = 500,
                        context.Exception.Message
                    });
                context.Result = json;
            }
            context.ExceptionHandled = true;
        }
    }

    public class HttpGlobalExceptionLogFilter : IExceptionFilter
    {
        private readonly ILogger<HttpGlobalExceptionLogFilter> logger;

        private readonly IFreeSql _freeSql;
        private readonly IBaseRepository<SystemLog> _logRepository;
        public HttpGlobalExceptionLogFilter(ILogger<HttpGlobalExceptionLogFilter> logger, IFreeSql freeSql)
        {
            this.logger = logger;
            _freeSql = freeSql;
            _logRepository = _freeSql.GetRepository<SystemLog>();
        }
        public virtual void OnException(ExceptionContext context)
        {  
            if (context.Exception is not BusinessException)
            {
                //不是业务异常就记日志
                _ = _logRepository.InsertAsync(new SystemLog()
                {
                    Type = "Error",
                    LogInfo = context.Exception.Message + context.Exception.StackTrace,
                    LogTime = DateTime.Now
                });
            } 
            var json = new ResponseResult<string>()
            {
                Code = 500,
                Message = context.Exception.Message
            };
            context.Result = new JsonResult(json);
            context.ExceptionHandled = true;
        }
    }
}
