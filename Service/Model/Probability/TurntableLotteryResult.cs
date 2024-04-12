using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Model.Probability
{
    public class TurntableLotteryResult
    {
        /// <summary>
        /// 奖品
        /// </summary>
        public PrizeModel Prize { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Count { get; set; }
    }
}
