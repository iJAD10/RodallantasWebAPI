using API_Roda_Llantas.Entities;

namespace API_Roda_Llantas.Interfaces
{
    public interface ICarritoModel
    {
        public bool RegistrarCompra(List<ProductosEntities> entidad);
        public CarritoEntities? MostrarCarritoTemporal(long Usu_Id);

        public List<CarritoEntities> MostrarCarritoTotal(long Usu_Id);

        public void ConfirmarPago(CarritoEntities entidad);

        public List<CarritoEntities> MostrarFacturasRealizadas(long Usu_Id);
    }
}
