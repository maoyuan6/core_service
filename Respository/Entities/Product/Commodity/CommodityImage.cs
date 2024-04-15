using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities.Product.Commodity
{
    /// <summary>
    /// 商品图片
    /// </summary>
    [Index("uk_id", "id", true)]
    [Index("uk_code", "code", true)]
    [Table(Name = "commodity_image")]
    public class CommodityImage : IEntityBase
    {
        /// <summary>
        /// ID
        /// </summary>
        /// <summary> 
        [Column(IsIdentity = true, Name = "id")]
        public int Id { get; set; }
        /// <summary>
        /// 商品编号
        /// </summary> 
        [Column(IsNullable = false, Name = "code", StringLength = 128)]
        public string Code { get; set; }
        /// <summary>
        /// 商品图片
        /// </summary> 
        [Column(IsNullable = false, Name = "image")]
        public string Image { get; set; } 
    }
}
