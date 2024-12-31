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
    public class CreateBranchCommand : IRequest<IResponseWrapper>
    {
        public CreateBranchRequest Request { get; set; }
    }

    public class CreateBranchCommandHandler : IRequestHandler<CreateBranchCommand, IResponseWrapper>
    {
        private IBranchService _service;

        public CreateBranchCommandHandler(IBranchService service)
        {
            _service = service;
        }

        public async Task<IResponseWrapper> Handle(CreateBranchCommand request, CancellationToken cancellationToken)
        {
            return await _service.CreateBranch(request.Request);
        }
    }

}
