using System;
using System.ComponentModel.DataAnnotations;

namespace Inmobiliaria_.Net_Core.Models
{
    public class Pago
    {
        public int Id_Pago { get; set; }

        [Required]
        public int Id_Contrato { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Pago")]
        public DateTime Fecha_Pago { get; set; }

        [Required, Display(Name = "Monto Pagado")]
        [Range(0.01, 999999.99)]
        public decimal Monto_Pagado { get; set; }

        [Display(Name = "Mes Correspondiente")]
        [Range(1, 12)]
        public byte Mes_Correspondiente { get; set; }

        [Display(Name = "Año Correspondiente")]
        public short Anio_Correspondiente { get; set; }

        [Display(Name = "Estado")]
        public string Estado { get; set; }

        // Propiedad de navegación opcional
        public Contrato Contrato { get; set; }
    }
}
