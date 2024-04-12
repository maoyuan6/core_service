using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities.Prize
{

    [Index("uk_id", "id", true)]
    [Index("uk_code", "code", true)]
    [Table(Name = "prize")]
    public class Prize : IEntityBase
    {
        /// <summary>
        /// 奖品ID
        /// </summary> 
        [Column(IsIdentity = true, Name = "id")]
        public int Id { get; set; }
        /// <summary>
        /// 奖品编号
        /// </summary>
        [Column(IsNullable = false, Name = "code", StringLength = 128)]
        public string Code { get; set; }
        /// <summary>
        /// 奖品名称
        /// </summary>
        [Column(IsNullable = false, Name = "name", StringLength = 128)]
        public string Name { get; set; }
        /// <summary>
        /// 奖品备注
        /// </summary> 
        [Column(IsNullable = false, Name = "remark")]
        public string Remark { get; set; }
        /// <summary>
        /// 库存
        /// </summary>
        [Column(IsNullable = false, Name = "inventory")]
        public long Inventory { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        [Column(IsNullable = false, Name = "image")]
        public string Image { get; set; }
        /// <summary>
        /// 概率
        /// </summary>
        [Column(IsNullable = false, Name = "probability")]
        public decimal Probability { get; set; }
    }
}
