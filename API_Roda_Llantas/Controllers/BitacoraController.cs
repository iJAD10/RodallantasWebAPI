using API_Roda_Llantas.Entities;
using API_Roda_Llantas.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Roda_Llantas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class BitacoraController : ControllerBase
    {
        private readonly IBitacoraModel _bitacoraModel;

        public BitacoraController(IBitacoraModel bitacoraModel)
        {
            _bitacoraModel = bitacoraModel;
        }

        [HttpPost]
        [Route("RegistrarBitacora")]
        public IActionResult RegistrarBitacora(BitacoraEntities entidad)
        {
            try
            {
                _bitacoraModel.RegistrarBitacora(entidad);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
