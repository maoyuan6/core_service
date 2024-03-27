using Infrastructure.DependencyInjection;
using Infrastructure.Extensions;
using Infrastructure.Filters;
using Infrastructure.Helpers;
using Infrastructure.JWT.Auth;
using Infrastructure.JWT.Check;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Connections;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Nacos.AspNetCore.V2;
using RabbitMQ.Client;
using Respository.Global;
using Respository.JWT;
using Service.DependencyInjection;
using Service.Service.MQConsumer;
using StackExchange.Redis;
using System.Threading.Channels;
using Mapster;
using Nacos.Microsoft.Extensions.Configuration;
using Webapi.Filters;
using Nacos.OpenApi;
using Nacos.V2.DependencyInjection;
using Nacos.V2;
using System;
using Microsoft.Extensions.Configuration.Memory;

namespace Webapi
{
    public static class Startup
    {
        public static IFreeSql Fsql { get; private set; }

        public static void AddCoreApp(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseCustomSwagger();
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
        }

        public static async Task AddCoreService(this IServiceCollection services, WebApplicationBuilder builder)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            //注册IHttpContextAccessor
            services.AddHttpContextAccessor();
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            Console.WriteLine($"Program环境变量-------{environment}");
            AppSettingHelper.ConfigInitialization(); 

            #region Redis 
            ConfigurationOptions redisConfigOption = new ConfigurationOptions();
            redisConfigOption.EndPoints.Add(GlobalContext.SystemConfig.RedisConnection.Configuration + ":" +
                                            GlobalContext.SystemConfig.RedisConnection.Port);
            redisConfigOption.AllowAdmin = true;
            redisConfigOption.Password = GlobalContext.SystemConfig.RedisConnection.Password;
            redisConfigOption.ClientName = GlobalContext.SystemConfig.RedisConnection.InstanceName;
            var conn = ConnectionMultiplexer.Connect(redisConfigOption);
            services.AddSingleton<ConnectionMultiplexer>(conn);
            Console.WriteLine(
                $"Redis环境准备成功,当前Redis服务器为{GlobalContext.SystemConfig.RedisConnection.Configuration}，实例名为{GlobalContext.SystemConfig.RedisConnection.InstanceName}");

            #endregion

            #region FreeSql

            var freeSqlBuild = new FreeSql.FreeSqlBuilder()
                .UseConnectionString(FreeSql.DataType.MySql, GlobalContext.SystemConfig.DbConnectionStrings.MySql);
            if (builder.Environment.IsDevelopment())
            {
                freeSqlBuild =
                    freeSqlBuild.UseMonitorCommand(cmd =>
                        Console.WriteLine(cmd.CommandText)); //.UseAutoSyncStructure(true)
            }

            Fsql = freeSqlBuild.UseNoneCommandParameter(true)
                .Build();

            Fsql.Aop.ConfigEntityProperty += (s, e) =>
            {
                if (e.Property.PropertyType.IsEnum)
                {
                    e.ModifyResult.MapType = typeof(int);
                }

                if (e.Property.PropertyType == typeof(DateTime))
                {
                    e.ModifyResult.DbType = "datetime(0)";
                }
            };
            Console.WriteLine(
                $"MySql环境准备成功，当前MySql服务器为{GlobalContext.SystemConfig.DbConnectionStrings.MySql.Split(new char[] { '=', ';' })[1]}");

            #endregion

            #region RabbitMQ

            var connectionFactory = new ConnectionFactory()
            {
                HostName = GlobalContext.SystemConfig.RabbitMQConnection.HostName,
                UserName = GlobalContext.SystemConfig.RabbitMQConnection.UserName,
                Password = GlobalContext.SystemConfig.RabbitMQConnection.Password,
                Port = GlobalContext.SystemConfig.RabbitMQConnection.Port
            };

            services.AddSingleton<ConnectionFactory>(connectionFactory);
            var connection = connectionFactory.CreateConnection();
            services.AddSingleton<IConnection>(connection);
            var channel = connection.CreateModel();
            //注册交换机
            channel.AddMQExchange();
            //注册队列
            channel.AddMQQueue();
            //队列绑定到交换机
            channel.BindToExchange();
            services.AddSingleton<IModel>(channel);
            services.AddHostedService<OrderConsumerService>();
            Console.WriteLine($"RabbitMQ环境准备成功,当前RabbitMQ服务器为{GlobalContext.SystemConfig.RabbitMQConnection.HostName}");

            #endregion

            //添加基础设施服务
            services.AddInfrastructureInjection();
            //添加服务
            services.AddServiceInjection();
            //认证
            services.AddAuthentication("Bearer")
                .AddJwtBearer(options => // 添加 JwtBearer 认证方案
                {
                    options.TokenValidationParameters =
                        AuthenticationService.GetTokenValidationParameters(GlobalContext.SystemConfig.JwtSetting);
                });
            // 授权 
            services.AddAuthorization(options =>
            {
                options.AddPolicy("CoreAuth", policy => policy.Requirements.Add(new PermissionRequirement()));
            }).AddScoped<IAuthorizationHandler, PermissionHandler>();

            services.AddControllers(options =>
            {
                //添加自定义模型认证
                //options.Filters.Add<GlobalExceptionFilter>();
                //添加全局异常过滤 只支持过滤控制器产生的异常 定时任务产生的异常需要自己处理
                options.Filters.Add(typeof(GlobalExceptionFilter));
                //权限校验过滤器
                options.Filters.Add(new TokenFilter());
            });

            services.AddCustomSwagger();

            services.AddSingleton<ITokenHelper, TokenHelper>();
            services.AddSingleton<IFreeSql>(Fsql);

            #region Nacos



            #endregion
        }
    }
}
