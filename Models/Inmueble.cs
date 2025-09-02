using System.ComponentModel.DataAnnotations;

namespace Inmobiliaria_.Net_Core.Models
{
    public class Inmueble
    {
        [Key]
        public int IdInmueble { get; set; }

        [Required]
        public int IdPropietario { get; set; }

        [Required]
        [StringLength(255)]
        public string Direccion { get; set; } = string.Empty; // Corregido

        [StringLength(50)]
        public string Tipo { get; set; } = string.Empty; // Corregido

        [Required]
        public string Estado { get; set; } = string.Empty; // Corregido

        // Relaciones
        public Propietario? Propietario { get; set; } // Permite nulo
    }
}