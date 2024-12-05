using Application.services.Order;
using Common.requests.Order;
using Common.Responses.wrappers;
using Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.services.order
{
    public class OrderService : IOrderService
    {

        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<IResponseWrapper> CreateOrderAsync(CreateOrderRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<IResponseWrapper> GetOrderById(string id)
        {
            throw new NotImplementedException();
        }
    }
}
