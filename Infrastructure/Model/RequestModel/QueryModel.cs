using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Model.RequestModel
{
    public class QueryModel
    {
        /// <summary>
        /// 查询项
        /// </summary>
        public List<QueryItemModel> QueryItemList { get; set; }

    }

    public class QueryItemModel
    {
        /// <summary>
        /// 字段
        /// </summary>
        public string Field { get; set; }
        /// <summary>
        /// 运算符
        /// </summary>
        public EQueryModel Operator { get; set; }
        /// <summary>
        /// 值（如果是对象，请转换成json字符串）
        /// </summary>
        public string Value { get; set; }
    }

    /// <summary>
    /// 查询枚举
    /// </summary>
    public enum EQueryModel
    {
        /// <summary>
        /// 等于
        /// </summary>
        Equal,

        /// <summary>
        /// 不等于
        /// </summary>
        NotEqual,

        /// <summary>
        /// 小于
        /// </summary>
        LessThan,

        /// <summary>
        /// 小于等于
        /// </summary>
        LessThanOrEqual,

        /// <summary>
        /// 大于
        /// </summary>
        GreaterThan,

        /// <summary>
        /// 大于等于
        /// </summary>
        GreaterThanOrEqual,

        /// <summary>
        /// 包含
        /// </summary>
        Contains,

        /// <summary>
        /// 不包含
        /// </summary>
        NotContains,

        /// <summary>
        /// 全模糊(%arg%)
        /// </summary>
        Like,

        /// <summary>
        /// 左边匹配(arg%)
        /// </summary>
        StartsWith,

        /// <summary>
        /// 右边匹配(%arg)
        /// </summary>
        EndsWith,
        /// <summary>
        /// 多条件模糊(%arg%)
        /// </summary>
        LikeIn
    }
}
