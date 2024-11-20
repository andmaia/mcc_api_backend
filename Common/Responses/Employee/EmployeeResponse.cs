using Common.Responses.identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Responses.Employee
{
    public class EmployeeResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string CPF { get; set; }
        public float ComissionPercentage { get; set; }
        public string Position { get; set; }
        public string UrlPerfil { get; set; }
        public bool IsActive { get; set; }

        public string CompanyName { get; set; }
        public UserResponse UserResponse { get; set; }
    }
}
