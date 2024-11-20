using Application.services.Company;
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
    public class GetAllEmployeesFromCompanyCommand : IRequest<IResponseWrapper>
    {
        public string idCompany { get; set; }
    }

    public class GetCompanyByEmployeeIdCommandHandler : IRequestHandler<GetAllEmployeesFromCompanyCommand, IResponseWrapper>
    {
        private readonly IEmployeService _service;

        public GetCompanyByEmployeeIdCommandHandler(IEmployeService service)
        {
            _service = service;
        }

        public async Task<IResponseWrapper> Handle(GetAllEmployeesFromCompanyCommand request, CancellationToken cancellationToken)
        {
            return await _service.GetAllEmployeesFromCompany(request.idCompany);
        }
    }
}
