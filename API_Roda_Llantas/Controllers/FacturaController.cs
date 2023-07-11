using API_Roda_Llantas.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_Roda_Llantas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FacturaController : ControllerBase
    {
        private readonly IFacturaModel _facturaModel;

        public FacturaController(IFacturaModel facturaModel)
        {
            _facturaModel = facturaModel;
        }
        
        [HttpGet]
        [Route("ConsultarUltimaCompra")]
        public IActionResult ConsultarUltimaCompra()
        {
            try
            {

                return Ok(_facturaModel.ConsultarUltimaCompra());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
