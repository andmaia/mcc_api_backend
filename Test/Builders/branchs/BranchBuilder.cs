using Domain.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Builders.branchs
{
    public class BranchBuilder
    {
        private string _id = Guid.NewGuid().ToString();
        private string _name = "Default Branch Name";
        private bool _isActive = true;
        private DateTime _createDate = DateTime.UtcNow;
        private DateTime _finishDate = DateTime.MinValue;
        private string _companyId= Guid.NewGuid().ToString();


        public BranchBuilder WithId(string id)
        {
            _id = id;
            return this;
        }

        public BranchBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public BranchBuilder WithIsActive(bool isActive)
        {
            _isActive = isActive;
            return this;
        }

        public BranchBuilder WithCreateDate(DateTime createDate)
        {
            _createDate = createDate;
            return this;
        }

        public BranchBuilder WithFinishDate(DateTime finishDate)
        {
            _finishDate = finishDate;
            return this;
        }

  
        public BranchBuilder WithCompanyId(string companyId)
        {
            _companyId = companyId;
            return this;
        }

        public Branch Build()
        {
            return new Branch
            {
                Id = _id,
                Name = _name,
                IsActive = _isActive,
                CreateDate = _createDate,
                FinishDate = _finishDate,
                companyId = _companyId
            };
        }
    }

}
