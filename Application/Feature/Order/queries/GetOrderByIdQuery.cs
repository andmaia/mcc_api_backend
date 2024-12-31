using Application.services.Order;
using Common.Responses.wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Order.queries
{
    public class GetOrderByIdQuery : IRequest<IResponseWrapper>
    {
        public string Id { get; set; }
    }

    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, IResponseWrapper>
    {
        private readonly IOrderService _orderService;

        public GetOrderByIdQueryHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<IResponseWrapper> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            // Chama o serviço para obter a ordem pelo Id
            return await _orderService.GetOrderById(request.Id);
        }
    }

}
