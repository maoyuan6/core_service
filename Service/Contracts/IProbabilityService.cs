using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Base;
using Service.Model.Probability;

namespace Service.Contracts
{
    public interface IProbabilityService : IBaseService
    {
        /// <summary>
        /// 单抽
        /// </summary>
        /// <returns></returns>
        Task<TurntableLotteryResult> GetTurntableLotteryByProbability();
        /// <summary>
        /// 十连抽
        /// </summary>
        /// <returns></returns>
        Task<List<TurntableLotteryResult>> GetTurntableLotteryListByProbability();
        /// <summary>
        /// 暴击
        /// </summary>
        /// <param name="arg">概率</param>
        /// <returns></returns>
        Task<bool> CriticalStrike(double arg);
    }
}
