using Application.services.Order;
using Application.Validators.Orders;
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
    public class CreateOrderAsyncCommand : IRequest<IResponseWrapper>
    {
        public CreateOrderRequest CreateOrderAsyncRequest { get; set; }
    }

    public class CreateOrderAsyncCommandHandler : IRequestHandler<CreateOrderAsyncCommand, IResponseWrapper>
    {
        private readonly IOrderService _service;
        private readonly IValidator<CreateOrderRequest> _validator;

        public CreateOrderAsyncCommandHandler(IOrderService service, IValidator<CreateOrderRequest> validator)
        {
            _service = service;
            _validator = validator;
        }

        public async Task<IResponseWrapper> Handle(CreateOrderAsyncCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request.CreateOrderAsyncRequest);

            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return await ResponseWrapper<List<string>>.FailAsync(errorMessages);
            }

            return await _service.CreateOrderAsync(request.CreateOrderAsyncRequest);
        }
    }
}
