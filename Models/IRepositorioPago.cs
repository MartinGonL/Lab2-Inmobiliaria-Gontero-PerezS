using System.Collections.Generic;

namespace Inmobiliaria_.Net_Core.Models
{
    public interface IRepositorioPago
    {
        int Alta(Pago pago);
        int Baja(int id);
        int Modificacion(Pago pago);
        Pago ObtenerPorId(int id);
        IList<Pago> ObtenerTodos();
        IList<Pago> ObtenerPorContrato(int idContrato);
    }
}
