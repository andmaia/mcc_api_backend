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

namespace Application.Feature.Application.Company.Commands
{
    public class UpdateCompanyCommand : IRequest<IResponseWrapper>
    {
        public CompanyUpdate CompanyRequest { get; set; }
    }

    public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand, IResponseWrapper>
    {
        private readonly ICompanyService _companyService;
        private readonly IValidator<CompanyUpdate> _validator;

        public UpdateCompanyCommandHandler(ICompanyService companyService, IValidator<CompanyUpdate> validator)
        {
            _companyService = companyService;
            _validator = validator;
        }

        public async Task<IResponseWrapper> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request.CompanyRequest);

            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return await ResponseWrapper<List<string>>.FailAsync(errorMessages);
            }

            return await _companyService.UpdateCompany(request.CompanyRequest);
        }
    }
}
