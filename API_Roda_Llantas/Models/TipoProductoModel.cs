using Dapper;
using API_Roda_Llantas.Entities;
using API_Roda_Llantas.Interfaces;
using System.Data.SqlClient;

namespace API_Roda_Llantas.Models

{
    public class TipoProductoModel : ITipoProductoModel
    {
        private IConfiguration _configuration;
        public TipoProductoModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<TipoProductoEntities> ConsultarTipoProducto()
        {
            using (var conexion = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return conexion.Query<TipoProductoEntities>("ConsultarTipoProducto",
                                    new { },
                                    commandType: System.Data.CommandType.StoredProcedure).ToList();
            }
        }
    }
}
