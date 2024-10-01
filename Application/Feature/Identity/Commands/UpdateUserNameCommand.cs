using Application.services.identity;
using Common.requests.identity;
using Common.Responses.wrappers;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Identity.Commands
{
    public class UpdateUserNameCommand:IRequest<IResponseWrapper>
    {
        public UpdateUserNameRequest UpdateUserNameRequest { get; set; }
    }

    public class UpdateUserNameCommandHandler : IRequestHandler<UpdateUserNameCommand, IResponseWrapper>
    {
        private readonly IUserService _userService;
        private readonly IValidator<UpdateUserNameRequest> _validator;

        public UpdateUserNameCommandHandler(IUserService userService, IValidator<UpdateUserNameRequest> validator)
        {
            _userService = userService;
            _validator = validator;
        }

        public async Task<IResponseWrapper> Handle(UpdateUserNameCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request.UpdateUserNameRequest);

            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return await ResponseWrapper<List<string>>.FailAsync(errorMessages);
            }

            return await _userService.UpdateUserNameAsync(request.UpdateUserNameRequest);
        }
    }
}
