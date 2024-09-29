using Common.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Permissions
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PeermissionRequirement>
    {
        public PermissionAuthorizationHandler() { }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PeermissionRequirement requirement)
        {
            if (context.User is null)
            {
                await Task.CompletedTask;
            }
            var perssions = context.User.Claims.
                Where(claim => claim.Type == AppClaim.Permission &&
                claim.Value == requirement.Permission &&
                claim.Issuer == "LOCAL AUTHORITY");
            if (perssions.Any())
            {
                context.Succeed(requirement);
            }
        }
    }

}
