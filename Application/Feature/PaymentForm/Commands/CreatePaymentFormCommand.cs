using Application.services.Employee;
using Application.services.PaymentForm;
using Common.requests.Employee;
using Common.requests.PaymentForm;
using Common.Responses.wrappers;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.PaymentForm.Commands
{
    public class CreatePaymentFormCommand : IRequest<IResponseWrapper>
    {
        public CreatePaymentFormRequest CreatePaymentFormRequest { get; set; }
    }

    public class CreatePaymentFormCommandHandler : IRequestHandler<CreatePaymentFormCommand, IResponseWrapper>
    {
        private readonly IPaymentFormService _service;
        private readonly IValidator<CreatePaymentFormRequest> _validator;

        public CreatePaymentFormCommandHandler(IPaymentFormService service, IValidator<CreatePaymentFormRequest> validator)
        {
            _service = service;
            _validator = validator;
        }

        public async Task<IResponseWrapper> Handle(CreatePaymentFormCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request.CreatePaymentFormRequest);

            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return await ResponseWrapper<List<string>>.FailAsync(errorMessages);
            }

            return await _service.CreatePaymentForm(request.CreatePaymentFormRequest);
        }
    }
}
