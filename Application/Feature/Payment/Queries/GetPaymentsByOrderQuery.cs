using Application.services.Payment;
using Common.Responses.wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Payment.Queries
{
  


    public class GetPaymentsByOrderQuery : IRequest<IResponseWrapper>
    {
        public string Id { get; set; }
    }

    public class GetPaymentsByOrderQueryHandler : IRequestHandler<GetPaymentsByOrderQuery, IResponseWrapper>
    {
        private readonly IPaymentService _service;

        public GetPaymentsByOrderQueryHandler(IPaymentService service)
        {
            _service = service;
        }

        public async Task<IResponseWrapper> Handle(GetPaymentsByOrderQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetPaymentsByOrderId(request.Id);
        }
    }
}
