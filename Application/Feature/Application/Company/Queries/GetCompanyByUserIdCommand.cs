using Application.services.Company;
using Common.Responses.wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Application.Company.Queries
{
    public class GetCompanyByUserIdCommand : IRequest<IResponseWrapper>
    {
        public string Id { get; set; }
    }

    public class GetCompanyByUserIdCommandHandler : IRequestHandler<GetCompanyByUserIdCommand, IResponseWrapper>
    {
        private readonly ICompanyService _service;

        public GetCompanyByUserIdCommandHandler(ICompanyService service)
        {
            _service = service;
        }

        public async Task<IResponseWrapper> Handle(GetCompanyByUserIdCommand request, CancellationToken cancellationToken)
        {
            return await _service.GetCompanyByUserId(request.Id);
        }
    }
}
