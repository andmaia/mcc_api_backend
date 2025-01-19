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
  
    public class UnPayComissionCommand : IRequest<IResponseWrapper>
    {
        public string Id { get; set; }
    }

    public class UnPayComissionCommandHandler : IRequestHandler<UnPayComissionCommand, IResponseWrapper>
    {

        private IComissionService _service;

        public UnPayComissionCommandHandler(IComissionService service)
        {
            _service = service;
        }

        public async Task<IResponseWrapper> Handle(UnPayComissionCommand request, CancellationToken cancellationToken)
        {
            return await _service.UnpayComission(request.Id);
        }
    }
}
