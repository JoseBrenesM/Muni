using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC
{
    public class Producto
    {
        [Required]
        public Guid ProductoID { get; set; }
        [Required(ErrorMessage = "El nombre del producto es obligatorio.")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "El nombre debe ser mayor a 6 caracteres y menor a 50 carcacteres.")]
        public string? Nombre { get; set; }
        [Required(ErrorMessage = "La categoria es obligatoria.")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "La categoria es obligatoria y ser mayor a 6 caracteres y menor a 50 carcacteres.")]
        public string? Categoria { get; set; }

        [Required(ErrorMessage = "La Presentacion es obligatoria.")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "La Presentacion es obligatoria y ser mayor a 6 caracteres y menor a 50 carcacteres.")]
        public string? Presentacion { get; set; }

        [Required(ErrorMessage = "El precio del producto es obligatorio.")]
        public decimal? Precio { get; set; }
        [Required(ErrorMessage = "La cantidad del producto es obligatoria.")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "La Descripcion es obligatoria.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "La Descripcion es obligatoria y ser mayor a 6 caracteres y menor a 100 carcacteres.")]
        public string? Descripcion { get; set; }
        public byte[]? Imagen { get; set; }
        public string? ImagenConvertida { get; set; }
    }
}
