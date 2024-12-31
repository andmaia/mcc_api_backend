using Application.services.Branch;
using Common.Responses.wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.branch.Queries
{
    

    public class GetOrdersByBranchQuery : IRequest<IResponseWrapper>
    {
        public string Id { get; set; }
    }

    public class GetOrdersByBranchQueryHandler : IRequestHandler<GetOrdersByBranchQuery, IResponseWrapper>
    {
        private readonly IBranchService _service;

        public GetOrdersByBranchQueryHandler(IBranchService service)
        {
            _service = service;
        }

        public async Task<IResponseWrapper> Handle(GetOrdersByBranchQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetOrdersByBranch(request.Id);
        }
    }
}
