using Common.Responses.Employee;
using Common.Responses.Paymenet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Responses.order
{
    public class ResponseOrderToComission
    {
    
            public string Id { get; set; }
            public string CustomerName { get; set; }
            public DateTime CreationDate { get; set; }
            public string PaymentOrderStatus { get; set; }
            public string CommissionOrderStatus { get; set; }
            public decimal TotalValue { get; set; }
            public decimal TotalDiscount { get; set; }
            public List<ResponsePaymentToComission> Payments { get; set; }
   
    }
}
