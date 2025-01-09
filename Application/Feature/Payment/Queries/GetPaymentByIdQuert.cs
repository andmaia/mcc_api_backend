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
    public class GetPaymentByIdQuery : IRequest<IResponseWrapper>
    {
        public string Id { get; set; }
    }

    public class GetPaymentByIdQueryHandler : IRequestHandler<GetPaymentByIdQuery, IResponseWrapper>
    {
        private readonly IPaymentService _service;

        public GetPaymentByIdQueryHandler(IPaymentService service)
        {
            _service = service;
        }

        public async Task<IResponseWrapper> Handle(GetPaymentByIdQuery request, CancellationToken cancellationToken)
        {
           return await _service.GetPaymentById(request.Id);
        }
    }



}
