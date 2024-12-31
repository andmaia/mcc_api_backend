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
    public class UnpayOrderAsyncCommand : IRequest<IResponseWrapper>
    {
        public string Id { get; set; }
    }

    public class UnpayOrderAsyncCommandHandler : IRequestHandler<UnpayOrderAsyncCommand, IResponseWrapper>
    {
        private readonly IOrderService _orderService;

        public UnpayOrderAsyncCommandHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<IResponseWrapper> Handle(UnpayOrderAsyncCommand request, CancellationToken cancellationToken)
        {
            // Chama o serviço para desmarcar o pagamento da ordem
            return await _orderService.UnpayOrderAsync(request.Id);
        }
    }

}
