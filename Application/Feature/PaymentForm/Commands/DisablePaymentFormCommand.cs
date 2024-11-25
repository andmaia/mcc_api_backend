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
    

    public class DisablePaymentFormCommand : IRequest<IResponseWrapper>
    {
        public string Id { get; set; }
        public string CompanyId { get; set; }

    }

    public class DisablePaymentFormCommandHandler : IRequestHandler<DisablePaymentFormCommand, IResponseWrapper>
    {
        private readonly IPaymentFormService _service;

        public DisablePaymentFormCommandHandler(IPaymentFormService service)
        {
            _service = service;
        }

        public async Task<IResponseWrapper> Handle(DisablePaymentFormCommand request, CancellationToken cancellationToken)
        {

            return await _service.DisablePaymentForm(request.Id,request.CompanyId);;
        }
    }
}
