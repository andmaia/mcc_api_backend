using Application.services.Employee;
using Common.Responses.wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Employee.Queries
{
    public class GetEmployeeByEmailCommand : IRequest<IResponseWrapper>
    {
        public string Email { get; set; }
        public string CompanyId { get; set; }
    }

    public class GetEmployeeByEmailCommandHandler : IRequestHandler<GetEmployeeByEmailCommand, IResponseWrapper>
    {
        private readonly IEmployeService _service;

        public GetEmployeeByEmailCommandHandler(IEmployeService service)
        {
            _service = service;
        }

        public async Task<IResponseWrapper> Handle(GetEmployeeByEmailCommand request, CancellationToken cancellationToken)
        {
            return await _service.GetEmployeeByEmail(request.Email, request.CompanyId);
        }
    }
}
