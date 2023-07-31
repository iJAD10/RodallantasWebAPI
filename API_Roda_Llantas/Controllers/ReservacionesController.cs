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
	[AllowAnonymous]

	public class ReservacionesController : ControllerBase
    {
        private readonly IReservacionesModel _reservacionesModel;
        public ReservacionesController(IReservacionesModel reservacionesModel)
        {
            _reservacionesModel = reservacionesModel;
        }

        [HttpPost]
        [Route("RegistrarReservacion")]
        public IActionResult RegistrarReservacion(ReservacionesEntities entidad)
        {
            try
            {
                return Ok(_reservacionesModel.RegistrarReservacion(entidad));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        [Route("RegistrarVehiculoYReservacion")]
        public IActionResult RegistrarVehiculoYReservacion(ReservacionesEntities entidad)
        {
            try
            {
                return Ok(_reservacionesModel.RegistrarVehiculoYReservacion(entidad));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Route("ConsultarReservaciones")]

        public IActionResult ConsultarReservaciones()

        {
	        try
	        {
		        var resultado = _reservacionesModel.ConsultarReservaciones();
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
        [Route("DetallesReservacion")]
        public IActionResult DetallesReservacion(long q)

        {
	        try
	        {
		        var resultado = _reservacionesModel.DetallesReservacion(q);
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
        [Route("CambiarCompletado")]
        public IActionResult CambiarCompletado(ReservacionesEntities entidad)
        {
	        try
	        {
		        return Ok(_reservacionesModel.CambiarCompletado(entidad));
	        }
	        catch (Exception ex)
	        {
		        return BadRequest(ex.Message);
	        }
        }
	}
}