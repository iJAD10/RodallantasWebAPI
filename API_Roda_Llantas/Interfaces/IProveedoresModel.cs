using API_Roda_Llantas.Entities;

namespace API_Roda_Llantas.Interfaces
{
    public interface IProveedoresModel
    {
        public int RegistrarProveedores(ProveedoresEntities entidad);
        public void ActualizarProveedores(ProveedoresEntities entidad);
        public List<ProveedoresEntities>? ConsultarProveedores();
        public ProveedoresEntities? ConsultarProveedor(long Prov_Id);
        public int InactivarProveedor(ProveedoresEntities entidad);
        public List<ProveedoresEntities> ConsultarProveedorXNombre();
    }
}