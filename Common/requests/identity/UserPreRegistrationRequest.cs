using Common.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.requests.identity
{
    public class UserPreRegistrationRequest
    {
        public string Email { get; set; }
        public string Role { get;set; } 
        
        public string UserName { get; set; }    
    }
}
