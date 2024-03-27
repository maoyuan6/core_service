using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Model.AppSetting.AppSettingSubModel
{
    public class RedisConnection
    {
        public string Configuration { get; set; }
        public string InstanceName { get; set; }
        public string Password { get; set; } 
        public int Port { get; set; }
    }
}
