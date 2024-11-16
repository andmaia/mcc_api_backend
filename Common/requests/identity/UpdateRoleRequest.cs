using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.requests.identity
{
    public class UpdateRoleRequest
    {
        public string IdUserToUpdate { get; set; }
        public string RoleOld { get; set; }
        public string RoleNew { get; set; }
    }
}
