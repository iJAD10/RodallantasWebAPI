using API_Roda_Llantas.Entities;

namespace API_Roda_Llantas.Interfaces
{
    public interface ICompras
    {
        public void FinalizarCompra(int usuId, int total, string correoUsuario);
        IEnumerable<OrdenCompraListar> ListarOrdenCompra();
        public IEnumerable<ListarDetalleOrdenPorId> ListarDetalleOrdenPorId(int orden_Id);
        public int CambiarCompletadoOrdenCompra(int orden_Id);
    }
}
