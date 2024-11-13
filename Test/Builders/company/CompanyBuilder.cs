using Domain.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Builders.company
{
    public class CompanyBuilder
    {
        private readonly Domain.models.Company _company;
        public CompanyBuilder()
        {
            _company = new Domain.models.Company
            {
                Id = Guid.NewGuid().ToString(),         // ID padrão gerado como GUID
                Name = "Default Company Name",          // Nome padrão
                CNPJ = "00000000000000",                // CNPJ padrão sem pontuação
                UserId = Guid.NewGuid().ToString()      // UserId padrão gerado como UUID (GUID)
            };
        }

        public CompanyBuilder WithId(string id)
        {
            _company.Id = id;
            return this;
        }

        public CompanyBuilder WithName(string name)
        {
            _company.Name = name;
            return this;
        }

        public CompanyBuilder WithCNPJ(string cnpj)
        {
            _company.CNPJ = cnpj;
            return this;
        }

        public CompanyBuilder WithUserId(string userId)
        {
            _company.UserId = userId;
            return this;
        }

        public Domain.models.Company Build()
        {
            return _company;
        }
    }
}
