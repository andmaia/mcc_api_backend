using Domain.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Builders.paymentForm
{
    public class PaymentFormBuilder
    {
        private PaymentForm _paymentForm;

        public PaymentFormBuilder()
        {
            _paymentForm = new PaymentForm
            {
                Id = Guid.NewGuid().ToString(),
                Tax = 0.0m,
                Name = "Default Name",
                CreationDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                CompletionDate = null,
                IsActive = true,
       
                CompanyId = Guid.NewGuid().ToString(),
            };
        }

        public PaymentFormBuilder WithId(string id)
        {
            _paymentForm.Id = id;
            return this;
        }

        public PaymentFormBuilder WithTax(decimal tax)
        {
            _paymentForm.Tax = tax;
            return this;
        }

        public PaymentFormBuilder WithName(string name)
        {
            _paymentForm.Name = name;
            return this;
        }

        public PaymentFormBuilder WithCreationDate(DateTime creationDate)
        {
            _paymentForm.CreationDate = creationDate;
            return this;
        }

        public PaymentFormBuilder WithUpdatedDate(DateTime? updatedDate)
        {
            _paymentForm.UpdatedDate = updatedDate;
            return this;
        }

        public PaymentFormBuilder WithCompletionDate(DateTime? completionDate)
        {
            _paymentForm.CompletionDate = completionDate;
            return this;
        }

        public PaymentFormBuilder WithIsActive(bool isActive)
        {
            _paymentForm.IsActive = isActive;
            return this;
        }

       
        public PaymentFormBuilder WithCompanyId(string companyId)
        {
            _paymentForm.CompanyId = companyId;
            return this;
        }

       
        public PaymentForm Build()
        {
            return _paymentForm;
        }
    }

   
}
