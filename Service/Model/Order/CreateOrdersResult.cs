using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Model.Order
{
    public class CreateOrdersResult
    {
        /// <summary>
        /// 订单状态
        /// </summary>
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
