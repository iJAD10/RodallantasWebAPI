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
    public class UsuarioModel : IUsuarioModel
    {
        private readonly IConfiguration _configuration;
        private readonly IUtilitariosModel _utilitariosModel;

        public UsuarioModel(IConfiguration configuration, IUtilitariosModel utilitariosModel)
        {
            _configuration = configuration;
            _utilitariosModel = utilitariosModel;
        }

        public UsuarioEntities? ValidarCredenciales(UsuarioEntities entidad)
        {
            using (var conexion = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return conexion.Query<UsuarioEntities>("ValidarCredenciales",
                    new { entidad.Usu_Correo, entidad.Usu_Clave },
                    commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        public string GenerarToken(string CorreoElectronico)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, CorreoElectronico)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("b14ca5898a4e4133bbce2ea2315a1916"));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(20),
                signingCredentials: cred);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public List<UsuarioEntities> ConsultarUsuarios()
        {
            using (var conexion = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return conexion.Query<UsuarioEntities>("ConsultarUsuarios",
                                    new { },
                                    commandType: System.Data.CommandType.StoredProcedure).ToList();
            }
        }

        public int InactivarUsuario(UsuarioEntities entidad)
        {
            using (var conexion = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return conexion.Execute("InactivarUsuario",
                    new { entidad.Usu_Id },
                    commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public UsuarioEntities? BuscarExisteCorreo(string Usu_Correo)
        {
            using (var conexion = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return conexion.Query<UsuarioEntities>("BuscarExisteCorreo",
                        new { Usu_Correo },
                        commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        public void RecuperarContrasenna(UsuarioEntities entidad)
        {
            var resultado = BuscarExisteCorreo(entidad.Usu_Correo);

            if (resultado != null)
                _utilitariosModel.EnviarCorreo(resultado.Usu_Correo, "Recuperar contraseña.", "Su contraseña para la página Roda Llantas es: " + resultado.Usu_Clave);
        }

        public void ActualizarUsuario(UsuarioEntities e)
        {
            using (var conexion = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                conexion.Execute("ActualizarUsuario",
                    new { e.Usu_Clave, e.Usu_Cedula, e.Usu_Nombre, e.Usu_Id },
                    commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public int RegistrarUsuario(UsuarioEntities entidad)
        {
            using (var conexion = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return conexion.Execute("RegistrarUsuario",
                    new 
                    {
                        entidad.Usu_Cedula,
                        entidad.Usu_Nombre,
                        entidad.Usu_Correo, 
                        entidad.Usu_Clave
                    },
                    commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public UsuarioEntities? ConsultarUsuario(long Usu_Id)
        {
            using (var conexion = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return conexion.Query<UsuarioEntities>("ConsultarUsuario",
                    new { Usu_Id},
                    commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
            }
        }
    }
}
