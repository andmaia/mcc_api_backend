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

    public class GetAllDisableEmployeesFromCompanyCommand : IRequest<IResponseWrapper>
    {
        public string CompanyId { get; set; }
    }

    public class GetAllDisableEmployeesFromCompanyCommandHandler : IRequestHandler<GetAllDisableEmployeesFromCompanyCommand, IResponseWrapper>
    {
        private readonly IEmployeService _service;

        public GetAllDisableEmployeesFromCompanyCommandHandler(IEmployeService service)
        {
            _service = service;
        }

        public async Task<IResponseWrapper> Handle(GetAllDisableEmployeesFromCompanyCommand request, CancellationToken cancellationToken)
        {
            return await _service.GetAllDisableEmployeesFromCompany(request.CompanyId);
        }
    }
}
