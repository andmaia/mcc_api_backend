using Common.requests.identity;

namespace Test.Builders
{
    public class UserPreRegistrationRequestBuilder
    {
        private string _email = "default@gmail.com";
        private string _role = "Basic";
        private string _userName = "default_username";

        public UserPreRegistrationRequestBuilder WithEmail(string email)
        {
            _email = email;
            return this;
        }

        public UserPreRegistrationRequestBuilder WithRole(string role)
        {
            _role = role;
            return this;
        }

        public UserPreRegistrationRequestBuilder WithUserName(string userName)
        {
            _userName = userName;
            return this;
        }

        public UserPreRegistrationRequest Build()
        {
            return new UserPreRegistrationRequest
            {
                Email = _email,
                Role = _role,
                UserName = _userName
            };
        }
    }
}
