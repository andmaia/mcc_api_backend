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
 

    public class GetAllPaymentFormsFromCompanyIdCommand : IRequest<IResponseWrapper>
    {
        public string CompanyId { get; set; }
    }

    public class GetAllPaymentFormsFromCompanyIdCommandHandler : IRequestHandler<GetAllPaymentFormsFromCompanyIdCommand, IResponseWrapper>
    {
        private readonly IPaymentFormService _service;

        public GetAllPaymentFormsFromCompanyIdCommandHandler(IPaymentFormService service)
        {
            _service = service;
        }

        public async Task<IResponseWrapper> Handle(GetAllPaymentFormsFromCompanyIdCommand request, CancellationToken cancellationToken)
        {
            return await _service.GetAllPaymentFormsFromCompanyId(request.CompanyId);
        }
    }
}
