using Infrastructure.Model.AppSetting;
using Microsoft.Extensions.Configuration;
using Respository.Global;
using Respository.JWT;

namespace Infrastructure.Helpers
{
    public class AppSettingHelper
    {
        public static void ConfigInitialization()
        {
            GlobalContext.SystemConfig ??= new AppSetting(); 
            GlobalContext.Configuration.Bind("CoreSetting", GlobalContext.SystemConfig); 
        }
    }
}
