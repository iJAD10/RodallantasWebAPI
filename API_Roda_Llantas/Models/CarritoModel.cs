using API_Roda_Llantas.Interfaces;
using API_Roda_Llantas.Entities;
using System.Data.SqlClient;
using Dapper;

namespace API_Roda_Llantas.Models
{
    public class CarritoModel : ICarritoModel
    {
        private IConfiguration _configuration;

        public CarritoModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public bool RegistrarCompra(List<ProductosEntities> entidad)
        {
            using (var conexion = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                int Fact_Id = conexion.Query<int>("RegistrarFactura",
                                    commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();

                foreach (var producto in entidad)
                {

                    conexion.Execute("RegistrarProductoFactura",
                       new { @PV_Cantidad = producto.Prod_Cantidad, @PV_Producto_Id = producto.Prod_Id, @PV_Factura_Id = Fact_Id },
                       commandType: System.Data.CommandType.StoredProcedure);
                }
                return true;
            }
        }

        public IEnumerable<CarritoListarEntities> ListarCarritoPorUsuario(int usuId)
        {
            using (var conexion = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return conexion.Query<CarritoListarEntities>("ListarCarritoPorUsuario",
                    new { @Usu_Id = usuId },
                    commandType: System.Data.CommandType.StoredProcedure);
            }
        }

    }
}
