using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Modelos.Persona
{
    public class PersonaPost
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "El nombre debe ser mayor a 2 caracteres y menor a 50 caracteres.")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "El nombre debe contener solo letras")]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "El nombre debe ser mayor a 2 caracteres y menor a 150 caracteres.")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "El apellido debe contener solo letras")]
        public string? Apellido { get; set; }

        [Required(ErrorMessage = "La dirección es obligatoria.")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "El nombre debe ser mayor a 2 caracteres y menor a 200 caracteres.")]
        public string? Direccion { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        [StringLength(9, MinimumLength = 2, ErrorMessage = "El teléfono debe ser de Costa Rica.")]
        [RegularExpression("^[- ]?\\d{4}[- ]?\\d{4}$", ErrorMessage = "El teléfono debe tener el formato 8897-7832")]
        public string? Telefono { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio.")]
        [StringLength(150, MinimumLength = 5, ErrorMessage = "El correo debe ser mayor a 5 caracteres y menor a 100 caracteres.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "El correo debe contener un @ y punto como minimo.")]
        public string? Correo { get; set; }
        public Boolean Activo { get; set; }
    }
}