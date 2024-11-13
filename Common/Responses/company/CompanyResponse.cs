using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Responses.Company
{
    public class CompanyResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string CNPJ { get; set; }

        public string UserId { get; set; }
    }
}
