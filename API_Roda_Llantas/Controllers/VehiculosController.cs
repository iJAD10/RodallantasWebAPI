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
    public class VehiculosController : ControllerBase
    {
        private readonly IVehiculosModel _vehiculosModel;
        // GET: VehiculosController
        public VehiculosController(IVehiculosModel vehiculosModel)
        {
            _vehiculosModel = vehiculosModel;
        }

        [HttpGet]
        [Route("ConsultarVehiculos")]

        public IActionResult ConsultarVehiculos()

        {
            try
            {
                var resultado = _vehiculosModel.ConsultarVehiculos();
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
        [Route("ConsultarVehiculo")]
        public IActionResult ConsultarVehiculo(long q)

        {
            try
            {
                var resultado = _vehiculosModel.ConsultarVehiculo(q);
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

        [HttpGet]
        [Route("ConsultarPlaca")]
        public IActionResult ConsultarPlaca(string q)

        {
            try
            {
                var resultado = _vehiculosModel.ConsultarPlaca(q);
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
        [Route("ActualizarVehiculos")]
        public IActionResult ActualizarVehiculos(VehiculosEntities entidad)
        {
            try
            {
                _vehiculosModel.ActualizarVehiculos(entidad);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("RegistrarVehiculos")]
        public IActionResult RegistrarVehiculos(VehiculosEntities entidad)
        {
            try
            {
                return Ok(_vehiculosModel.RegistrarVehiculos(entidad));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
