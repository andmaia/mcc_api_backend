using Domain.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.models
{
    public class Order
    {
        public string Id { get; set; }
        public string CustomerName { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public StatusPayment PaymentOrderStatus { get; set; }
        public StatusPayment CommissionOrderStatus { get; set; }
        public decimal TotalValue { get; set; }
        public decimal CommissionPercentage { get; set; }
        public decimal TotalDiscount { get; set; }
        public Company Company { get; set; }
        public string CompanyId { get; set; }

        public Employee Employee { get; set; }
        public string EmployeeId { get; set; }
        public Comission Comission { get; set; }
        public string ComissionId { get; set; }
        public  IEnumerable<Payment> Payments { get; set; } = new List<Payment>();
    }
}
