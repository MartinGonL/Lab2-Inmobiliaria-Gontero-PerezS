using System;
using System.ComponentModel.DataAnnotations;

namespace Inmobiliaria_.Net_Core.Models
{
    public class Contrato
    {
        [Key]
        public int IdContrato { get; set; }

        [Required]
        public int IdInquilino { get; set; }

        [Required]
        public int IdInmueble { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal MontoMensual { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime FechaInicio { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime FechaFin { get; set; }

        public int CreadoPor { get; set; }

        public int? TerminadoPor { get; set; }

    }
}