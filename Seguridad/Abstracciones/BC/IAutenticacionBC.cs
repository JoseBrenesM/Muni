using Abstracciones.Modelos;

namespace Abstracciones.BC
{
    public interface IAutenticacionBC
    {
        Task<Token> LoginAsync(Login login);
    }
}