using Inmobiliaria_.Net_Core.Models;
using System.Collections.Generic;

namespace Inmobiliaria_.Net_Core.Models
{
    public interface IRepositorioPropietario
    {
        int Alta(Propietario p);
        int Baja(int id);
        int Modificacion(Propietario p);        
        IList<Propietario> ObtenerTodos();
        Propietario ObtenerPorId(int id);
    }
}