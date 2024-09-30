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
    public class FinishPreUserRegistrationCommand:IRequest<IResponseWrapper>
    {
        public UserRegistrationRequest Request { get; set; }
    }

    public class FinishPreUserRegistrationCommandHandler : IRequestHandler<FinishPreUserRegistrationCommand, IResponseWrapper>
    {
        private readonly IUserService _userService;
        private readonly IValidator<UserRegistrationRequest> _validator;

        public FinishPreUserRegistrationCommandHandler(IUserService userService, IValidator<UserRegistrationRequest> validator)
        {
            _userService = userService;
            _validator = validator;
        }

        public async Task<IResponseWrapper> Handle(FinishPreUserRegistrationCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request.Request);

            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return await ResponseWrapper<List<string>>.FailAsync(errorMessages);
            }

            return await _userService.FinishRegisterUserAsync(request.Request);
        }
    }
}
