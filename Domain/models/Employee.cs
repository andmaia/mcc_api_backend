using Domain.enums;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Domain.models
{
    public class Employee
    {


        public string Id { get; set; }
        public string Name { get; set; }
        public string CPF { get; set; }
        public float ComissionPercentage { get; set; }
        public Position Position { get; set; }
        public string UrlPerfil { get; set; }
        public bool IsActive { get; set; }
        public bool RegistrationFinish { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime FinishedDate { get; set; }
        public Company Company { get; set; }
        public string CompanyId { get; set; }

        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
        public IEnumerable<Expense> Expenses { get; set; }
        public IEnumerable<Order> Orders { get; set; }
        public IEnumerable<Comission> Comissions { get; set; }

    }
}
