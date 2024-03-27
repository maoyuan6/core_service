using Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.JWT.Check
{
    public interface IAuthenticationService : IBaseService
    {
        /// <summary>
        /// 获取当前请求携带的token（从cookie、heads、url）
        /// </summary>
        /// <param name="tokenName">空值，默认配置项中取</param>
        /// <returns></returns>
        string GetCurrentToken(string tokenName = "");
        /// <summary>
        /// 创建Token
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="user"></param>
        /// <returns></returns>
        string CreateToken<T>(T user);
        /// <summary>
        /// 是否认证
        /// </summary>
        /// <param name="expiredToken">是否过期</param>
        /// <returns></returns>
        bool IsAuthenticated(out bool expiredToken);
        /// <summary>
        /// 是否认证
        /// </summary>
        /// <param name="expiredToken">是否过期</param>
        /// <param name="token"></param>
        /// <returns></returns>
        bool IsAuthenticated(out bool expiredToken, string token);
    }
}
