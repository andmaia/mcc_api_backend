using Application.services.Order;
using Application.services.Payment;
using Common.requests.Order;
using Common.requests.Payment;
using Common.Responses.wrappers;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Payment.Command
{
    public class CreatePaymentCommand : IRequest<IResponseWrapper>
    {
        public CreatePaymentRequest CreatePaymentRequest { get; set; }
    }
    public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, IResponseWrapper>
    {
        private readonly IPaymentService _service;
        private readonly IValidator<CreatePaymentRequest> _validator;

        public CreatePaymentCommandHandler(IPaymentService service, IValidator<CreatePaymentRequest> validator)
        {
            _service = service;
            _validator = validator;
        }

        public async Task<IResponseWrapper> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request.CreatePaymentRequest);

            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return await ResponseWrapper<List<string>>.FailAsync(errorMessages);
            }

            return await _service.CreatePayment(request.CreatePaymentRequest);
        }
    }

}
