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
    public class FinishRegisterEmoloyeeCommand : IRequest<IResponseWrapper>
    {
        public FinishRegisterEmployee finishRegisterEmployee { get; set; }
    }

    public class FinishRegisterEmoloyeeCommandHandler : IRequestHandler<FinishRegisterEmoloyeeCommand, IResponseWrapper>
    {
        private readonly IEmployeService _employeService;
        private readonly IValidator<FinishRegisterEmployee> _validator;

        public FinishRegisterEmoloyeeCommandHandler(IEmployeService employeService, IValidator<FinishRegisterEmployee> validator)
        {
            _employeService = employeService;
            _validator = validator;
        }

        public async Task<IResponseWrapper> Handle(FinishRegisterEmoloyeeCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request.finishRegisterEmployee);

            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return await ResponseWrapper<List<string>>.FailAsync(errorMessages);
            }

            return await _employeService.FinishRegisterEmployee(request.finishRegisterEmployee);
        }
    }
}
