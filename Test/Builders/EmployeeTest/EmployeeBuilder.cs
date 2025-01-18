using Domain.enums;
using Domain.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Builders.EmployeeTest
{
    public class EmployeeBuilder
    {
        private string _id = Guid.NewGuid().ToString();
        private string _name = "Test Employee";
        private string _cpf = "12345678900";
        private float _comissionPercentage = 10;
        private Position _position = Position.Receptionist;
        private string _urlPerfil = "https://example.com/profile.jpg";
        private bool _isActive = true;
        private string _companyId = Guid.NewGuid().ToString();
        private string _userId = Guid.NewGuid().ToString();

        public Employee Build()
        {
            return new Employee
            {
                Id = _id,
                Name = _name,
                CPF = _cpf,
                ComissionPercentage = _comissionPercentage,
                Position = _position,
                UrlPerfil = _urlPerfil,
                IsActive = _isActive,
                CompanyId = _companyId,
                UserId = _userId,
                Expenses = new List<Domain.models.Expense>(),
                Orders = new List<Order>(),
                Comissions = new List<Domain.models.Comission>()
            };
        }

        public EmployeeBuilder WithId(string id)
        {
            _id = id;
            return this;
        }

        public EmployeeBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public EmployeeBuilder WithCPF(string cpf)
        {
            _cpf = cpf;
            return this;
        }

        public EmployeeBuilder WithComissionPercentage(float comissionPercentage)
        {
            _comissionPercentage = comissionPercentage;
            return this;
        }

        public EmployeeBuilder WithPosition(Position position)
        {
            _position = position;
            return this;
        }

        public EmployeeBuilder WithUrlPerfil(string urlPerfil)
        {
            _urlPerfil = urlPerfil;
            return this;
        }

        public EmployeeBuilder WithIsActive(bool isActive)
        {
            _isActive = isActive;
            return this;
        }

        public EmployeeBuilder WithCompanyId(string companyId)
        {
            _companyId = companyId;
            return this;
        }

        public EmployeeBuilder WithUserId(string userId)
        {
            _userId = userId;
            return this;
        }

        public EmployeeBuilder WithExpenses(IEnumerable<Domain.models.Expense> expenses)
        {
            return this;
        }

        public EmployeeBuilder WithOrders(IEnumerable<Order> orders)
        {
            return this;
        }

        public EmployeeBuilder WithComissions(IEnumerable<Domain.models.Comission> comissions)
        {
            return this;
        }
    }
}
