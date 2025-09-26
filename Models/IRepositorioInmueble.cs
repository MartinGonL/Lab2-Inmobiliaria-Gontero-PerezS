using System.Collections.Generic;

namespace Inmobiliaria_.Net_Core.Models
{
    public interface IRepositorioInmueble
    {
        int Alta(Inmueble casa);
        int Baja(int id);
        int Modificacion(Inmueble casa);
        IList<Inmueble> ObtenerTodos();
        Inmueble ObtenerPorId(int id);
        IList<Inmueble> BuscarPorDireccion(string direccion);
    }
}