using Application.Feature.Identity.Commands;
using Application.services.Company;
using Application.services.identity;
using Common.requests.company;
using Common.requests.identity;
using Common.Responses.wrappers;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Application.Company.Commands
{
    public class CreateCompanyCommand : IRequest<IResponseWrapper>
    {
        public CompanyRequest CompanyRequest { get; set; }
    }

    public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, IResponseWrapper>
    {
        private readonly ICompanyService _companyService;
        private readonly IValidator<CompanyRequest> _validator;

        public CreateCompanyCommandHandler(ICompanyService companyService, IValidator<CompanyRequest> validator)
        {
            _companyService = companyService;
            _validator = validator;
        }

        public async Task<IResponseWrapper> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request.CompanyRequest);

            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return await ResponseWrapper<List<string>>.FailAsync(errorMessages);
            }

            return await _companyService.CreateCompany(request.CompanyRequest);
        }
    }

}
