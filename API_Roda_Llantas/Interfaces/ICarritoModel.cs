using API_Roda_Llantas.Entities;

namespace API_Roda_Llantas.Interfaces
{
    public interface ICarritoModel
    {
        public bool RegistrarCompra(List<ProductosEntities> entidad);
        IEnumerable<CarritoListarEntities> ListarCarritoPorUsuario(int usuId);

    }
}
