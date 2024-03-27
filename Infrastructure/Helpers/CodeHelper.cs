using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snowflake.Core;

namespace Infrastructure.Helpers
{
    public class CodeHelper
    {
        /// <summary>
        /// 创建订单编号
        /// </summary>
        /// <returns></returns>
        public static string CreateOrdersCode()
        {
            string code = "ordercode_"+ new IdWorker(1, 1).NextId(); 
            return code;
        }
        /// <summary>
        /// 创建GUID
        /// </summary>
        /// <returns></returns>
        public static string CreateGuid()
        {
            string code = Guid.NewGuid().ToString(); 
            return code;
        }

    }
}
