using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Webapi.Controllers.Base;

namespace Webapi.Controllers.Home
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Probability")]
    public class ProbabilityController : BaseApiController
    {
        private readonly IProbabilityService _probabilityService;
        public ProbabilityController(IProbabilityService probabilityService)
        {
            _probabilityService = probabilityService;
        }

        /// <summary>
        /// 抽奖根据概率获取奖品
        /// </summary> 
        /// <returns></returns> 
        [HttpPost]
        public async Task<IActionResult> GetTurntableLotteryByProbability()
        {
            return await PackageResultAsync(await _probabilityService.GetTurntableLotteryByProbability());
        }
        /// <summary>
        /// 抽奖根据概率获取奖品，十连抽
        /// </summary> 
        /// <returns></returns> 
        [HttpPost]
        public async Task<IActionResult> GetTurntableLotteryListByProbability()
        {
            return await PackageResultAsync(await _probabilityService.GetTurntableLotteryListByProbability());
        }
    }
}
