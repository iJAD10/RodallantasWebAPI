using API_Roda_Llantas.Entities;
using API_Roda_Llantas.Interfaces;
using API_Roda_Llantas.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_Roda_Llantas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class LoginController : ControllerBase
    {
        private readonly IUsuarioModel _usuarioModel;

        public LoginController(IUsuarioModel usuariosModel)
        {
            _usuarioModel = usuariosModel;
        }

        [HttpPost]
        [Route("ValidarCredenciales")]
        public IActionResult ValidarCredenciales(UsuarioEntities entidad)
        {
            try
            {
                var resultado = _usuarioModel.ValidarCredenciales(entidad);

                if (resultado != null)
                {
                    resultado.Token = _usuarioModel.GenerarToken(resultado.Usu_Correo);
                    return Ok(resultado);
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("Registrarusuario")]
        public IActionResult RegistrarUsuario(UsuarioEntities entidad)
        {
            try
            {
                return Ok(_usuarioModel.RegistrarUsuario(entidad));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("RecuperarContrasenna")]
        public IActionResult RecuperarContrasenna(UsuarioEntities entidad)
        {
            try
            {
                _usuarioModel.RecuperarContrasenna(entidad);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("BuscarExisteCorreo")]
        public IActionResult BuscarExisteCorreo(string Usu_Correo)
        {
            try
            {
                var resultado = _usuarioModel.BuscarExisteCorreo(Usu_Correo);
                string respuesta = string.Empty;

                if (resultado == null)
                    return NotFound();
                else
                {
                    if (!resultado.Usu_Estado)
                        respuesta = "Ya existe una cuenta inactiva con este correo.";
                    else
                        respuesta = "Ya existe una cuenta con este correo.";

                    return Ok(respuesta);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
