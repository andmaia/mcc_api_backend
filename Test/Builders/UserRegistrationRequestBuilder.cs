using Common.Responses.identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Builders
{
    public class UserRegistrationRequestBuilder
    {
        private readonly UserRegistrationRequest _request;

        public UserRegistrationRequestBuilder()
        {
            _request = new UserRegistrationRequest
            {
                Email = "default@example.com",
                UserName = "defaultUser",
                Password = "defaultPassword123",
                ComfirmPassword = "defaultPassword123",
                PhoneNumber = "1234567890",
       
            };
        }

        public UserRegistrationRequestBuilder WithEmail(string email)
        {
            _request.Email = email;
            return this;
        }

        public UserRegistrationRequestBuilder WithUserName(string userName)
        {
            _request.UserName = userName;
            return this;
        }

        public UserRegistrationRequestBuilder WithPassword(string password)
        {
            _request.Password = password;
            return this;
        }

        public UserRegistrationRequestBuilder WithConfirmPassword(string confirmPassword)
        {
            _request.ComfirmPassword = confirmPassword;
            return this;
        }

        public UserRegistrationRequestBuilder WithPhoneNumber(string phoneNumber)
        {
            _request.PhoneNumber = phoneNumber;
            return this;
        }

    
        public UserRegistrationRequest Build()
        {
            return _request;
        }
    }

}
