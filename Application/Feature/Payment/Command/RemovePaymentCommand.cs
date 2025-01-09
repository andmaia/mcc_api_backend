using Application.services.Payment;
using Common.Responses.wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Payment.Command
{
    public class RemovePaymentCommand : IRequest<IResponseWrapper>
    {
        public string Id { get; set; }
    }
    public class RemovePaymentCommandHandler : IRequestHandler<RemovePaymentCommand, IResponseWrapper>
    {
        private readonly IPaymentService _service;

        public RemovePaymentCommandHandler(IPaymentService service)
        {
            _service = service;
        }

        public async Task<IResponseWrapper> Handle(RemovePaymentCommand request, CancellationToken cancellationToken)
        {
            return await _service.RemovePayment(request.Id);

        }
    }
}
