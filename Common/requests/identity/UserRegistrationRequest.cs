using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Responses.identity;

public class UserRegistrationRequest
{
    public string Email { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string ComfirmPassword { get; set; }
    public string PhoneNumber { get; set; }
    public bool ActivateUser { get; set; }
    public bool AutoComfirmEmail { get; set; }
}
