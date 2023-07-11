using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API_Roda_Llantas.Entities;
using API_Roda_Llantas.Interfaces;

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

    }
}
