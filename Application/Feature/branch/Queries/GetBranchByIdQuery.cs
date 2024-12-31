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
    public class GetBranchByIdQuery : IRequest<IResponseWrapper>
    {
        public string Id { get; set; }
    }

    public class GetBranchByIdQueryHandler : IRequestHandler<GetBranchByIdQuery, IResponseWrapper>
    {
        private readonly IBranchService _service;

        public GetBranchByIdQueryHandler(IBranchService service)
        {
            _service = service;
        }

        public async Task<IResponseWrapper> Handle(GetBranchByIdQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetBranchById(request.Id);
        }
    }

}
