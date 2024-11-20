using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.requests.Employee
{
    public class UpdateEmployeeToCompany
    {
        public float ComissionPercentage { get; set; }
        public string Position { get; set; }
        public string CompanyId { get; set; }
        public string UserId { get; set; }
        public string EmployeeId { get; set; }

    }
}
