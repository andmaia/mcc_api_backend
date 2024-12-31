using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Responses.order
{
    public class ResponseOrder
    {
     
            public string Id { get; set; }
            public string CustomerName { get; set; }
            public bool IsActive { get; set; }
            public DateTime CreationDate { get; set; }
            public DateTime? UpdatedDate { get; set; }
            public DateTime? CompletionDate { get; set; }
            public string PaymentOrderStatus { get; set; }
            public String CommissionOrderStatus { get; set; }
            public decimal TotalValue { get; set; }
            public decimal CommissionPercentage { get; set; }
            public decimal TotalDiscount { get; set; }
            public string CompanyId { get; set; }
            public string branchId { get; set; }
            public string EmployeeId { get; set; }
            public string ComissionId { get; set; }
       
    }
}
