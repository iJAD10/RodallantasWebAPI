using API_Roda_Llantas.Entities;
using API_Roda_Llantas.Interfaces;
using Dapper;
using System.Data.SqlClient;

namespace API_Roda_Llantas.Models
{
    public class FacturaModel : IFacturaModel
    {
        private IConfiguration _configuration;

        public FacturaModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public FacturaEntities ConsultarUltimaCompra()
        {
            using (var conexion = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return conexion.Query<FacturaEntities>("ConsultarUltimaCompra",
                                    commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault()!;
            }
        }
    }
}
