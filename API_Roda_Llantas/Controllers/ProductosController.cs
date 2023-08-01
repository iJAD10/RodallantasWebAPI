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
    public class ProductosController : ControllerBase
    {
        private readonly IProductosModel _productosModel;

        public ProductosController(IProductosModel productosModel)
        {
            _productosModel = productosModel;
        }

        [HttpGet]
        [Route("ConsultarProductos")]

        public IActionResult ConsultarProductos()

        {
            try
            {
                var resultado = _productosModel.ConsultarProductos();
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
        [Route("ConsultarProductosXIDTipoProducto")]
        public IActionResult ConsultarProductosXIDTipoProducto(TipoProductoEntities entidad)

        {
            try
            {
                var resultado = _productosModel.ConsultarProductosXIDTipoProducto(entidad);
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
        [Route("ConsultarProductoXID")]
        public IActionResult ConsultarProductoXID(int Id)

        {
            try
            {
                var resultado = _productosModel.ConsultarProductoXID(Id);
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
        [Route("ActualizarProductos")]
        public IActionResult ActualizarProductos(ProductosEntities entidad)
        {
            try
            {
                _productosModel.ActualizarProductos(entidad);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("RegistrarProductos")]
        public IActionResult RegistrarProductos(ProductosRegistrarEntities entidad)
        {
            try
            {
                return Ok(_productosModel.RegistrarProductos(entidad));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
