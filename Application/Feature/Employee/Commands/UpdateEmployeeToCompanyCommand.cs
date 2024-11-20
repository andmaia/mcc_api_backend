using Application.services.Employee;
using Common.requests.Employee;
using Common.Responses.wrappers;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Employee.Commands
{
    public class UpdateEmployeeToCompanyCommand : IRequest<IResponseWrapper>
    {
        public UpdateEmployeeToCompany updateEmployeeToCompany { get; set; }
    }

    public class UpdateEmployeeToCompanyCommandHandler : IRequestHandler<UpdateEmployeeToCompanyCommand, IResponseWrapper>
    {
        private readonly IEmployeService _employeService;
        private readonly IValidator<UpdateEmployeeToCompany> _validator;

        public UpdateEmployeeToCompanyCommandHandler(IEmployeService employeService, IValidator<UpdateEmployeeToCompany> validator)
        {
            _employeService = employeService;
            _validator = validator;
        }

        public async Task<IResponseWrapper> Handle(UpdateEmployeeToCompanyCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request.updateEmployeeToCompany);

            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return await ResponseWrapper<List<string>>.FailAsync(errorMessages);
            }

            return await _employeService.UpdateEmployeeToCompany(request.updateEmployeeToCompany);
        }
    }
}
