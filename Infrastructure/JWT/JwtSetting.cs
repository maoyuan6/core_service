using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Respository.JWT
{
    public class JwtSetting
    {
        /// <summary>
        /// 
        /// </summary>
        public string SecurityKey { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Issuer { get; set; }
        public bool ValidateIssuer { get; set; }
        public bool ValidateAudience { get; set; }
        public bool ValidateLifetime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Audience { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ExpireSeconds { get; set; }
        public string Name { get; set; }
        public int AccessExpireMins { get; set; }
    }
}
