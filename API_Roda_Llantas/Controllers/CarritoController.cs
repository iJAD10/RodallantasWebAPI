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

        [HttpGet("ListarPorUsuario/{usuId}")]
        public IActionResult ListarPorUsuario(int usuId)
        {
            var items = _carritoModel.ListarCarritoPorUsuario(usuId);
            return Ok(items);
        }

    }


}
