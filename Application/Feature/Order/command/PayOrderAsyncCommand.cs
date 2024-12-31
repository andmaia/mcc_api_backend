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
    public class PayOrderAsyncCommand : IRequest<IResponseWrapper>
    {
        public string Id { get; set; }
    }

    public class PayOrderAsyncCommandHandler : IRequestHandler<PayOrderAsyncCommand, IResponseWrapper>
    {
        private readonly IOrderService _orderService;

        public PayOrderAsyncCommandHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<IResponseWrapper> Handle(PayOrderAsyncCommand request, CancellationToken cancellationToken)
        {
            return await _orderService.PayOrderAsync(request.Id);
        }
    }
}