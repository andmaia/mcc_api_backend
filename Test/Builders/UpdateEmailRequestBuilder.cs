using Common.requests.identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Builders
{
    public class UpdateEmailRequestBuilder
    {
        private string _id = Guid.NewGuid().ToString(); // Gerando um GUID como string de 36 caracteres
        private string _oldEmail = "oldemail@example.com"; // Email antigo padrão
        private string _newEmail = "newemail@example.com"; // Novo email padrão

        public UpdateEmailRequestBuilder WithId(string id)
        {
            _id = id;
            return this;
        }

        public UpdateEmailRequestBuilder WithOldEmail(string oldEmail)
        {
            _oldEmail = oldEmail;
            return this;
        }

        public UpdateEmailRequestBuilder WithNewEmail(string newEmail)
        {
            _newEmail = newEmail;
            return this;
        }

        public UpdateEmailRequest Build()
        {
            return new UpdateEmailRequest
            {
                Id = _id,
                OldEmail = _oldEmail,
                NewEmail = _newEmail
            };
        }
    }
}
