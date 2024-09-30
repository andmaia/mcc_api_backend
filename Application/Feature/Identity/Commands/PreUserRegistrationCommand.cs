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
    public class PreUserRegistrationCommand:IRequest<IResponseWrapper>
    {
        public UserPreRegistrationRequest Request { get; set; }
    }

    public class PreUserRegistrationCommandHandler : IRequestHandler<PreUserRegistrationCommand, IResponseWrapper>
    {

        private readonly IUserService _userService;
        private readonly IValidator<UserPreRegistrationRequest> _validator;

        public PreUserRegistrationCommandHandler(IUserService userService, IValidator<UserPreRegistrationRequest> validator)
        {
            _userService = userService;
            _validator = validator;
        }

        public async Task<IResponseWrapper> Handle(PreUserRegistrationCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request.Request);

            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return await ResponseWrapper<List<string>>.FailAsync(errorMessages);
            }

            return await _userService.PreRegisterUserAsync(request.Request);
        }
    }
}
