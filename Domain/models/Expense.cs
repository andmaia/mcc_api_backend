using Domain.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.models
{
    public class Expense
    {
        public string Id { get; set; }
        public decimal Value { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public StatusPayment StatusPayment { get; set; }
        public bool IsActive { get; set; }
        public Company Company { get; set; }
        public string EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public string ComissionId { get; set; }
        public Comission Comission { get; set; }
        public string CompanyId { get; set; }
    }
}
