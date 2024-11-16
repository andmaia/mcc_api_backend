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
    public class GetCompanyByEmployeeIdCommand : IRequest<IResponseWrapper>
    {
        public string Id { get; set; }
    }

    public class GetCompanyByEmployeeIdCommandHandler : IRequestHandler<GetCompanyByEmployeeIdCommand, IResponseWrapper>
    {
        private readonly ICompanyService _service;

        public GetCompanyByEmployeeIdCommandHandler(ICompanyService service)
        {
            _service = service;
        }

        public async Task<IResponseWrapper> Handle(GetCompanyByEmployeeIdCommand request, CancellationToken cancellationToken)
        {
            return await _service.GetCompanyByEmployeeUd(request.Id);
        }
    }
}
