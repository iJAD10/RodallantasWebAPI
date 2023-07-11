using API_Roda_Llantas.Entities;
using API_Roda_Llantas.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace API_Roda_Llantas.Models
{
    public class ServiciosModel : IServiciosModel
    {
        private IConfiguration _configuration;

        public ServiciosModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<ServiciosEntities> ConsultarServicios()
        {
            using (var conexion = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return conexion.Query<ServiciosEntities>("ConsultarServicios",
                                    new { },
                                    commandType: System.Data.CommandType.StoredProcedure).ToList();
            }
        }

        public ServiciosEntities? ConsultarServicio(long Ser_Id)
        {
            using (var conexion = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return conexion.Query<ServiciosEntities>("ConsultarServicio",
                                    new { Ser_Id },
                                    commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
            }
        }


        public void ActualizarServicios(ServiciosEntities e)
        {
            using (var conexion = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                conexion.Execute("ActualizarServicios",
                    new { e.Ser_Id, e.Ser_Monto, e.Ser_Nombre },
                    commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public int RegistrarServicios(ServiciosEntities entidad)
        {
            using (var conexion = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return conexion.Execute("RegistrarServicios",
                    new 
                    { 
                        entidad.Ser_Monto, 
                        entidad.Ser_Nombre 
                    },
                    commandType: System.Data.CommandType.StoredProcedure);
            }
        }
    }
}


    

