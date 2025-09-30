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

        public Inquilino? Inquilino { get; set; }
        public Inmueble? Inmueble { get; set; }
        public Propietario? Propietario { get; set; }
        //public string Descripcion => $"Contrato #{IdContrato} - Inquilino {IdInquilino}";
        public string Descripcion
    {
        get
        {
            var inquilino = Inquilino != null ? $"{Inquilino.Nombre} {Inquilino.Apellido}" : "Sin Inquilino";
            var direccion = Inmueble != null ? Inmueble.Direccion : "Sin Dirección";
            return $"Contrato #{IdContrato} - {inquilino} - {direccion}";
        }
    }
    }
}