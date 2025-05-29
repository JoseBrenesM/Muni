using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Modelos.Roles
{
    public class RolesxUsuarioPost
    {
        [Required(ErrorMessage = "El Id del rol es obligatorio.")]
        public int Id { get; set; }

        public string? Tipo { get; set; }

        [Required(ErrorMessage = "El id de la persona es requerido.")]
        [RegularExpression(@"^[{]?[0-9a-fA-F]{8}-([0-9a-fA-F]{4}-){3}[0-9a-fA-F]{12}[}]?$", ErrorMessage = "El id de la persona no es valido.")]
        public Guid IdPersona { get; set; }
    }
}
