using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.requests.identity
{
    public class UpdateEmailRequest
    {
        public string Id { get; set; }
        public string OldEmail { get; set; }
        public string NewEmail { get; set; }
    }
}
