using Application.services.identity;
using Common.Responses.identity;
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
    public class UserRegistrationCommand : IRequest<IResponseWrapper>
    {
        public UserRegistrationRequest UserRegistrationRequest { get; set; }
    }

    public class UserRegistrationCommandHandler : IRequestHandler<UserRegistrationCommand, IResponseWrapper>
    {
        private readonly IUserService _userService;
        private readonly IValidator<UserRegistrationRequest> _validator;

        public UserRegistrationCommandHandler(IUserService userService, IValidator<UserRegistrationRequest> validator)
        {
            _userService = userService;
            _validator = validator;
        }

        public async Task<IResponseWrapper> Handle(UserRegistrationCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request.UserRegistrationRequest);

            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return await ResponseWrapper<List<string>>.FailAsync(errorMessages);
            }

            return await _userService.RegisterUserAsync(request.UserRegistrationRequest);
        }
    }


}
