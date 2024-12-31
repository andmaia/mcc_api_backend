using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.requests.branch
{
    public class CreateBranchRequest
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string companyId { get; set; }
    }
}
