using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Modelos.Roles
{
    public class RolesPost
    {
        [Required(ErrorMessage = "El Id del rol es obligatorio.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del rol es obligatorio.")]
        [StringLength(13, MinimumLength = 3, ErrorMessage = "El nombre del rol debe ser mayor a 3 caracteres y menor a 13 caracteres.")]
        public string? Tipo { get; set; }
    }
}