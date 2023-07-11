using API_Roda_Llantas.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Roda_Llantas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TipoProductoController : ControllerBase
    {
        private readonly ITipoProductoModel _tipoproductosModel;

        public TipoProductoController(ITipoProductoModel tipoproductosModel)
        {
            _tipoproductosModel = tipoproductosModel;
        }
        
        [HttpGet]
        [Route("ConsultarTipoProducto")]
        
        public IActionResult ConsultarTipoProducto()
        {
            try
            {
                var resultado = _tipoproductosModel.ConsultarTipoProducto();
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
    }
}
