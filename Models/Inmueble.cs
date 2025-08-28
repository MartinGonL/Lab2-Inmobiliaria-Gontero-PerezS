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
        public string Direccion { get; set; }

        [StringLength(50)]
        public string Tipo { get; set; }

        [Required]
        public string Estado { get; set; } 

        // Relaciones (opcional esto nos lo ofrecio la ia como mejora para tener acceso a la relacion)
        public Propietario Propietario { get; set; }
    }
}