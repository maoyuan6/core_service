using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection; 
using Respository.Global;

namespace Infrastructure.Helpers
{
    public class LogHelper
    {
        private readonly IFreeSql _freeSql = GlobalContext.ServiceProvider.GetService<IFreeSql>();

        public static async Task WriteErrorLogAsync()
        {
             
        } 

    }
}
