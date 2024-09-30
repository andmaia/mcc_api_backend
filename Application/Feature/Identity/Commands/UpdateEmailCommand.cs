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
    public class UpdateEmailCommand:IRequest<IResponseWrapper>
    {
        public UpdateEmailRequest Request { get; set; }
    }

    public class UpdateEmailCommandHandler : IRequestHandler<UpdateEmailCommand, IResponseWrapper>
    {
        private readonly IUserService _userService;
        private readonly IValidator<UpdateEmailRequest> _validator;

        public UpdateEmailCommandHandler(IUserService userService, IValidator<UpdateEmailRequest> validator)
        {
            _userService = userService;
            _validator = validator;
        }

        public async Task<IResponseWrapper> Handle(UpdateEmailCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request.Request);

            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return await ResponseWrapper<List<string>>.FailAsync(errorMessages);
            }

            return await _userService.UpdateEmailUserAsync(request.Request);
        }
    }
}
