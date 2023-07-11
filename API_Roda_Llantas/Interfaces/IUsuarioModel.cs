 using API_Roda_Llantas.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API_Roda_Llantas.Interfaces
{
    public interface IUsuarioModel
    {
        public UsuarioEntities? ValidarCredenciales(UsuarioEntities entidad);
        public string GenerarToken(string CorreoElectronico);
        public void RecuperarContrasenna(UsuarioEntities entidad);
        public UsuarioEntities? BuscarExisteCorreo(string Usu_Correo);
        public List<UsuarioEntities> ConsultarUsuarios();
        public int InactivarUsuario(UsuarioEntities entidad);
        public void ActualizarUsuario(UsuarioEntities entidad);
        public int RegistrarUsuario(UsuarioEntities entidad);
        public UsuarioEntities? ConsultarUsuario(long Usu_Id);
    }
}
