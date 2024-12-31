using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.requests.Order
{
    public class UpdateOrderRequest
    {
        public string Id { get; set; }
       
       
        public decimal? TotalValue { get; set; }
        public decimal? CommissionPercentage { get; set; }
        public string branchId { get; set; }
        public string EmployeeId { get; set; }


    }
}
