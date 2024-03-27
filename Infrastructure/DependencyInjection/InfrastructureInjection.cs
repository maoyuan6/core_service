using Infrastructure.Base;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DependencyInjection
{
    public static class InfrastructureInjection
    {
        /// <summary>
        /// 注入基础设施服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddInfrastructureInjection(this IServiceCollection services)
        {
            var asse = typeof(InfrastructureInjection).Assembly;
            var types = asse.GetTypes();
            foreach (var iRepo in types.Where(a => a.IsInterface == true && typeof(IBaseService).IsAssignableFrom(a)))
            {
                var repo = types.FirstOrDefault(a => a.IsAbstract == false && iRepo.IsAssignableFrom(a));
                if (repo != null)
                {
                    services.AddScoped(iRepo, repo);
                }
            }

            return services;
        }
    }
}
