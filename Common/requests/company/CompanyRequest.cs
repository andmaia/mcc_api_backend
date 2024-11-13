using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.requests.company
{
    public class CompanyRequest
    {
        public string Name { get; set; }
        public string CNPJ { get; set; }

        public string UserId { get; set; }
    }
}
