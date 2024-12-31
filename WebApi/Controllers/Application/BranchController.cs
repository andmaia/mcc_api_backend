using Application.Feature.Application.Company.Commands;
using Application.Feature.Application.Company.Queries;
using Application.Feature.branch.Commands;
using Application.Feature.branch.Queries;
using Common.Authorization;
using Common.requests.branch;
using Common.requests.company;
using Microsoft.AspNetCore.Mvc;
using WebApi.Attributes;

namespace WebApi.Controllers.Application
{


    [Route("api/branch/")]
    public class BranchController : MyBaseController<BranchController>
    {
        [MustHavePermission(AppFeature.Companies, AppAction.Create)]
        [HttpPost("register-branch/")]
        public async Task<IActionResult> RegisterBranch([FromBody] CreateBranchRequest createBranchRequest)
        {
            var response = await MediatorSender.Send(new CreateBranchCommand { Request = createBranchRequest });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [MustHavePermission(AppFeature.Companies, AppAction.Update)]
        [HttpPut("update-branch/")]
        public async Task<IActionResult> UpdateBranch([FromBody] UpdateBranchRequest updateBranchRequest)
        {
            var response = await MediatorSender.Send(new UpdateBranchCommand { Request = updateBranchRequest });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [MustHavePermission(AppFeature.Companies, AppAction.Update)]
        [HttpPut("disable-branch/{id}")]
        public async Task<IActionResult> DisableBranch([FromRoute] string id)
        {
            var response = await MediatorSender.Send(new DisableBranchCommand { Id = id });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [MustHavePermission(AppFeature.Companies, AppAction.Update)]
        [HttpPut("enable-branch/{id}")]
        public async Task<IActionResult> EnableBranch([FromRoute] string id)
        {
            var response = await MediatorSender.Send(new EnableBranchCommand { Id = id });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [MustHavePermission(AppFeature.Companies, AppAction.Read)]
        [HttpGet("/{id}")]
        public async Task<IActionResult> GetBranchId([FromRoute] string id)
        {
            var response = await MediatorSender.Send(new GetBranchByIdQuery { Id = id });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        [MustHavePermission(AppFeature.Companies, AppAction.Read)]
        [HttpGet("company/{id}")]
        public async Task<IActionResult> GetBranchByCompanyId([FromRoute] string id)
        {
            var response = await MediatorSender.Send(new GetBranchsByCompanyQuery { Id = id });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [MustHavePermission(AppFeature.Companies, AppAction.Read)]
        [HttpGet("orders/{id}")]
        public async Task<IActionResult> GetOrdersByBranchId([FromRoute] string id)
        {
            var response = await MediatorSender.Send(new GetOrdersByBranchQuery { Id = id });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
