using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.requests.identity
{
    public class UpdateCellPhoneNumberRequest
    {
        public string Id { get; set; }
        public string PhoneNumber { get; set; }
        public string OldPhoneNumber { get; set; }
    }
}
