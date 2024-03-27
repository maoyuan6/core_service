using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using Respository.Global;

namespace Infrastructure.Helpers
{
    public class RabbitMQHelper
    {
        /// <summary>
        /// 获取mq连接
        /// </summary>
        /// <returns></returns>
        public static IConnection GetConnectionFactory()
        {
            var connectionFactory = GlobalContext.ServiceProvider.GetService<ConnectionFactory>();
            return connectionFactory.CreateConnection();
        } 
    }
}
