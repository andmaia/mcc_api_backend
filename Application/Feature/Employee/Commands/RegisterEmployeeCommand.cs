using Application.services.Company;
using Application.services.Employee;
using Common.requests.company;
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
    public class RegisterEmployeeCommand : IRequest<IResponseWrapper>
    {
       public EmployeeRegisterRequest employeeRegisterRequest { get; set; }
    }

    public class RegisterEmployeeCommandHandler : IRequestHandler<RegisterEmployeeCommand, IResponseWrapper>
    {
        private readonly IEmployeService _employeService;
        private readonly IValidator<EmployeeRegisterRequest> _validator;

        public RegisterEmployeeCommandHandler(IEmployeService employeService, IValidator<EmployeeRegisterRequest> validator)
        {
            _employeService = employeService;
            _validator = validator;
        }

        public async Task<IResponseWrapper> Handle(RegisterEmployeeCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request.employeeRegisterRequest);

            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return await ResponseWrapper<List<string>>.FailAsync(errorMessages);
            }

            return await _employeService.RegisterEmployee(request.employeeRegisterRequest);
        }
    }

}
