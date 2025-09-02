using System.ComponentModel.DataAnnotations;

namespace Inmobiliaria_.Net_Core.Models
{
    public class Inquilino
    {
        [Key]
        [Display(Name = "Código")]
        public int IdInquilino { get; set; }
        [Required]
        public string Nombre { get; set; } = string.Empty;      // Corregido
        [Required]
        public string Apellido { get; set; } = string.Empty;    // Corregido
        [Required]
        public string Dni { get; set; } = string.Empty;         // Corregido
        public string Telefono { get; set; } = string.Empty;    // Corregido
        public string Direccion { get; set; } = string.Empty;   // Corregido
    }
}
