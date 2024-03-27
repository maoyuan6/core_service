using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreeSql.DataAnnotations;

namespace Repository.Entities.Product.Commodity
{
    /// <summary>
    /// 商品详情
    /// </summary>
    [Index("uk_id", "id", true)]
    [Index("uk_code", "code", true)]
    [Table(Name = "commodity_info")]
    public class CommodityInfo : IEntityBase
    {
        /// <summary>
        /// ID 
        /// </summary>
        [Column(IsIdentity = true, Name = "id")]
        public int Id { get; set; }
        /// <summary>
        /// 商品编号
        /// </summary>
        [Column(IsIdentity = true, Name = "code")]
        public string Code { get; set; } 
        /// <summary>
        /// 商品标题
        /// </summary>
        [Column(IsNullable = false, Name = "title", StringLength = 128)]
        public string Title { get; set; } 
        /// <summary>
        /// 商品简介
        /// </summary>
        [Column(IsNullable = false, Name = "introduction")]
        public string Introduction { get; set; }

        /// <summary>
        /// 商品
        /// </summary>
        [Navigate(nameof(Code))]
        public virtual Commodity Commodity { get; set; }
    }
}
