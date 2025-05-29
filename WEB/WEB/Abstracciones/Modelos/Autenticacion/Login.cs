using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Abstracciones.Modelos.Autenticacion
{
    public class Login
    {
        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Ingresa un correo valido")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "El correo debe contener un @ y punto como minimo.")]
        [DisplayName("Correo electrónico")]
        public string? Correo { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "La contraseña debe ser como minimo de 6 caracteres.")]
        [DisplayName("Contraseña")]
        public string? Password { get; set; }

        public string? Hash { get; set; }
    }

    public class LoginRequest
    {
        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Ingresa un correo valido")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "El correo debe contener un @ y punto como minimo.")]
        [DisplayName("Correo electrónico")]
        public string? Correo { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "La contraseña debe ser como minimo de 5 caracteres.")]
        [DisplayName("Contraseña")]
        public string? Hash { get; set; }
    }
}