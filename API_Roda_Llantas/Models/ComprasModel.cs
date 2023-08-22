using API_Roda_Llantas.Entities;
using API_Roda_Llantas.Interfaces;
using Dapper;
using System;
using System.Data;
using System.Data.SqlClient;

namespace API_Roda_Llantas.Models
{
    public class ComprasModel: ICompras
    {
        private IConfiguration _configuration;

        public ComprasModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

            public void FinalizarCompra(int usuId, int total)
        {
            using (var conexion = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                try
                {
                    conexion.Execute("FinalizarCompra",
                        new { @Usu_Id = usuId, @Total = total},
                        commandType: System.Data.CommandType.StoredProcedure);
                }
                catch (SqlException ex)
                { 
                    throw;  // Si no es el error esperado, relanza la excepción original.
                }
        }
        public IEnumerable<OrdenCompraListar> ListarOrdenCompra()
        {
            using (var conexion = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                try
            {
                return conexion.Query<OrdenCompraListar>("sp_ListarOrdenCompra",
                   
                    commandType: System.Data.CommandType.StoredProcedure);
            }
                catch (SqlException ex)
                {
                    throw;  // Si no es el error esperado, relanza la excepción original.
                }
        }

        public IEnumerable<ListarDetalleOrdenPorId> ListarDetalleOrdenPorId(int orden_Id)
        {
            using (var conexion = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return conexion.Query<ListarDetalleOrdenPorId>("sp_ListarDetalleOrden",
                    new { @Orden_Id = orden_Id },
                    commandType: System.Data.CommandType.StoredProcedure);
            }
        }
        public int CambiarCompletadoOrdenCompra(int orden_Id)
        {
            using (var conexion = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return conexion.Execute("[sp_ToggleEstadoOrdenCompra]",
                    new { @Orden_Id = orden_Id },
                    commandType: System.Data.CommandType.StoredProcedure);
            }
        }
    }
}
