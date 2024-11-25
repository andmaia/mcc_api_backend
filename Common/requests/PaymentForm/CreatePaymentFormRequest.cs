using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.requests.PaymentForm
{
    public class CreatePaymentFormRequest
    {
        public decimal Tax { get; set; }
        public string Name { get; set; }
        public string CompanyId { get; set; }
    }
}
