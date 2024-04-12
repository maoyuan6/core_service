using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Cache;
using Infrastructure.Extensions;
using Mapster;
using Repository.Entities;
using Repository.Entities.Prize;
using Service.Contracts;
using Service.Model.Probability;

namespace Service.Service
{
    public class ProbabilityService : IProbabilityService
    {
        private readonly IFreeSql _freeSql;
        private readonly ICacheService _cacheService;

        public ProbabilityService(IFreeSql freeSql, ICacheService cacheService)
        {
            _freeSql = freeSql;
            _cacheService = cacheService;
        }

        public async Task<TurntableLotteryResult> GetTurntableLotteryByProbability()
        {
            var repo = new List<Prize>();
            var redisKey = "AllPrize";
            var cacheString = _cacheService.GetCacheByKey(redisKey);
            if (cacheString.IsNullOrEmpty())
            {
                repo = await _freeSql.Select<Prize>().ToListAsync();
                _cacheService.SetCache(repo, redisKey);
            }
            else
            {
                repo = cacheString.ToList<Prize>();
            }
            // 生成一个0到1之间的随机数
            Random random = new Random();
            double r = random.NextDouble();

            // 计算累积概率并选择元素
            double cumulativeProbability = 0;
            Prize selectedItem = new Prize();
            foreach (var kvp in repo)
            {
                cumulativeProbability += (double)kvp.Probability;
                if (r <= cumulativeProbability)
                {
                    selectedItem = kvp;
                    break;
                }
            }

            var result = new TurntableLotteryResult
            {
                Prize = selectedItem.Adapt<PrizeModel>(),
                Count = await CriticalStrike(0.05) ? 1 : 2
            };
            return result;
        }

        public async Task<List<TurntableLotteryResult>> GetTurntableLotteryListByProbability()
        {
            var result = new List<TurntableLotteryResult>();
            for (int i = 0; i < 9; i++)
            {
                result.Add(await GetTurntableLotteryByProbability());
            }

            var temp = result.FirstOrDefault(a => a.Prize.Code is "SP004" or "SP005");
            if (temp.IsNotNullOrEmpty())
            {
                result.Add(await GetTurntableLotteryByProbability());
            }
            else
            {
                var repo = new List<Prize>();
                var redisKey = "AllPrize";
                var cacheString = _cacheService.GetCacheByKey(redisKey);
                if (cacheString.IsNullOrEmpty())
                {
                    repo = await _freeSql.Select<Prize>().ToListAsync();
                    _cacheService.SetCache(repo.ToJson(), redisKey);
                }
                else
                {
                    repo = cacheString.ToObject<List<Prize>>();
                }
                var data = repo.FirstOrDefault(a => a.Code == "SP004");
                result.Add(new TurntableLotteryResult()
                {
                    Prize = data.Adapt<PrizeModel>(),
                    Count = await CriticalStrike(0.05) ? 1 : 2
                });
            }
            return result;
        }

        public async Task<bool> CriticalStrike(double arg)
        {
            // 生成一个0到1之间的随机数
            Random random = new Random();
            double r = random.NextDouble();
            return r > arg;
        }
    }
}
