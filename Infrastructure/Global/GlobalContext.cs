using Respository.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Model.AppSetting;
using Microsoft.Extensions.Configuration;

namespace Respository.Global
{
    /// <summary>
    /// 全局变量
    /// </summary>
    public class GlobalContext
    {
        public static IServiceProvider ServiceProvider { get; set; }
        public static IConfiguration Configuration { get; set; }
        public static AppSetting SystemConfig { get; set; } = new AppSetting(); 
    }
}
