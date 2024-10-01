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
    public class UpdateCellPhoneNumberCommand:IRequest<IResponseWrapper>
    {
        public UpdateCellPhoneNumberRequest UpdateCellPhoneNumberRequest { get; set; }
    }

    public class UpdateCellPhoneNumberCommandHandler : IRequestHandler<UpdateCellPhoneNumberCommand, IResponseWrapper>
    {
        private readonly IUserService _userService;
        private readonly IValidator<UpdateCellPhoneNumberRequest> _validator;

        public UpdateCellPhoneNumberCommandHandler(IUserService userService, IValidator<UpdateCellPhoneNumberRequest> validator)
        {
            _userService = userService;
            _validator = validator;
        }

        public async Task<IResponseWrapper> Handle(UpdateCellPhoneNumberCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request.UpdateCellPhoneNumberRequest);

            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return await ResponseWrapper<List<string>>.FailAsync(errorMessages);
            }

            return await _userService.UpdatePhoneNumber(request.UpdateCellPhoneNumberRequest);
        }
    }
}
