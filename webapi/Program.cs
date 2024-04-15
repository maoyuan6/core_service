using Respository.Global;
using System;
using Autofac.Extensions.DependencyInjection;
using Webapi;
using Autofac.Core;
using Nacos.AspNetCore.V2;
using Service.Service.MQConsumer;
using Nacos.V2.Naming.Dtos;
using Nacos.Microsoft.Extensions.Configuration;
using Nacos.V2.DependencyInjection;
using Nacos.V2;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
// 设置 Nacos 通用配置
builder.Configuration.AddJsonFile(builder.Environment.IsDevelopment()
    ? $"appsettings.{builder.Environment.EnvironmentName}.json"
    : "appsettings.json", optional: true, reloadOnChange: true);
//builder.Configuration.AddJsonFile($"nacosconfig.json");
try
{
    // 注册 Nacos 配置源.暂时不处理nacos 的日志
    builder.Configuration.AddNacosV2Configuration(builder.Configuration.GetSection("Nacos"), logAction: x => { });
}
catch (Exception e)
{
    // 兜底 nacos 的配置
    builder.Configuration.AddJsonFile($"nacosconfig.json");
}

GlobalContext.Configuration = builder.Configuration;
await builder.Services.AddCoreService(builder);
var app = builder.Build();
GlobalContext.ServiceProvider = app.Services;
app.AddCoreApp();
// Configure the HTTP request pipeline.

app.Run();

