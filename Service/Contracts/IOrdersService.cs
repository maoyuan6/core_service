using Infrastructure.Base;
using Service.Model.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IOrdersService : IBaseService
    {
        Task<bool> OrderJoinMQAsync(CreateOrderModel arg);

        Task<CreateOrdersResult> OrderSubmitAsync(CreateOrderModel arg);

    }
}
