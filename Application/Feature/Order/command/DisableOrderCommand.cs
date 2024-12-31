using Application.services.Order;
using Common.Responses.wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Order.command
{
    public class DisableOrderCommand : IRequest<IResponseWrapper>
    {
        public string Id { get; set; }
    }
    public class DisableOrderCommandHandler : IRequestHandler<DisableOrderCommand, IResponseWrapper>
    {
        private readonly IOrderService _orderService;

        public DisableOrderCommandHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<IResponseWrapper> Handle(DisableOrderCommand request, CancellationToken cancellationToken)
        {
            return await _orderService.DisableOrder(request.Id);
        }
    }
}
