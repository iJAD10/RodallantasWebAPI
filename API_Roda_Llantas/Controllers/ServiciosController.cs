using API_Roda_Llantas.Entities;
using API_Roda_Llantas.Interfaces;
using API_Roda_Llantas.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace API_Roda_Llantas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ServiciosController : ControllerBase
    {
        private readonly IServiciosModel _serviciosModel;

        public ServiciosController(IServiciosModel serviciosModel)
        {
            _serviciosModel = serviciosModel;
        }

        [HttpGet]
        [Route("ConsultarServicios")]
        public IActionResult ConsultarServicios()

        {
            try
            {
                var resultado = _serviciosModel.ConsultarServicios();
                if (resultado.Count == 0)
                    return NotFound();
                else
                    return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("ConsultarServicio")]
        public IActionResult ConsultarServicio(long q)

        {
            try
            {
                var resultado = _serviciosModel.ConsultarServicio(q);
                if (resultado == null)
                    return NotFound();
                else
                    return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("ActualizarServicios")]
        public IActionResult ActualizarServicios(ServiciosEntities entidad)
        {
            try
            {
                _serviciosModel.ActualizarServicios(entidad);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("RegistrarServicios")]
        public IActionResult RegistrarServicios(ServiciosEntities entidad)
        {
            try
            {
                return Ok(_serviciosModel.RegistrarServicios(entidad));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}

    
