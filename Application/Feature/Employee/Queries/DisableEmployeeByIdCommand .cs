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
    public class DisableEmployeeByIdCommand : IRequest<IResponseWrapper>
    {
        public string EmployeeId { get; set; }
        public string CompanyId { get; set; }
    }

    public class DisableEmployeeByIdCommandHandler : IRequestHandler<DisableEmployeeByIdCommand, IResponseWrapper>
    {
        private readonly IEmployeService _service;

        public DisableEmployeeByIdCommandHandler(IEmployeService service)
        {
            _service = service;
        }

        public async Task<IResponseWrapper> Handle(DisableEmployeeByIdCommand request, CancellationToken cancellationToken)
        {
            return await _service.DisableEmployeeById(request.EmployeeId, request.CompanyId);
        }
    }
}
