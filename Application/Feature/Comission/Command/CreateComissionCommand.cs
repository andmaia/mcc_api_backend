using Application.services.Comission;
using Application.services.Order;
using Common.requests.Comission;
using Common.requests.Order;
using Common.Responses.wrappers;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Comission.Command
{
    public class CreateComissionCommand : IRequest<IResponseWrapper>
    {
        public CreateComissionRequest Request { get; set; }
    }

    public class CreateComissionCommandHandler : IRequestHandler<CreateComissionCommand, IResponseWrapper>
    {
        private readonly IComissionService _service;
        private readonly IValidator<CreateComissionRequest> _validator;

        public CreateComissionCommandHandler(IComissionService service, IValidator<CreateComissionRequest> validator)
        {
            _service = service;
            _validator = validator;
        }

        public async Task<IResponseWrapper> Handle(CreateComissionCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request.Request);

            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return await ResponseWrapper<List<string>>.FailAsync(errorMessages);
            }

            return await _service.CreateComissionAsync(request.Request);
        }
    }

}
