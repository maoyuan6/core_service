using Infrastructure.Base;
using Microsoft.Extensions.DependencyInjection;

namespace Service.DependencyInjection
{
    public static class ServiceInjection
    {
        /// <summary>
        /// 注入服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddServiceInjection(this IServiceCollection services)
        {
            var asse = typeof(ServiceInjection).Assembly;
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
