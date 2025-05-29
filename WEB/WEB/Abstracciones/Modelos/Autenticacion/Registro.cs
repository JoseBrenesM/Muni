using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Modelos.Autenticacion
{
    public class Registro
    {
        [BindProperty(SupportsGet = true)]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "El nombre debe contener solo letras")]
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "El nombre debe ser mayor a 2 caracteres y menor a 50 caracteres.")]
        [DisplayName("Nombre")]
        public string? Nombre { get; set; }

        [BindProperty(SupportsGet = true)]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "Los apellidos deben contener solo letras")]
        [Required(ErrorMessage = "Los apellidos son obligatorios.")]
        [StringLength(150, MinimumLength = 5, ErrorMessage = "Los apellidos debe ser mayor a 5 caracteres y menor a 150 caracteres.")]
        [DisplayName("Apellidos")]
        public string? Apellido { get; set; }

        [BindProperty(SupportsGet = true)]
        [Required(ErrorMessage = "El departamento es obligatorio.")]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "El departamento debe ser mayor a 5 caracteres y menor a 200 caracteres.")]
        [DisplayName("Dirección")]
        public string? Direccion { get; set; }

        [Required(ErrorMessage = "El teléfono es requerido.")]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "Ingresa un número de telefono válido")]
        [RegularExpression("^[- ]?\\d{4}[- ]?\\d{4}$", ErrorMessage = "El formato del número debe ser como: 7896-7802")]
        [DisplayName("Teléfono")]
        public string? Telefono { get; set; }

        [BindProperty(SupportsGet = true)]
        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [StringLength(150, MinimumLength = 5, ErrorMessage = "Ingresa un correo válido")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "El correo debe contener un @ y punto como minimo.")]
        [DisplayName("Correo electrónico")]
        public string? Correo { get; set; }

        [BindProperty(SupportsGet = true)]
        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 12, ErrorMessage = "La contraseña debe tener al menos 12 caracteres.")]
        [DisplayName("Contraseña")]
        public string? Password { get; set; }

        [BindProperty(SupportsGet = true)]  
        [Required(ErrorMessage = "Repetir la contraseña es obligatorio.")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 12, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
        [DisplayName("Repetir contraseña")]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
        public string? ConfirmPassword { get; set; }


        public string? Hash { get; set; }
    }
}