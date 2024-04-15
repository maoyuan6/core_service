using FreeSql.DataAnnotations;
using Repository.Entities.Product.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities.Product.Commodity
{
    [Index("uk_id", "commodity_id", true)]
    [Index("uk_code", "code", true)]
    [Table(Name = "commodity")]
    public class Commodity : IEntityBase
    {
        /// <summary>
        /// ID
        /// </summary>
        [Column(IsIdentity = true, Name = "id")]
        public int  Id { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        [Column(IsNullable = false, Name = "code", StringLength = 128)]
        public string Code { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        [Column(IsNullable = false, Name = "name", StringLength = 128)]
        public string Name { get; set; }
        
        /// <summary>
        /// 商品价格
        /// </summary>
        [Column(IsNullable = false, Name = "price",DbType = "decimal(10,2)")]
        public decimal Price { get; set; }

        /// <summary>
        /// 库存
        /// </summary>
        [Column(IsNullable = false, Name = "inventory")]
        public int Inventory { get; set; } 
    }
}
