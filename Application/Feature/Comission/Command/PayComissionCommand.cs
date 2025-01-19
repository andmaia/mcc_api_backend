using Application.services.Comission;
using Common.Responses.wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Comission.Command
{
    public class PayComissionCommand : IRequest<IResponseWrapper>
    {
        public string Id { get; set; }
    }

    public class PayComissionCommandHandler : IRequestHandler<PayComissionCommand, IResponseWrapper>
    {

        private IComissionService _service;

        public PayComissionCommandHandler(IComissionService service)
        {
            _service = service;
        }

        public async Task<IResponseWrapper> Handle(PayComissionCommand request, CancellationToken cancellationToken)
        {
            return await _service.PayComission(request.Id);
        }
    }

}
