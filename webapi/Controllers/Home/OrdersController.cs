using Infrastructure.Model.RequestModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Service.Model.Order;
using Webapi.Controllers.Base;

namespace Webapi.Controllers.Home
{
    /// <summary>
    /// 商品订单管理
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Commodity")]
    public class OrdersController : BaseApiController
    {
        private readonly IOrdersService _ordersService;
        public OrdersController(IOrdersService ordersService)
        {
            _ordersService = ordersService;
        }
        /// <summary>
        /// 下单,将订单添加进队列
        /// </summary>
        /// <param name="arg"></param> 
        /// <returns></returns> 
        [HttpPost]
        public async Task<IActionResult> OrderJoinMQAsync(CreateOrderModel arg)
        {
            return await PackageResultAsync(await _ordersService.OrderJoinMQAsync(arg));
        }
        /// <summary>
         /// 直接消费
         /// </summary>
         /// <param name="arg"></param> 
         /// <returns></returns> 
        [HttpPost]
        public async Task<IActionResult> OrderSubmitAsync(CreateOrderModel arg)
        {
            return await PackageResultAsync(await _ordersService.OrderSubmitAsync(arg));
        }

    }
}
