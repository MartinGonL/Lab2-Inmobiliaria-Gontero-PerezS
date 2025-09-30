using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inmobiliaria_.Net_Core.Models
{
    public class Pago
    {
        [Column("id_pago")]
        public int IdPago { get; set; }

        [Required]
        [Column("id_contrato")]
        public int IdContrato { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Pago")]
        [Column("fecha_pago")]
        public DateTime FechaPago { get; set; }

        [Required, Display(Name = "Monto Pagado")]
        [Range(0.01, 999999.99)]
        [Column("monto_pagado")]
        public decimal MontoPagado { get; set; }

        [Display(Name = "Mes Correspondiente")]
        [Range(1, 12)]
        [Column("mes_correspondiente")]
        public byte MesCorrespondiente { get; set; }

        [Display(Name = "Año Correspondiente")]
        [Column("anio_correspondiente")]
        public short AnioCorrespondiente { get; set; }

        [Display(Name = "Estado")]
        [Column("estado")]
        public string Estado { get; set; }

        // Propiedad de navegación opcional
        public Contrato Contrato { get; set; }
    }
}
