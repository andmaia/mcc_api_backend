using Application.services.PaymentForm;
using Common.Responses.wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.PaymentForm.Commands
{
   

    public class EnablePaymentFormCommand : IRequest<IResponseWrapper>
    {
        public string Id { get; set; }
        public string CompanyId { get; set; }

    }

    public class EnablePaymentFormCommandHandler : IRequestHandler<EnablePaymentFormCommand, IResponseWrapper>
    {
        private readonly IPaymentFormService _service;

        public EnablePaymentFormCommandHandler(IPaymentFormService service)
        {
            _service = service;
        }

        public async Task<IResponseWrapper> Handle(EnablePaymentFormCommand request, CancellationToken cancellationToken)
        {

            return await _service.EnablePaymentForm(request.Id, request.CompanyId); ;
        }
    }
}
