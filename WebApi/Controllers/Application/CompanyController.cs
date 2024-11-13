using Application.Feature.Application.Company.Commands;
using Application.Feature.Application.Company.Queries;
using Application.Feature.Identity.Commands;
using Common.Authorization;
using Common.requests.company;
using Common.requests.identity;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using WebApi.Attributes;
using WebApi.Controllers.Identity;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace WebApi.Controllers.Application
{
    [Route("api/[controller]")]
    public class CompanyController : MyBaseController<CompanyController>
    {
        [MustHavePermission(AppFeature.Companies, AppAction.Create)]
        [HttpPost("/register")]
        public async Task<IActionResult> RegisterCompany([FromBody] CompanyRequest companyRequest)
        {
            var response = await MediatorSender.Send(new CreateCompanyCommand { CompanyRequest = companyRequest });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [MustHavePermission(AppFeature.Companies, AppAction.Update)]
        [HttpPut("/update")]
        public async Task<IActionResult> UpdateCompany([FromBody] CompanyUpdate companyRequest)
        {
            var response = await MediatorSender.Send(new UpdateCompanyCommand { CompanyRequest = companyRequest });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [MustHavePermission(AppFeature.Companies, AppAction.Read)]
        [HttpGet("get-company/{id}")]
        public async Task<IActionResult> GetCompanyById([FromRoute]string id)
        {
            var response = await MediatorSender.Send(new GetCompanyByIdCommand { Id = id });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }


    }
}
