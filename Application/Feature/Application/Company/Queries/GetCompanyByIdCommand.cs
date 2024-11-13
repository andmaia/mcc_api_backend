using Application.services.Company;
using Common.requests.company;
using Common.Responses.wrappers;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Application.Company.Queries
{
    public class GetCompanyByIdCommand : IRequest<IResponseWrapper>
    {
        public string Id { get; set; }
    }

    public class GetCompanyByIdCommandHandler : IRequestHandler<GetCompanyByIdCommand, IResponseWrapper>
    {
        private readonly ICompanyService _companyService;

        public GetCompanyByIdCommandHandler(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        public async Task<IResponseWrapper> Handle(GetCompanyByIdCommand request, CancellationToken cancellationToken)
        {
            return await _companyService.GetCompanyById(request.Id);
        }
    }
}
