using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API_Roda_Llantas.Entities;
using API_Roda_Llantas.Interfaces;
using API_Roda_Llantas.Models;

namespace API_Roda_Llantas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   [Authorize]
    public class CarritoController : ControllerBase
    {
        private readonly ICarritoModel _carritoModel;

        public CarritoController(ICarritoModel carritoModel)
        {
            _carritoModel = carritoModel;
        }

        [HttpGet]
        [Route("MostrarCarritoTemporal")]
        public IActionResult MostrarCarritoTemporal(long Usu_Id)
        {
            try
            {
                var resultado = _carritoModel.MostrarCarritoTemporal(Usu_Id);

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
        [Route("MostrarCarritoTotal")]
        public IActionResult MostrarCarritoTotal(long Usu_Id)
        {
            try
            {
                var resultado = _carritoModel.MostrarCarritoTotal(Usu_Id);

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

        [HttpPost]
        [Route("ConfirmarPago")]
        public IActionResult ConfirmarPago(CarritoEntities entidad)
        {
            try
            {
                _carritoModel.ConfirmarPago(entidad);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPost]
        [Route("RegistrarCompra")]
        public IActionResult RegistrarCompra(List<ProductosEntities> entidad)
        {
            try
            {
                
                return Ok(_carritoModel.RegistrarCompra(entidad));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
