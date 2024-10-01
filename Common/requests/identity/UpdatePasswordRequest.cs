using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.requests.identity
{
    public class UpdatePasswordRequest
    {
        public string Id { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmationPassword { get; set; }

        public string Email { get; set; }
    }
}
