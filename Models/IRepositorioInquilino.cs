namespace Inmobiliaria_.Net_Core.Models
{
    public interface IRepositorioInquilino
    {
        int Alta(Inquilino i);
        int Baja(int id);
        int Modificacion(Inquilino i);
        IList<Inquilino> ObtenerTodos();
        Inquilino ObtenerPorId(int id);
        IList<Inquilino> BuscarPorNombre(string nombre);
    }
}