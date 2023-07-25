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
    public class VehiculosModel : IVehiculosModel
    {
        private readonly IConfiguration _configuration;

        public VehiculosModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<VehiculosEntities> ConsultarVehiculos()
        {
            using (var conexion = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return conexion.Query<VehiculosEntities>("ConsultarVehiculos",
                                    new { },
                                    commandType: System.Data.CommandType.StoredProcedure).ToList();
            }
        }

        public VehiculosEntities? ConsultarVehiculo(long Veh_Id)
        {
            using (var conexion = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return conexion.Query<VehiculosEntities>("ConsultarVehiculo",
                                    new { Veh_Id },
                                    commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        public VehiculosEntities? ConsultarPlaca(string Placa)
        {
            using (var conexion = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return conexion.Query<VehiculosEntities>("ConsultarPlaca",
                    new { Placa },
                    commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
            }
        }
        
        public void ActualizarVehiculos(VehiculosEntities e)
        {
            using (var conexion = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                conexion.Execute("ActualizarVehiculos",
                    new { Veh_Id = e.Veh_Id, Veh_Marca = e.Veh_Marca, Veh_Modelo = e.Veh_Modelo, Veh_Placa = e.Veh_Placa, 
                                Veh_Tipo_Vehiculo_Id = e.Veh_Tipo_Vehiculo_Id,Veh_Fecha_Lanzamiento = e.Veh_Fecha_Lanzamiento },
                    commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public int RegistrarVehiculos(VehiculosEntities entidad)
        {
            using (var conexion = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return conexion.Execute("RegistrarVehiculos",
                    new { entidad.Veh_Placa, entidad.Veh_Marca, entidad.Veh_Modelo, entidad.Veh_Fecha_Lanzamiento },
                    commandType: System.Data.CommandType.StoredProcedure);
            }
        }

    }
}
