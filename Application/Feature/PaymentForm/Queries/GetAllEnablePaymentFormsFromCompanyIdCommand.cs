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
 

    public class GetAllEnablePaymentFormsFromCompanyIdCommand : IRequest<IResponseWrapper>
    {
        public string CompanyId { get; set; }
    }

    public class GetAllEnablePaymentFormsFromCompanyIdCommandHandler : IRequestHandler<GetAllEnablePaymentFormsFromCompanyIdCommand, IResponseWrapper>
    {
        private readonly IPaymentFormService _service;

        public GetAllEnablePaymentFormsFromCompanyIdCommandHandler(IPaymentFormService service)
        {
            _service = service;
        }

        public async Task<IResponseWrapper> Handle(GetAllEnablePaymentFormsFromCompanyIdCommand request, CancellationToken cancellationToken)
        {
            return await _service.GetAllEnablePaymentFormsFromCompanyId(request.CompanyId);
        }
    }
}
