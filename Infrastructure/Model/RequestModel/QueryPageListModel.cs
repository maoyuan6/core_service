using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Model.RequestModel
{
    public class QueryPageListModel
    {
        /// <summary>
        /// 当前页（1开始）
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// 条数
        /// </summary>
        public int Size { get; set; }
        /// <summary>
        /// 查询模型
        /// </summary>
        public QueryModel QueryModel { get; set; }
    }
}
