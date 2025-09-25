using System.ComponentModel.DataAnnotations;

namespace Inmobiliaria_.Net_Core.Models
{
    public class Inmueble
    {
        [Key]
        public int IdInmueble { get; set; }

        [Required]
        public int IdPropietario { get; set; }
        public Propietario? Duenio { get; set; } // Propietario relacionado

        [Required]
        [StringLength(255)]
        public string Direccion { get; set; } = string.Empty;

        [StringLength(50)]
        public string Tipo { get; set; } = string.Empty;

        [Required]
        public string Estado { get; set; } = string.Empty;

        // Relación con Propietario
        public Propietario? Propietario { get; set; }
    }
}