using Application.services.Branch;
using Common.Responses.wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.branch.Commands
{
    
    public class EnableBranchCommand : IRequest<IResponseWrapper>
    {
        public string Id { get; set; }
    }

    public class EnableBranchCommandHandler : IRequestHandler<EnableBranchCommand, IResponseWrapper>
    {
        private IBranchService _service;

        public EnableBranchCommandHandler(IBranchService service)
        {
            _service = service;
        }

        public async Task<IResponseWrapper> Handle(EnableBranchCommand request, CancellationToken cancellationToken)
        {
            return await _service.EnableBranch(request.Id);
        }
    }
}
