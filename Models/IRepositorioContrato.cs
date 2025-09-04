namespace Inmobiliaria_.Net_Core.Models
{
    public interface IRepositorioContrato
    {
        int Alta(Contrato contrato);
        int Baja(int id);
        int Modificacion(Contrato contrato);
        IList<Contrato> ObtenerTodos();
        Contrato ObtenerPorId(int id);
    }
}