using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.requests.company
{
    public class CompanyUpdate
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string CNPJ { get; set; }
    }
}
