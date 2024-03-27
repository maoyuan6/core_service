using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Respository.Global;

namespace Infrastructure.Extensions
{
    public static partial class Extensions
    {
        public static IServiceCollection AddFreeSql(this IServiceCollection services,IFreeSql freeSql, bool isDevelopment)
        {
            var freeSqlBuild = new FreeSql.FreeSqlBuilder()
                .UseConnectionString(FreeSql.DataType.MySql, GlobalContext.SystemConfig.DbConnectionStrings.MySql);
            if (isDevelopment)
            {
                freeSqlBuild = freeSqlBuild.UseMonitorCommand(cmd => Console.WriteLine(cmd.CommandText));//.UseAutoSyncStructure(true)
            }
            freeSql = freeSqlBuild.UseNoneCommandParameter(true)
                .Build();

            freeSql.Aop.ConfigEntityProperty += (s, e) =>
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
            return services;
        } 
    }
}
