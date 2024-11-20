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
    public class UpdateEmployeeCommand : IRequest<IResponseWrapper>
    {
        public UpdateEmployee updateEmployee { get; set; }
    }

    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, IResponseWrapper>
    {
        private readonly IEmployeService _employeService;
        private readonly IValidator<UpdateEmployee> _validator;

        public UpdateEmployeeCommandHandler(IEmployeService employeService, IValidator<UpdateEmployee> validator)
        {
            _employeService = employeService;
            _validator = validator;
        }

        public async Task<IResponseWrapper> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request.updateEmployee);

            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return await ResponseWrapper<List<string>>.FailAsync(errorMessages);
            }

            return await _employeService.UpdateEmployee(request.updateEmployee);
        }
    }
}
