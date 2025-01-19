using Application.Feature.branch.Commands;
using Application.Feature.Comission.Command;
using Application.Feature.Comission.Query;
using Common.Authorization;
using Common.requests.branch;
using Common.requests.Comission;
using Microsoft.AspNetCore.Mvc;
using WebApi.Attributes;

namespace WebApi.Controllers.Application
{

    [Route("api/comission/")]
    public class ComissionController : MyBaseController<ComissionController>
    {
        [MustHavePermission(AppFeature.Commissions, AppAction.Create)]
        [HttpPost("create/")]
        public async Task<IActionResult> CreateComission([FromBody] CreateComissionRequest createComissionRequest)
        {
            var response = await MediatorSender.Send(new CreateComissionCommand { Request = createComissionRequest });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [MustHavePermission(AppFeature.Commissions, AppAction.Read)]
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetComissionById([FromRoute] string Id)
        {
            var response = await MediatorSender.Send(new GetComissionByIdQuery { Id = Id });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [MustHavePermission(AppFeature.Commissions, AppAction.Read)]
        [HttpPost("{Take}/{Page}/user")]
        public async Task<IActionResult> GetComissionByUserId([FromBody] ComissionFilterRequest request, int Take, int Page)
        {
            var response = await MediatorSender.Send(new GetComissionByUserQuery { ComissionFilterRequest = request, page = Page, take = Take });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }


        [MustHavePermission(AppFeature.Commissions, AppAction.Read)]
        [HttpPost("{Take}/{Page}/company")]
        public async Task<IActionResult> GetComissionByCompanyId([FromBody] ComissionFilterRequest request, int Take, int Page)
        {
            var response = await MediatorSender.Send(new GetComissionByCompanyQuery { ComissionFilterRequest = request, page = Page, take = Take });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [MustHavePermission(AppFeature.Commissions, AppAction.Read)]
        [HttpPost("{Take}/{Page}/employee")]
        public async Task<IActionResult> GetComissionByEmployeeId([FromBody] ComissionFilterRequest request, int Take, int Page)
        {
            var response = await MediatorSender.Send(new GetComissionByEmployeeQuery { ComissionFilterRequest = request, page = Page, take = Take });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [MustHavePermission(AppFeature.Commissions, AppAction.Delete)]
        [HttpDelete("{Id}/disable")]
        public async Task<IActionResult> DisableComissionById([FromRoute] string Id)
        {
            var response = await MediatorSender.Send(new DeleteComissionCommand { Id = Id });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [MustHavePermission(AppFeature.Commissions, AppAction.Update)]
        [HttpPut("{Id}/pay")]
        public async Task<IActionResult> PayComissionById([FromRoute] string Id)
        {
            var response = await MediatorSender.Send(new PayComissionCommand { Id = Id });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [MustHavePermission(AppFeature.Commissions, AppAction.Update)]
        [HttpPut("{Id}/unpay")]
        public async Task<IActionResult> unPayComissionById([FromRoute] string Id)
        {
            var response = await MediatorSender.Send(new UnPayComissionCommand { Id = Id });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }

}