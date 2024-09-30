using Application.Feature.Identity.Commands;
using Application.Feature.Identity.Queries;
using Common.Authorization;
using Common.requests.identity;
using Common.Responses.identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Attributes;

namespace WebApi.Controllers.Identity
{

    [Route("api/[controller]")]
    public class UserController:MyBaseController<UserController>
    {
        [AllowAnonymous]

        [HttpPost("/company")]
        public async Task<IActionResult> RegisterUserCompany([FromBody] UserRegistrationRequest userRegistration)
        {
            var response = await MediatorSender.Send(new UserRegistrationCommand { UserRegistrationRequest = userRegistration });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [MustHavePermission(AppFeature.Users,AppAction.Create)]
        [HttpPost("/pre-register")]
        public async Task<IActionResult> PreRegisterUser([FromBody] UserPreRegistrationRequest userPreRegistrationRequest)
        {
            var response = await MediatorSender.Send(new PreUserRegistrationCommand {Request = userPreRegistrationRequest });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [AllowAnonymous]
        [HttpPost("/finish-register")]
        public async Task<IActionResult> FinishRegisterUser([FromBody] UserRegistrationRequest userRegistrationRequest)
        {
            var response = await MediatorSender.Send(new FinishPreUserRegistrationCommand {Request = userRegistrationRequest  });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }



        [MustHavePermission(AppFeature.Users, AppAction.Read)]
        [HttpGet("/{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var response = await MediatorSender.Send(new GetUserByIdQuery { Id = id });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

    }
}
