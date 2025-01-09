using Application.services.Payment;
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
    public class UpdatePaymentCommand : IRequest<IResponseWrapper>
    {
        public UpdatePaymentRequest updatePaymentRequest { get; set; }
    }

    public class UpdatePaymentCommandHandler : IRequestHandler<UpdatePaymentCommand, IResponseWrapper>
    {
        private readonly IPaymentService _service;
        private readonly IValidator<UpdatePaymentRequest> _validator;

        public UpdatePaymentCommandHandler(IPaymentService service, IValidator<UpdatePaymentRequest> validator)
        {
            _service = service;
            _validator = validator;
        }

        public async Task<IResponseWrapper> Handle(UpdatePaymentCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request.updatePaymentRequest);

            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return await ResponseWrapper<List<string>>.FailAsync(errorMessages);
            }

            return await _service.UpdatePayment(request.updatePaymentRequest);
        }
    }

}
