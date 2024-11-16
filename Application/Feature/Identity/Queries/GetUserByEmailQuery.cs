using Application.services.identity;
using Common.Responses.wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Identity.Queries
{
    public class GetUserByEmailCommand : IRequest<IResponseWrapper>
    {
        public string Email { get; set; }
    }

    public class GetUserByEmailCommandHandler : IRequestHandler<GetUserByEmailCommand, IResponseWrapper>
    {
        private IUserService _service;

        public GetUserByEmailCommandHandler(IUserService service)
        {
            _service = service;
        }

        public async Task<IResponseWrapper> Handle(GetUserByEmailCommand request, CancellationToken cancellationToken)
        {
            return await _service.GetUserByEmailAsync(request.Email);
        }
    }


}
