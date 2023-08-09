using API_Roda_Llantas.Entities;
using API_Roda_Llantas.Interfaces;
using Dapper;
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

            public void FinalizarCompra(int usuId)
        {
            using (var conexion = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                try
                {
                    conexion.Execute("FinalizarCompra",
                        new { @Usu_Id = usuId},
                        commandType: System.Data.CommandType.StoredProcedure);
                }
                catch (SqlException ex)
                { 
                    throw;  // Si no es el error esperado, relanza la excepción original.
                }
        }
    }
}
