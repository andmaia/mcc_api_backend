using Microsoft.AspNetCore.Authorization;

namespace WebApi.Permissions
{
    public class PeermissionRequirement : IAuthorizationRequirement
    {
        public string Permission { get; set; }

        public PeermissionRequirement(string permission)
        {
            Permission = permission;
        }
    }

}
