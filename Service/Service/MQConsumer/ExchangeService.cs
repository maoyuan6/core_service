using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service.MQConsumer
{
    
    public static class ExchangeService
    {
        /// <summary>
        ///  声明交换机
        /// </summary>
        /// <param name="channel"></param>
        public static void AddMQExchange(this IModel channel)
        {
            //添加订单交换机
            channel.ExchangeDeclare("order_exchange", "direct");
        } 
    }
}
