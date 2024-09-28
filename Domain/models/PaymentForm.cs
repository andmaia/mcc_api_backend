using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.models
{
    public class PaymentForm
    {
     
        public string Id { get; set; }
        public decimal Tax { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public bool IsActive { get; set; }

        public Company Company { get; set; }
        public string CompanyId { get; set; }
        public IEnumerable<Payment> Payments { get; set; } = new List<Payment>();
    }
}
