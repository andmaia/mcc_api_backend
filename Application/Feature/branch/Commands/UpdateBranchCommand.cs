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
    public class UpdateBranchCommand : IRequest<IResponseWrapper>
    {
        public UpdateBranchRequest Request { get; set; }
    }

    public class UpdateBranchCommandHandler : IRequestHandler<UpdateBranchCommand, IResponseWrapper>
    {
        private readonly IBranchService _service;

        public UpdateBranchCommandHandler(IBranchService service)
        {
            _service = service;
        }

        public async Task<IResponseWrapper> Handle(UpdateBranchCommand request, CancellationToken cancellationToken)
        {
            return await _service.UpdateBranch(request.Request);
        }
    }
}
