using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.requests.identity
{
    public class UpdateUserNameRequest
    {
        public string Id { get; set; }
        public string OldUserName { get; set; }
        public string NewUserName { get; set; }
    }
}
