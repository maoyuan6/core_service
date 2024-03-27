using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Base;

namespace Infrastructure.Cache
{
    public interface ICacheService : IBaseService
    {
        string GetCacheByKey(string key);

        bool SetCache(object arg, string key);
    }
}
