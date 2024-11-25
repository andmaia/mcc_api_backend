using Application.services.PaymentForm;
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
    
    public class UpdatePaymentFormCommand : IRequest<IResponseWrapper>
    {
        public UpdatePaymentFormRequest UpdatePaymentFormRequest { get; set; }
    }

    public class UpdatePaymentFormCommandHandler : IRequestHandler<UpdatePaymentFormCommand, IResponseWrapper>
    {
        private readonly IPaymentFormService _service;
        private readonly IValidator<UpdatePaymentFormRequest> _validator;

        public UpdatePaymentFormCommandHandler(IPaymentFormService service, IValidator<UpdatePaymentFormRequest> validator)
        {
            _service = service;
            _validator = validator;
        }

        public async Task<IResponseWrapper> Handle(UpdatePaymentFormCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request.UpdatePaymentFormRequest);

            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return await ResponseWrapper<List<string>>.FailAsync(errorMessages);
            }

            return await _service.UpdatePaymentForm(request.UpdatePaymentFormRequest);
        }
    }
}
