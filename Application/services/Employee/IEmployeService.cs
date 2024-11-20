using Common.requests.Employee;
using Common.Responses.wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.services.Employee
{
    public interface IEmployeService
    {
        Task<IResponseWrapper> RegisterEmployee(EmployeeRegisterRequest request);
        Task<IResponseWrapper> FinishRegisterEmployee(FinishRegisterEmployee request);
        Task<IResponseWrapper> UpdateEmployeeToCompany(UpdateEmployeeToCompany request);
        Task<IResponseWrapper> UpdateEmployee(UpdateEmployee request);
        Task<IResponseWrapper> GetAllEmployeesFromCompany(string companyId);
        Task<IResponseWrapper> GetAllEnableEmployeesFromCompany(string companyId);
        Task<IResponseWrapper> GetAllDisableEmployeesFromCompany(string companyId);
        Task<IResponseWrapper> GetEmployeeByUserId(string userId, string companyId);
        Task<IResponseWrapper> GetEmployeeByEmail(string email, string companyId);
        Task<IResponseWrapper> GetEmployeeById(string id, string companyId);
        Task<IResponseWrapper> DisableEmployeeById(string Employeeid, string companyId);
        Task<IResponseWrapper> EnableEmployeeById(string Employeeid, string companyId);



    }
}
