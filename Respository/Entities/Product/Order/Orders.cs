using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities.Product.Order
{
    [Index("uk_id", "id", true)]
    [Table(Name = "orders")]
    public class Orders : IEntityBase
    {

        /// <summary>
        /// ID
        /// </summary>
        [Column(IsIdentity = true, Name = "id")]
        public int Id { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        [Column(IsNullable = false, Name = "code", StringLength = 128)]
        public string Code { get; set; }
        /// <summary>
        /// 用户编号
        /// </summary>
        [Column(IsNullable = false, Name = "user_id", StringLength = 128)]
        public string UserCode { get; set; }
        /// <summary>
        /// 商品编号
        /// </summary>
        [Column(IsNullable = false, Name = "commodity_id", StringLength = 128)]
        public string CommodityCode { get; set; }
        /// <summary>
        /// 下单时间
        /// </summary>
        [Column(IsNullable = false, Name = "order_time")]
        public DateTime OrderTime { get; set; }
        /// <summary>
        /// 下单金额
        /// </summary>
        [Column(IsNullable = false, Name = "order_amount")]
        public decimal OrderAmount { get; set; }
        /// <summary>
        /// 下单数量
        /// </summary>
        [Column(IsNullable = false, Name = "order_quantity")]
        public int OrderQuantity { get; set; }
        /// <summary>
        /// 下单状态
        /// </summary>
        [Column(IsNullable = false, Name = "order_status")]
        public OrderStatus OrderStatus { get; set; }

        /// <summary>
        /// 商品
        /// </summary>
        [Navigate(nameof(CommodityCode))]
        public virtual ICollection<Commodity.Commodity> CommodityList { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        [Navigate(nameof(UserCode))]
        public virtual ICollection<User> User { get; set; }

    }
    /// <summary>
    /// 订单下单状态
    /// </summary>
    public enum OrderStatus
    {
        Success,
        Fail
    }
}
