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
    public class ProveedoresController : ControllerBase
    {
        private readonly IProveedoresModel _proveedoresModel;

        public ProveedoresController(IProveedoresModel proveedoresModel)
        {
            _proveedoresModel = proveedoresModel;
        }

        [HttpGet]
        [Route("ConsultarProveedores")]
        public IActionResult ConsultarProveedores()
        {
            try
            {
                var resultado = _proveedoresModel.ConsultarProveedores();
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

        [HttpPut]
        [Route("ActualizarProveedores")]
        public IActionResult ActualizarProveedores(ProveedoresEntities entidad)
        {
            try
            {
                _proveedoresModel.ActualizarProveedores(entidad);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("InactivarProveedor")]
        public IActionResult InactivarProveedor(ProveedoresEntities entidad)
        {
            try
            {
                return Ok(_proveedoresModel.InactivarProveedor(entidad));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("ConsultarProveedor")]
        public IActionResult ConsultarProveedor(long q)
        {
            try
            {
                var resultado = _proveedoresModel.ConsultarProveedor(q);
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
        [Route("RegistrarProveedores")]
        public IActionResult RegistrarProveedores(ProveedoresEntities entidad)
        {
            try
            {
                _proveedoresModel.RegistrarProveedores(entidad);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}