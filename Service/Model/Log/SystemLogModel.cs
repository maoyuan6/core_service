using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Model.Log
{
    public class SystemLogModel
    {
        public int Id { get; set; }
        /// <summary>
        /// 日志类型
        /// </summary> 
        public string Type { get; set; }
        /// <summary>
        /// 日志信息
        /// </summary> 
        public string LogInfo { get; set; }

        /// <summary>
        /// 日志记录时间
        /// </summary> 
        public DateTime LogTime { get; set; }
    }
}
