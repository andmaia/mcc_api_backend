using Domain.enums;
using Domain.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.requests.Comission
{
    public class CreateComissionRequest
    {
        public DateTime CommissionStartDate { get; set; }
        public DateTime CommissionEndDate { get; set; }

        public bool ApplyDiscount { get; set; }
        public decimal Percentage { get; set; }
        public string Url { get; set; }
        public string EmployeeId { get; set; }

        public ICollection<string> Orders { get; set; }
        public ICollection<string> Expenses { get; set; }
    }

}

