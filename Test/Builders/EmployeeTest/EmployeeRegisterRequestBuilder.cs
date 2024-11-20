using Common.requests.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Builders.EmployeeTest
{
    public class EmployeeRegisterRequestBuilder
    {
        private float _comissionPercentage = 10;
        private string _position = "Receptionist"; // Valor padrão para o position
        private string _companyId = Guid.NewGuid().ToString();
        private string _userId = Guid.NewGuid().ToString();

        public EmployeeRegisterRequest Build()
        {
            return new EmployeeRegisterRequest
            {
                ComissionPercentage = _comissionPercentage,
                Position = _position,
                CompanyId = _companyId,
                UserId = _userId
            };
        }

        public EmployeeRegisterRequestBuilder WithComissionPercentage(float comissionPercentage)
        {
            _comissionPercentage = comissionPercentage;
            return this;
        }

        public EmployeeRegisterRequestBuilder WithPosition(string position)
        {
            _position = position;
            return this;
        }

        public EmployeeRegisterRequestBuilder WithCompanyId(string companyId)
        {
            _companyId = companyId;
            return this;
        }

        public EmployeeRegisterRequestBuilder WithUserId(string userId)
        {
            _userId = userId;
            return this;
        }
    }

}
