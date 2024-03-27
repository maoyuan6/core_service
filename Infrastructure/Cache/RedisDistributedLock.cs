using Infrastructure.Model.AppSetting.AppSettingSubModel;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Cache
{
    /// <summary>
    /// redis 分布式锁
    /// </summary>
    public class RedisDistributedLock
    {
        private readonly ConnectionMultiplexer _redis;
        private readonly IDatabase _database;

        public RedisDistributedLock(ConnectionMultiplexer redis)
        {
            _redis = redis;
            _database = redis.GetDatabase();
        }

        public async Task<bool> AcquireLockAsync(string lockKey, string value, TimeSpan lockTimeout)
        {
            var acquired = await _database.StringSetAsync(lockKey, value, lockTimeout, When.NotExists);
            if (!acquired)
            {
                // 循环重试直到成功或超时
                var startTime = DateTime.UtcNow;
                while (true)
                {
                    Thread.Sleep(10); // 等待一段时间后再重试

                    if (await _database.StringSetAsync(lockKey, value, lockTimeout, When.NotExists))
                    {
                        acquired = true; // 成功获取到锁
                        break;
                    }
                }
            }
            return acquired;
        }

        public async Task ReleaseLockAsync(string lockKey, string value)
        {
            //验证锁的拥有者才能释放锁
            string currentValue = await _database.StringGetAsync(lockKey);
            if (currentValue == value)
            {
                await _database.KeyDeleteAsync(lockKey);
            }
        }
    }
}
