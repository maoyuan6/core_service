using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Respository.Global;

namespace Respository.JWT
{
    public class TokenHelper : ITokenHelper
    { 
        public string GetToken<T>(T user)
        {
            //携带的负载部分，类似一个键值对
            var claims = new List<Claim>();
            //这里我们用反射把model数据提供给它
            foreach (var item in user.GetType().GetProperties())
            {
                object obj = item.GetValue(user);
                string value = "";
                if (obj != null)
                    value = obj.ToString();

                claims.Add(new Claim(item.Name, value));
            }

            //创建令牌
            var token = new JwtSecurityToken(
                issuer: GlobalContext.SystemConfig.JwtSetting.Issuer,
                audience: GlobalContext.SystemConfig.JwtSetting.Audience,
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddSeconds(GlobalContext.SystemConfig.JwtSetting.ExpireSeconds)
            );

            string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            return jwtToken;
        }
    }
}
