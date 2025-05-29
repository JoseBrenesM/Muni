using Abstracciones.Modelos;

namespace Abstracciones.BW
{
    public interface IAutenticacionBW
    {
        Task<Token> LoginAsync(Login login);
    }
}
