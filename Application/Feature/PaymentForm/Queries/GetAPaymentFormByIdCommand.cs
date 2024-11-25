using Application.services.Employee;
using Application.services.PaymentForm;
using Common.Responses.wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.PaymentForm.Queries
{
    public class GetAPaymentFormByIdCommand : IRequest<IResponseWrapper>
    {
        public string Id { get; set; }
    }

    public class GetAPaymentFormByIdCommandHandler : IRequestHandler<GetAPaymentFormByIdCommand, IResponseWrapper>
    {
        private readonly IPaymentFormService  _service;

        public GetAPaymentFormByIdCommandHandler(IPaymentFormService service)
        {
            _service = service;
        }

        public async Task<IResponseWrapper> Handle(GetAPaymentFormByIdCommand request, CancellationToken cancellationToken)
        {
            return await _service.GetAPaymentFormById(request.Id);
        }
    }
}
