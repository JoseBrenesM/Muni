using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Modelos
{
    public class Login
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Ingresa un correo valido")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "El correo debe contener un @ y punto como minimo.")]
        public string? Correo { get; set; }

        public string? Hash { get; set; }
    }
}