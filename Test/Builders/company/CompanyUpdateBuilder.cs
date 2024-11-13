using Common.requests.company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Builders.company
{
    public class CompanyUpdateBuilder
    {
        private readonly CompanyUpdate _companyUpdate;

        public CompanyUpdateBuilder()
        {
            _companyUpdate = new CompanyUpdate
            {
                Name = "Default Company Name",
                CNPJ = "00000000000000"                
            };
        }

        public CompanyUpdateBuilder WithName(string name)
        {
            _companyUpdate.Name = name;
            return this;
        }

        public CompanyUpdateBuilder WithCNPJ(string cnpj)
        {
            _companyUpdate.CNPJ = cnpj;
            return this;
        }

        public CompanyUpdate Build()
        {
            return _companyUpdate;
        }
    }
}
