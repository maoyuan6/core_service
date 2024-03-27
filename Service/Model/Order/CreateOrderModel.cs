using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Entities.Product.Order;
using Service.Model.User;

namespace Service.Model.Order
{
    public class CreateOrderModel
    {
        /// <summary>
        /// 商品编号
        /// </summary>
        public string CommodityCode { get; set; }
        /// <summary>
        /// 用户编号
        /// </summary>
        public string UserCode { get; set; }
        /// <summary>
        /// 下单数量
        /// </summary>
        public int OrderQuantity { get; set; }
        /// <summary>
        /// 下单金额
        /// </summary>
        public decimal OrderAmount { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public OrderStatus OrderStatus { get; set; }
    }
}
