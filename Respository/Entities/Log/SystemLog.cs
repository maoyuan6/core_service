using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities.Log
{
    [Index("uk_id", "id", true)]
    [Table(Name = "system_log")]
    public class SystemLog : IEntityBase
    {
        /// <summary>
        /// 
        /// </summary>
        [Column(IsIdentity = true, Name = "id")]
        public int Id { get; set; }
        /// <summary>
        /// 日志类型
        /// </summary>
        [Column(IsNullable = false, Name = "type", StringLength = 128)]
        public string Type { get; set; }
        /// <summary>
        /// 日志信息
        /// </summary>
        [Column(IsNullable = false, Name = "log_info", DbType = "LONGTEXT")]
        public string LogInfo { get; set; }

        /// <summary>
        /// 日志记录时间
        /// </summary>
        [Column(IsNullable = false, Name = "log_time")]
        public DateTime LogTime { get; set; }
    }
}
