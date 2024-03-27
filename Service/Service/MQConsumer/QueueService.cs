using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service.MQConsumer
{
    public static class QueueService
    {
        /// <summary>
        /// 声明队列
        /// </summary>
        /// <param name="channel"></param>
        public static void AddMQQueue(this IModel channel)
        {
            // 声明订单队列
            channel.QueueDeclare(queue: "order_queue", durable: true, exclusive: false, autoDelete: false,
                arguments: null);
        }
        /// <summary>
        /// 队列绑定到交换机
        /// </summary>
        /// <param name="channel"></param>
        public static void BindToExchange(this IModel channel)
        {
            channel.QueueBind("order_queue", "order_exchange", "place_order");
        }
    }
}
