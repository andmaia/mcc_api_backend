using Application.services.Branch;
using Common.requests.branch;
using Common.Responses.wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.branch.Commands
{
  
    public class DisableBranchCommand : IRequest<IResponseWrapper>
    {
        public string Id { get; set; }
    }

    public class DisableBranchCommandHandler : IRequestHandler<DisableBranchCommand, IResponseWrapper>
    {
        private IBranchService _service;

        public DisableBranchCommandHandler(IBranchService service)
        {
            _service = service;
        }

        public async Task<IResponseWrapper> Handle(DisableBranchCommand request, CancellationToken cancellationToken)
        {
            return await _service.DisableBranch(request.Id);
        }
    }
}
