using API_Roda_Llantas.Entities;

namespace API_Roda_Llantas.Interfaces
{
    public interface IProductosModel
    {
        public List<ProductosEntities> ConsultarProductos();
        public List<ProductosEntities> ConsultarProductosXIDTipoProducto(TipoProductoEntities entidad);
        public ProductosEntities ConsultarProductoXID(TipoProductoEntities entidad);
        public int RegistrarProductos(ProductosRegistrarEntities entidad);
        public void ActualizarProductos(ProductosEntities e);


    }
}
