using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.models
{
    public class Company
    {
      

        public string Id { get; set; }
        public string Name { get; set; }
        public string CNPJ { get; set; }

        public IEnumerable<Employee> Employees { get; set; } 
        public IEnumerable<PaymentForm> PaymentForms { get; set; }
        public IEnumerable<Expense> Expenses { get; set; }
        public IEnumerable<Order> Orders { get; set; }
        public IEnumerable<Comission> Comissions { get; set; }

    }
}
