using Application.services.identity;
using Application.Validators.Identity;
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
    public class UpdateRoleCommand : IRequest<IResponseWrapper>
    {
        public UpdateRoleRequest updateRoleRequest { get; set; }
    }

    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, IResponseWrapper>
    {

        private readonly IUserService _userService;
        private readonly IValidator<UpdateRoleRequest> _validator;

        public UpdateRoleCommandHandler(IUserService userService, IValidator<UpdateRoleRequest> validator)
        {
            _userService = userService;
            _validator = validator;
        }

        public async Task<IResponseWrapper> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request.updateRoleRequest);

            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return await ResponseWrapper<List<string>>.FailAsync(errorMessages);
            }

            return await _userService.UpdateRoleFromUser(request.updateRoleRequest);
        }
    }

}
