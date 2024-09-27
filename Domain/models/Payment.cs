using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.models
{
    public class Payment
    {
        public string Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? PaymentDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public bool IsActive { get; set; }
        public string PaymentFormId { get; set; }
        public PaymentForm PaymentForm { get; set; }
        public Order Order { get; set; }
        public string OrderId { get; set; }
        public decimal Tax { get; set; }
        public string Url { get; set; }
        public decimal Discount { get; set; }
        public decimal Amount { get; set; }
 
    }
}
