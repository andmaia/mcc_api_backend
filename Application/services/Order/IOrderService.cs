using Common.requests.Order;
using Common.requests.PaymentForm;
using Common.Responses.order;
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
        Task<IResponseWrapper<ResponseOrder>> CreateOrderAsync(CreateOrderRequest request);
        Task<IResponseWrapper<ResponseOrder>> GetOrderById(string id);
        Task<IResponseWrapper<IList<ResponseOrder>>> SearchOrdersByEmployee(OrderSearchFilter orderSearchFilter,int page = 1,int take = 10);
        Task<IResponseWrapper<IList<ResponseOrder>>> SearchOrdersByBranch(OrderSearchFilter orderSearchFilter, int page = 1, int take = 10);
        Task<IResponseWrapper<IList<ResponseOrder>>> SearchOrdersByCompany(OrderSearchFilter orderSearchFilter, int page = 1, int take = 10);

        Task<IResponseWrapper> DisableOrder(string id);
        Task<IResponseWrapper> PayOrderAsync(string id);
        Task<IResponseWrapper> UnpayOrderAsync(string id);

        Task<ResponseWrapper<ResponseOrder>> UpdateOrderAsync(UpdateOrderRequest request);

    }
}
