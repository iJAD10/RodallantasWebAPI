using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using API_Roda_Llantas.Interfaces;
using API_Roda_Llantas.Models;

namespace API_Roda_Llantas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ComprasController : ControllerBase
    {
        private readonly ICompras _comprasModel;

        public ComprasController(ICompras comprasModel)
        {
            _comprasModel = comprasModel;
        }

        [HttpPost]
        [HttpPost("FinalizarCompra")]
        public IActionResult FinalizarCompra([FromBody] Request request)
        {
            try
            {
                _comprasModel.FinalizarCompra(request.Usu_Id);
                return Ok(new { Message = "Producto agregado al carrito exitosamente." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
    public class Request
    {
        public int Usu_Id { get; set; }
  

    }
}

