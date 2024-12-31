using Application.services.Order;
using Common.requests.Order;
using Common.Responses.wrappers;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Order.command
{
    public class UpdateOrderAsyncCommand : IRequest<IResponseWrapper>
    {
        public UpdateOrderRequest UpdateOrderRequest { get; set; }
    }
    public class UpdateOrderAsyncCommandHandler : IRequestHandler<UpdateOrderAsyncCommand, IResponseWrapper>
    {
        private readonly IOrderService _orderService;
        private readonly IValidator<UpdateOrderRequest> _validator;

        public UpdateOrderAsyncCommandHandler(IOrderService orderService, IValidator<UpdateOrderRequest> validator)
        {
            _orderService = orderService;
            _validator = validator;
        }

        public async Task<IResponseWrapper> Handle(UpdateOrderAsyncCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request.UpdateOrderRequest, cancellationToken);
            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return await ResponseWrapper<List<string>>.FailAsync(errorMessages);
            }

            return await _orderService.UpdateOrderAsync(request.UpdateOrderRequest);
        }
    }
}
