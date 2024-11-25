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
   
    public class GetAllDisablePaymentFormsFromCompanyIdCommand : IRequest<IResponseWrapper>
    {
        public string CompanyId { get; set; }
    }

    public class GetAllDisablePaymentFormsFromCompanyIdCommandHandler : IRequestHandler<GetAllDisablePaymentFormsFromCompanyIdCommand, IResponseWrapper>
    {
        private readonly IPaymentFormService _service;

        public GetAllDisablePaymentFormsFromCompanyIdCommandHandler(IPaymentFormService service)
        {
            _service = service;
        }

        public async Task<IResponseWrapper> Handle(GetAllDisablePaymentFormsFromCompanyIdCommand request, CancellationToken cancellationToken)
        {
            return await _service.GetAllDisablePaymentFormsFromCompanyId(request.CompanyId);
        }
    }
}
