using Respository.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Respository.JWT;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.DataProtection.KeyManagement;

namespace Infrastructure.JWT.Check
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthenticationService()
        {

        }

        public AuthenticationService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetCurrentToken(string tokenName = "")
        {
            string token = "";
            var httpRequest = _httpContextAccessor.HttpContext.Request;

            //head获取
            if (token.IsNullOrEmpty())
                token = httpRequest.Headers[tokenName].FirstOrDefault() ?? "";

            //cookies获取 
            if (token.IsNullOrEmpty())
                httpRequest.Cookies.TryGetValue(tokenName, out token);

            //url传参
            if (token.IsNullOrEmpty())
                token = httpRequest.GetQueryString(tokenName);

            return token;
        }

        public string CreateToken<T>(T user)
        {
            //携带的负载部分，类似一个键值对
            var claims = new List<Claim>();
            //这里我们用反射把model数据提供给它
            foreach (var item in user.GetType().GetProperties())
            {
                object? obj = item.GetValue(user);
                string? value = "";
                if (obj != null)
                    value = obj.ToString();

                if (value != null) claims.Add(new Claim(item.Name, value));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(GlobalContext.SystemConfig.JwtSetting.SecurityKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //令牌
            var expires = DateTime.Now.AddMinutes(GlobalContext.SystemConfig.JwtSetting.AccessExpireMins);
            var token = new JwtSecurityToken(
                issuer: GlobalContext.SystemConfig.JwtSetting.Issuer,
                audience: GlobalContext.SystemConfig.JwtSetting.Audience,
                claims: claims,
                notBefore: DateTime.Now,
                expires: expires,
                signingCredentials: credentials
            );
            string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return jwtToken;
        }

        public bool IsAuthenticated(out bool expiredToken)
        {
            string token = GetCurrentToken();
            var isAuthenticated = CheckToken(token, GlobalContext.SystemConfig.JwtSetting, out expiredToken);
            return isAuthenticated;
        }

        public bool IsAuthenticated(out bool expiredToken, string token)
        {
            var isAuthenticated = CheckToken(token, GlobalContext.SystemConfig.JwtSetting, out expiredToken);
            return isAuthenticated;
        }

        /// <summary>
        /// 校验Token是否有效
        /// </summary>
        /// <param name="token"></param>
        /// <param name="jwtOptions"></param>
        /// <param name="expiredToken">是否是过期引起的验证失败</param>
        /// <returns></returns>
        public static bool CheckToken(string token, JwtSetting jwtSetting, out bool expiredToken)
        {
            var principal = GetPrincipal(token, jwtSetting, out expiredToken);
            if (principal is null)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 从Token中获取用户身份
        /// </summary>
        /// <param name="token"></param>
        /// <param name="jwtSetting"></param>
        /// <param name="expiredToken">是否是过期引起的获取不到用户身份</param>
        /// <returns></returns>
        public static ClaimsPrincipal? GetPrincipal(string token, JwtSetting jwtSetting, out bool expiredToken)
        {
            expiredToken = false;
            try
            {
                var handler = new JwtSecurityTokenHandler();
                TokenValidationParameters tokenValidationParameters = GetTokenValidationParameters(jwtSetting);
                var cp = handler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);
                expiredToken = true;
                return cp;
            }
            catch (SecurityTokenExpiredException)
            {
                expiredToken = true;
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static TokenValidationParameters GetTokenValidationParameters(JwtSetting jwtOptions)
        {
            return new TokenValidationParameters
            {
                ValidIssuer = jwtOptions.Issuer,
                ValidAudience = jwtOptions.Audience,
                ValidateIssuer = jwtOptions.ValidateIssuer,
                ValidateAudience = jwtOptions.ValidateAudience,
                ValidateLifetime = jwtOptions.ValidateLifetime,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecurityKey)),
                //缓冲过期时间，总的有效时间等于这个时间加上jwt的过期时间
                ClockSkew = TimeSpan.Zero
            };
        }
    }
}
