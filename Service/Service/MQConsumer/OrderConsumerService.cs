using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Extensions;
using Respository.Global;
using Infrastructure.Helpers;
using Service.Contracts;
using Service.Model.Order;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Repository.Entities.Log;
using FreeSql;
using System.Threading.Channels;

namespace Service.Service.MQConsumer
{
    public class OrderConsumerService : IHostedService, IDisposable
    {
        private readonly ILogger<OrderConsumerService> _logger;
        private readonly IModel _channel;
        private readonly EventingBasicConsumer _consumer;
        private readonly IOrdersService _orderService = GlobalContext.ServiceProvider.GetService<IOrdersService>();
        public OrderConsumerService(ILogger<OrderConsumerService> logger, IModel channel)
        {
            _logger = logger;
            _channel = channel;
            _consumer = new EventingBasicConsumer(_channel);
        }

        public void Dispose()
        {
            StopAsync(CancellationToken.None).Wait();
        }
        
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                // 扣库存,操作数据库
                var result = await _orderService.OrderSubmitAsync(message.ToObject<CreateOrderModel>()); 
                // 确认消息已经被成功处理
                _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            };
            // 开始接收消息，参数为队列名、是否自动应答、消费者实例
            _channel.BasicConsume(queue: "order_queue", autoAck: false, consumer: _consumer); 
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
