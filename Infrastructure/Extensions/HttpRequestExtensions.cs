using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Extensions
{
    public static partial class Extensions
    {
        /// <summary>
        /// 获取<see cref="HttpRequest"/>的请求数据
        /// </summary>
        /// <param name="request">请求信息</param>
        /// <param name="key">要获取数据的键名</param>
        /// <returns></returns>
        public static string GetQueryString(this HttpRequest request, string key)
        {
            //Check.NotNull(request, nameof(request));
            string requestParam = "";
            if (request.Query.ContainsKey(key))
            {
                requestParam = request.Query[key];
            }
            if (request.HasFormContentType && request.Form.ContainsKey(key))
            {
                requestParam = request.Form[key];
            }
            if (!requestParam.IsNullOrEmpty())
                requestParam = System.Web.HttpUtility.UrlDecode(requestParam);
            return requestParam;
        }
    }
}
