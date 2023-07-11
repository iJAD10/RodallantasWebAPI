using API_Roda_Llantas.Entities;
using API_Roda_Llantas.Interfaces;
using Dapper;
using System.Data.SqlClient;

namespace API_Roda_Llantas.Models
{
    public class ProveedoresModel : IProveedoresModel
    {
        private readonly IConfiguration _configuration;

        public ProveedoresModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public int RegistrarProveedores(ProveedoresEntities entidad)
        {
            using (var conexion = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return conexion.Execute("RegistrarProveedores",
                    new
                    {
                        entidad.Prov_Apellido_Agente,
                        entidad.Prov_Nombre_Agente,
                        entidad.Prov_Correo,
                        entidad.Prov_Direccion,
                        entidad.Prov_Telefono,
                        entidad.Prov_Telefono_Fijo,
                        entidad.Prov_Nombre_Empresa
                    },
                    commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public void ActualizarProveedores(ProveedoresEntities entidad)
        {
            using (var conexion = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                conexion.Execute("ActualizarProveedores",
                    new
                    {
                        entidad.Prov_Apellido_Agente,
                        entidad.Prov_Nombre_Agente,
                        entidad.Prov_Correo,
                        entidad.Prov_Direccion,
                        entidad.Prov_Telefono,
                        entidad.Prov_Telefono_Fijo,
                        entidad.Prov_Nombre_Empresa
                    },
                    commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public List<ProveedoresEntities> ConsultarProveedores()
        {
            using (var conexion = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return conexion.Query<ProveedoresEntities>("ConsultarProveedores",
                    new { },
                    commandType: System.Data.CommandType.StoredProcedure).ToList();
            }
        }

        public int InactivarProveedor(ProveedoresEntities entidad)
        {
            using (var conexion = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return conexion.Execute("InactivarProveedor",
                    new { entidad.Prov_Id },
                    commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public ProveedoresEntities? ConsultarProveedor(long Prov_Id)
        {
            using (var conexion = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return conexion.Query<ProveedoresEntities>("ConsultarProveedor",
                    new { Prov_Id },
                    commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
            }
        }

    }
}