using Respository.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Model.AppSetting.AppSettingSubModel;

namespace Infrastructure.Model.AppSetting
{
    public class AppSetting
    {
        /// <summary>
        /// 
        /// </summary>
        public string AllowedHosts { get; set; }
        /// <summary>
        /// jwt配置
        /// </summary>
        public JwtSetting JwtSetting { get; set; }
        /// <summary>
        /// jwt配置
        /// </summary>
        public AlibabaConfigModel AlibabaConfig  { get; set; }
        /// <summary>
        /// 数据库链接
        /// </summary>
        public DBConnectionStrings DbConnectionStrings { get; set; }
        /// <summary>
        /// 消息队列链接
        /// </summary>
        public RabbitMQConnection RabbitMQConnection { get; set; }
        /// <summary>
        /// redis链接
        /// </summary>
        public RedisConnection RedisConnection { get; set; }
        /// <summary>
        /// nacos配置
        /// </summary>
        public NacosConnection NacosConnection { get; set; }

    }
}
