using Common.requests.company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Builders.Company
{
    public class CompanyRequestBuilder
    {
        private readonly CompanyRequest _companyRequest;

        public CompanyRequestBuilder()
        {
            _companyRequest = new CompanyRequest
            {
                Name = "Default Company Name",
                CNPJ = "00000000000000",                // CNPJ padrão sem pontos
                UserId = Guid.NewGuid().ToString()      // UserId padrão gerado como UUID
            };
        }

        public CompanyRequestBuilder WithName(string name)
        {
            _companyRequest.Name = name;
            return this;
        }

        public CompanyRequestBuilder WithCNPJ(string cnpj)
        {
            _companyRequest.CNPJ = cnpj;
            return this;
        }

        public CompanyRequestBuilder WithUserId(string userId)
        {
            _companyRequest.UserId = userId;
            return this;
        }

        public CompanyRequest Build()
        {
            return _companyRequest;
        }
    }
}
