using Infrastructure.Models;
using System;

namespace Test.Builders
{
    public class ApplicationUserBuilder
    {
        // Valores padrão
        private string _userName = "DefaultUser";
        private string _email = "default@example.com";
        private string _passwordHash = "default_password_hash";
        private string _phoneNumber = "0000000000";
        private bool _isActive = true; // Iniciado como ativo por padrão
        private string _refreshToken = string.Empty; // Deve ser definido explicitamente
        private DateTime _refreshTokenExpiryDate = DateTime.MinValue; // Deve ser definido explicitamente

        public ApplicationUserBuilder WithUserName(string userName)
        {
            _userName = userName;
            return this;
        }

        public ApplicationUserBuilder WithEmail(string email)
        {
            _email = email;
            return this;
        }

        public ApplicationUserBuilder WithPasswordHash(string passwordHash)
        {
            _passwordHash = passwordHash;
            return this;
        }

        public ApplicationUserBuilder WithPhoneNumber(string phoneNumber)
        {
            _phoneNumber = phoneNumber;
            return this;
        }

        public ApplicationUserBuilder WithIsActive(bool isActive)
        {
            _isActive = isActive;
            return this;
        }

        public ApplicationUserBuilder WithRefreshToken(string refreshToken)
        {
            _refreshToken = refreshToken;
            return this;
        }

        public ApplicationUserBuilder WithRefreshTokenExpiryDate(DateTime expiryDate)
        {
            _refreshTokenExpiryDate = expiryDate;
            return this;
        }

        // Método para construir o ApplicationUser com valores padrão
        public ApplicationUser Build()
        {
            return new ApplicationUser
            {
                UserName = _userName,
                Email = _email,
                PasswordHash = _passwordHash,
                PhoneNumber = _phoneNumber,
                IsActive = _isActive,
                RefreshToken = _refreshToken,
                RefreshTokenExpiryDate = _refreshTokenExpiryDate
            };
        }
    }
}
