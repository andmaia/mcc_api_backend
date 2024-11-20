using Application.Feature.Application.Company.Commands;
using Application.Feature.Application.Company.Queries;
using Application.Feature.Employee.Commands;
using Application.Feature.Employee.Queries;
using Common.Authorization;
using Common.requests.company;
using Common.requests.Employee;
using Microsoft.AspNetCore.Mvc;
using WebApi.Attributes;

namespace WebApi.Controllers.Application
{
    [Route("api/employee/")]
    public class EmployeeController : MyBaseController<EmployeeController>
    {

        [MustHavePermission(AppFeature.Companies, AppAction.Create)]
        [HttpPost("register-employee")]
        public async Task<IActionResult> RegisterEmployee([FromBody] EmployeeRegisterRequest employeeRegisterRequest)
        {
            var response = await MediatorSender.Send(new RegisterEmployeeCommand { employeeRegisterRequest = employeeRegisterRequest });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [MustHavePermission(AppFeature.Employees, AppAction.Create)]
        [HttpPost("finish-register-employee")]
        public async Task<IActionResult> FinishRegisterEmployee([FromBody] FinishRegisterEmployee finishRegisterEmployee)
        {
            var response = await MediatorSender.Send(new FinishRegisterEmoloyeeCommand { finishRegisterEmployee = finishRegisterEmployee });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [MustHavePermission(AppFeature.Companies, AppAction.Update)]
        [HttpPut("update/company")]
        public async Task<IActionResult> UpdateFromCompanyEmployee([FromBody] UpdateEmployeeToCompany updateEmployeeToCompany)
        {
            var response = await MediatorSender.Send(new UpdateEmployeeToCompanyCommand { updateEmployeeToCompany = updateEmployeeToCompany });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [MustHavePermission(AppFeature.Employees, AppAction.Update)]
        [HttpPut("update/")]
        public async Task<IActionResult> UpdateEmployee([FromBody] UpdateEmployee updateEmployee)
        {
            var response = await MediatorSender.Send(new UpdateEmployeeCommand { updateEmployee = updateEmployee });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }


        [MustHavePermission(AppFeature.Employees, AppAction.Delete)]
        [HttpDelete("{id}/company/{companyId}/disable")]
        public async Task<IActionResult> DisableEmployeeById([FromRoute] string id, string companyId)
        {
            var response = await MediatorSender.Send(new DisableEmployeeByIdCommand { EmployeeId = id, CompanyId = companyId });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [MustHavePermission(AppFeature.Employees, AppAction.Read)]
        [HttpGet("by-id/{id}/company/{companyId}")] // Rota única para ID
        public async Task<IActionResult> GetEmployeeById([FromRoute] string id, [FromRoute] string companyId)
        {
            var response = await MediatorSender.Send(new GetEmployeeByIdCommand { Id = id, CompanyId = companyId });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [MustHavePermission(AppFeature.Employees, AppAction.Read)]
        [HttpGet("by-email/{email}/company/{companyId}")] // Rota única para Email
        public async Task<IActionResult> GetEmployeeByEmail([FromRoute] string email, [FromRoute] string companyId)
        {
            var response = await MediatorSender.Send(new GetEmployeeByEmailCommand { Email = email, CompanyId = companyId });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [MustHavePermission(AppFeature.Employees, AppAction.Read)]
        [HttpGet("by-user-id/{userId}/company/{companyId}")] // Rota única para UserId
        public async Task<IActionResult> GetEmployeeByUserId([FromRoute] string userId, [FromRoute] string companyId)
        {
            var response = await MediatorSender.Send(new GetEmployeeByUserIdCommand { UserId = userId, CompanyId = companyId });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }


        [MustHavePermission(AppFeature.Employees, AppAction.Delete)]
        [HttpDelete("{id}/company/{companyId}/enable")]
        public async Task<IActionResult> EnableEmployeeById([FromRoute] string id, string companyId)
        {
            var response = await MediatorSender.Send(new EnableEmployeeByIdCommand { EmployeeId = id, CompanyId = companyId });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [MustHavePermission(AppFeature.Employees, AppAction.Read)]
        [HttpGet("company/{companyId}/all")]
        public async Task<IActionResult> GetAllEmployeeById([FromRoute] string companyId)
        {
            var response = await MediatorSender.Send(new GetAllEmployeesFromCompanyCommand { idCompany = companyId });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        [MustHavePermission(AppFeature.Employees, AppAction.Read)]
        [HttpGet("company/{companyId}/enable")]
        public async Task<IActionResult> GetAllEnableEmployeeById([FromRoute] string companyId)
        {
            var response = await MediatorSender.Send(new GetAllEnableEmployeesFromCompanyCommand { idCompany = companyId });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        [MustHavePermission(AppFeature.Employees, AppAction.Read)]
        [HttpGet("company/{companyId}/disable")]
        public async Task<IActionResult> GetAllDisableEmployeeById([FromRoute] string companyId)
        {
            var response = await MediatorSender.Send(new GetAllDisableEmployeesFromCompanyCommand { CompanyId = companyId });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }


    }
}
