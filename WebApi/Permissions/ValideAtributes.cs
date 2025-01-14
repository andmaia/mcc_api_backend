using Application.Validators;

namespace WebApi.Permissions
{
    public static class ValideAtributes
    {
        public static bool ValidatePagination(int take, int skip, out string errorMessage)
        {
            // Criação de uma instância do validador (caso ele não seja injetado diretamente)
            var paginationValidator = new PaginationValidator();

            // Validação dos valores de take e skip
            var takeValidationResult = paginationValidator.Validate(take);
            var skipValidationResult = paginationValidator.Validate(skip);

            // Verifica se algum dos resultados de validação é inválido
            if (!takeValidationResult.IsValid || !skipValidationResult.IsValid)
            {
                errorMessage = takeValidationResult.Errors.FirstOrDefault()?.ErrorMessage ?? skipValidationResult.Errors.FirstOrDefault()?.ErrorMessage;
                return false;
            }

            // Se ambos forem válidos
            errorMessage = string.Empty;
            return true;
        }
    }
}
