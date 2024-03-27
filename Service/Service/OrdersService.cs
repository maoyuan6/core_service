using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Model.Order;
using Infrastructure.Cache;
using Infrastructure.Helpers;
using Mapster;
using Repository.Entities.Product.Order;
using Repository.Entities.Product.Commodity;
using Repository.Entities;
using System.Threading;
using FreeSql;
using Newtonsoft.Json;
using RabbitMQ.Client;
using StackExchange.Redis;
using Microsoft.IdentityModel.Logging;
using Repository.Entities.Log;

namespace Service.Service
{
    public class OrdersService : IOrdersService
    {
        private readonly IFreeSql _freeSql;
        private readonly ICacheService _cacheService;
        private readonly ConnectionMultiplexer _connectionMultiplexer;
        private readonly IModel _channel;
        //创建一个redis锁
        private readonly RedisDistributedLock _lockHelper;
        private readonly string _lockKey = "inventory_lock:OrderSubmit"; // 锁的唯一标识

        private readonly IBaseRepository<SystemLog> _logRepository;
        public OrdersService(IFreeSql freeSql, ICacheService cacheService, ConnectionMultiplexer connectionMultiplexer, IModel channel)
        {
            _freeSql = freeSql;
            _cacheService = cacheService;
            _connectionMultiplexer = connectionMultiplexer;
            _lockHelper = new RedisDistributedLock(_connectionMultiplexer);
            _channel = channel;
            _logRepository = _freeSql.GetRepository<SystemLog>();
        }


        public async Task<bool> OrderJoinMQAsync(CreateOrderModel arg)
        {
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(arg)); 
            _channel.BasicPublish(exchange: "order_exchange", routingKey: "place_order", basicProperties: null, body: body);  
            return true;
        }

        public async Task<CreateOrdersResult> OrderSubmitAsync(CreateOrderModel arg)
        {
            var orderStatus = OrderStatus.Fail;
            var message = string.Empty;
            var lockValue = $"lock_{CodeHelper.CreateGuid()}";
            const int maxRetries = 5;
            TimeSpan retryDelay = TimeSpan.FromMilliseconds(100);
            if (await _lockHelper.AcquireLockAsync(_lockKey, lockValue, TimeSpan.FromSeconds(10)))
            {
                try
                {
                    var oldInventory = await _freeSql.Select<Commodity>().Where(a => a.Code == arg.CommodityCode).FirstAsync();
                    if (oldInventory.Inventory >= arg.OrderQuantity && oldInventory.Inventory > 0)
                    {
                        var repo = _freeSql.GetRepository<Commodity>();
                        oldInventory.Inventory = oldInventory.Inventory - arg.OrderQuantity;
                        await repo.UpdateAsync(oldInventory);
                        orderStatus = OrderStatus.Success;
                        message = "下单成功";
                    }
                    else
                    {
                        message = "下单失败，库存不足";
                    }
                }
                catch (Exception e)
                {
                    message = "下单失败，" + e.Message;
                }
                finally
                {
                    // 无论成功还是失败，最后都要释放锁
                    await _lockHelper.ReleaseLockAsync(_lockKey, lockValue);
                }
            }

            var ordersRepo = _freeSql.GetRepository<Orders>();
            _ = ordersRepo.InsertAsync(new Orders()
            {
                Code = CodeHelper.CreateOrdersCode(),
                UserCode = arg.UserCode,
                CommodityCode = arg.CommodityCode,
                OrderTime = DateTime.Now,
                OrderAmount = arg.OrderAmount,
                OrderQuantity = arg.OrderQuantity,
                OrderStatus = orderStatus
            });
            return new CreateOrdersResult()
            {
                Status = true,
                Message = message
            };

        }
    }
}
