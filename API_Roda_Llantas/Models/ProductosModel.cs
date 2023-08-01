using API_Roda_Llantas.Entities;
using API_Roda_Llantas.Interfaces;
using Dapper;
using System.Data.SqlClient;

namespace API_Roda_Llantas.Models
{
    public class ProductosModel: IProductosModel
    {
        private IConfiguration _configuration;

        public ProductosModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<ProductosEntities> ConsultarProductos()
        {
            using (var conexion = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return conexion.Query<ProductosEntities>("ConsultarProductos",
                                    new { },
                                    commandType: System.Data.CommandType.StoredProcedure).ToList();
            }
        }

        public List<ProductosEntities> ConsultarProductosXIDTipoProducto(TipoProductoEntities entidad)
        {
            using (var conexion = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return conexion.Query<ProductosEntities>("ConsultarProductosXIDTipoProducto",
                                    new { @Nombre = entidad.TP_Nombre },
                                    commandType: System.Data.CommandType.StoredProcedure).ToList();
            }
        }

        public ProductosEntities ConsultarProductoXID(int Id)
        {
            using (var conexion = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return conexion.Query<ProductosEntities>("ConsultarProductoXID",
                                    new { Id },
                                    commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault()!;
            }
        }

        public void ActualizarProductos(ProductosEntities e)
        {
            using (var conexion = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                conexion.Execute("ActualizarProductos",
                    new { e.Prod_Marca, e.Prod_Precio, e.Prod_Id, e.Prod_Proveedor_Id },
                    commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public int RegistrarProductos(ProductosRegistrarEntities entidad)
        {
            using (var conexion = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return conexion.Execute("RegistrarProductos",
                    new
                    {
                        entidad.Prod_Marca,
                        entidad.Prod_Precio,
                        entidad.Prod_Proveedor_Id,
                        entidad.Prod_CantStock,
                        entidad.Prod_TipoProducto_Id,
                        entidad.Prod_Url_Img
                    },
                    commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public int RegistrarProductos(ProductosEntities entidad)
        {
            throw new NotImplementedException();
        }
    }
}
