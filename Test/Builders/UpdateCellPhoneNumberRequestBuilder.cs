using Common.requests.identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Builders
{
    public class UpdateCellPhoneNumberRequestBuilder
    {
        private string _id = Guid.NewGuid().ToString();
        private string _phoneNumber = "11987654321";  
        private string _oldPhoneNumber = "11912345678"; 

        public UpdateCellPhoneNumberRequestBuilder WithId(string id)
        {
            _id = id;
            return this;
        }

        public UpdateCellPhoneNumberRequestBuilder WithPhoneNumber(string phoneNumber)
        {
            _phoneNumber = phoneNumber;
            return this;
        }

        public UpdateCellPhoneNumberRequestBuilder WithOldPhoneNumber(string oldPhoneNumber)
        {
            _oldPhoneNumber = oldPhoneNumber;
            return this;
        }

        public UpdateCellPhoneNumberRequest Build()
        {
            return new UpdateCellPhoneNumberRequest
            {
                Id = _id,
                PhoneNumber = _phoneNumber,
                OldPhoneNumber = _oldPhoneNumber
            };
        }
    }

}
