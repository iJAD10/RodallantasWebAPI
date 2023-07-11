using API_Roda_Llantas.Entities;
using API_Roda_Llantas.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace API_Roda_Llantas.Models
{
    public class BitacoraModel : IBitacoraModel
    {
        private readonly IConfiguration _configuration;

        public BitacoraModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void RegistrarBitacora(BitacoraEntities entidad)
        {
            using (var conexion = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                conexion.Execute("RegistrarBitacora",
                    new { entidad.Bit_Detalle, entidad.Bit_Origen, entidad.Bit_Usu_Id },
                    commandType: System.Data.CommandType.StoredProcedure);
            }
        }
    }
}
