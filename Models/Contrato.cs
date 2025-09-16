using System;
using System.ComponentModel.DataAnnotations;

namespace Inmobiliaria_.Net_Core.Models
{
    public class Contrato
    {
        public int IdContrato { get; set; }
        public int IdInquilino { get; set; }
        public int IdInmueble { get; set; }
        public decimal MontoMensual { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

        // Agrega estas propiedades para evitar errores y warnings
        public Inquilino Inquilino { get; set; } = new Inquilino();
        public Inmueble Inmueble { get; set; } = new Inmueble();
        public Propietario Propietario { get; set; } = new Propietario();
    }
}