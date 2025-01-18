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
    }

}