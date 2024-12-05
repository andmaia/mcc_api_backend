using Common.requests.Order;
using Common.requests.PaymentForm;
using Common.Responses.wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.services.Order
{
    public interface IOrderService
    {
        Task<IResponseWrapper> CreateOrderAsync(CreateOrderRequest request);
        Task<IResponseWrapper> GetOrderById(string id);

    }
}
