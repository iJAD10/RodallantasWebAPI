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

        public CarritoEntities? MostrarCarritoTemporal(long Usu_Id)
        {
            using (var conexion = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return conexion.Query<CarritoEntities>("MostrarCarritoTemporal",
                                    new { Usu_Id },
                                    commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        public List<CarritoEntities> MostrarCarritoTotal(long Usu_Id)
        {
            using (var conexion = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return conexion.Query<CarritoEntities>("MostrarCarritoTotal",
                                    new { Usu_Id },
                                    commandType: System.Data.CommandType.StoredProcedure).ToList();
            }
        }

        public void ConfirmarPago(CarritoEntities entidad)
        {
            using (var conexion = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                conexion.Execute("ConfirmarPago",
                                    new { entidad.Usu_Id },
                                    commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public List<CarritoEntities> MostrarFacturasRealizadas(long Usu_Id)
        {
            using (var conexion = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return conexion.Query<CarritoEntities>("MostrarFacturasRealizadas",
                                    new { Usu_Id },
                                    commandType: System.Data.CommandType.StoredProcedure).ToList();
            }
        }
    }
}
