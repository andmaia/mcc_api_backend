using Application.services.Expense;
using Common.requests.Expense;
using Common.Responses.wrappers;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Expense.Command
{
   

    public class UpdateExpeseCommand : IRequest<IResponseWrapper>
    {
        public UpdateExpenseRequest Request { get; set; }
    }

    public class CUpdateExpeseCommandHandler : IRequestHandler<UpdateExpeseCommand, IResponseWrapper>
    {
        private readonly IExpenseService _service;
        private readonly IValidator<UpdateExpenseRequest> _validator;

        public CUpdateExpeseCommandHandler(IExpenseService service, IValidator<UpdateExpenseRequest> validator)
        {
            _service = service;
            _validator = validator;
        }

        public async Task<IResponseWrapper> Handle(UpdateExpeseCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request.Request);

            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return await ResponseWrapper<List<string>>.FailAsync(errorMessages);
            }

            return await _service.UpdateExpese(request.Request);
        }
    }
}
