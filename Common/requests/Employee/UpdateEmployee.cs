using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.requests.Employee
{
    public class UpdateEmployee
    {
        public string IdEmployee { get; set; }
        public string Name { get; set; }
        public string CPF { get; set; }
        public string UrlPerfil { get; set; }
        public string IdUser { get; set; }

    }

}

