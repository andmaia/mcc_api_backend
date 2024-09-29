using Application.Feature.Identity.Commands;
using Common.Responses.identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Identity
{

    [Route("api/[controller]")]
    public class UserController:MyBaseController<UserController>
    {
        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationRequest userRegistration)
        {
            var response = await MediatorSender.Send(new UserRegistrationCommand { UserRegistrationRequest = userRegistration });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

    }
}
