using Domain.enums;
using Domain.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Builders.Expense
{
    public class ExpenseBuilder
    {
        private Domain.models.Expense _expense;

        public ExpenseBuilder()
        {
            _expense = new Domain.models.Expense
            {
                Id = Guid.NewGuid().ToString(),
                Value = 0.0m,
                CreationDate = DateTime.Now,
                UpdatedDate = DateTime.MinValue,
                CompletionDate = DateTime.MinValue,
                Description = string.Empty,
                Url = string.Empty,
                StatusPayment = StatusPayment.PENDING, // Default status
                IsActive = true,
                Company = null,
                EmployeeId = Guid.NewGuid().ToString(),
                Employee = null,
                ComissionId = null,
                Comission = null,
                CompanyId = Guid.NewGuid().ToString()
            };
        }

        public ExpenseBuilder WithId(string id)
        {
            _expense.Id = id;
            return this;
        }

        public ExpenseBuilder WithValue(decimal value)
        {
            _expense.Value = value;
            return this;
        }

        public ExpenseBuilder WithCreationDate(DateTime creationDate)
        {
            _expense.CreationDate = creationDate;
            return this;
        }

        public ExpenseBuilder WithUpdatedDate(DateTime? updatedDate)
        {
            _expense.UpdatedDate = updatedDate ?? DateTime.MinValue;
            return this;
        }

        public ExpenseBuilder WithCompletionDate(DateTime? completionDate)
        {
            _expense.CompletionDate = completionDate ?? DateTime.MinValue;
            return this;
        }

        public ExpenseBuilder WithDescription(string description)
        {
            _expense.Description = description;
            return this;
        }

        public ExpenseBuilder WithUrl(string url)
        {
            _expense.Url = url;
            return this;
        }

        public ExpenseBuilder WithStatusPayment(StatusPayment status)
        {
            _expense.StatusPayment = status;
            return this;
        }

        public ExpenseBuilder WithIsActive(bool isActive)
        {
            _expense.IsActive = isActive;
            return this;
        }

       

        public ExpenseBuilder WithEmployeeId(string employeeId)
        {
            _expense.EmployeeId = employeeId;
            return this;
        }

      

        public ExpenseBuilder WithComissionId(string comissionId)
        {
            _expense.ComissionId = comissionId;
            return this;
        }

      

        public ExpenseBuilder WithCompanyId(string companyId)
        {
            _expense.CompanyId = companyId;
            return this;
        }

        public Domain.models.Expense Build()
        {
            return _expense;
        }
    }
}
