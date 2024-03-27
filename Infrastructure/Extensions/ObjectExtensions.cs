using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Infrastructure.Extensions
{
    public static partial class ObjectExtensions
    {
        /// <summary>
        /// 判断是否为Null或者空
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this object? obj)
        {
            if (obj == null || obj == DBNull.Value)
                return true;
            else
            {
                if (obj.GetType().Name == "String")
                    return string.IsNullOrEmpty(obj.ToString());
                else
                    return false;
            }
        }
        public static bool IsNotNullOrEmpty(this object? obj)
        {
            return !obj.IsNullOrEmpty();
        }
        /// <summary>
        /// 对象转json
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJson(this object? obj)
        {
            if (obj.IsNullOrEmpty())
            {
                return string.Empty;
            }
            return JsonConvert.SerializeObject(obj, Formatting.Indented);
        }
    }
}
