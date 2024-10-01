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
    public class UpdatePasswordCommand:IRequest<IResponseWrapper>
    {
        public UpdatePasswordRequest UpdatePasswordRequest { get; set; }
    }

    public class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommand, IResponseWrapper>
    {
        private readonly IUserService _userService;
        private readonly IValidator<UpdatePasswordRequest> _validator;

        public UpdatePasswordCommandHandler(IUserService userService, IValidator<UpdatePasswordRequest> validator)
        {
            _userService = userService;
            _validator = validator;
        }

        public async Task<IResponseWrapper> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request.UpdatePasswordRequest);

            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return await ResponseWrapper<List<string>>.FailAsync(errorMessages);
            }

            return await _userService.UpdatePasswordAsync(request.UpdatePasswordRequest);
        }
    }
}
