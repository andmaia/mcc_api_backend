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
    

    public class GetBranchsByCompanyQuery : IRequest<IResponseWrapper>
    {
        public string Id { get; set; }
    }

    public class GetBranchsByCompanyQueryHandler : IRequestHandler<GetBranchsByCompanyQuery, IResponseWrapper>
    {
        private readonly IBranchService _service;

        public GetBranchsByCompanyQueryHandler(IBranchService service)
        {
            _service = service;
        }

        public async Task<IResponseWrapper> Handle(GetBranchsByCompanyQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetBranchsByCompany(request.Id);
        }
    }
}
