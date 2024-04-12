using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Model.Probability
{
    /// <summary>
    /// 奖品信息
    /// </summary>
    public class PrizeModel
    {
        /// <summary>
        /// 奖品编号
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 奖品名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 奖品备注
        /// </summary>
        public string Remark { get; set; }
    }
}
