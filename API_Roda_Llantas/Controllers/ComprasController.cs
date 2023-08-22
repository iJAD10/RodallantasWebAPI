using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using API_Roda_Llantas.Interfaces;
using API_Roda_Llantas.Models;
using API_Roda_Llantas.Entities;

namespace API_Roda_Llantas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ComprasController : ControllerBase
    {
        private readonly ICompras _comprasModel;
        private readonly IUtilitariosModel _utilitariosModel;

        public ComprasController(ICompras comprasModel, IUtilitariosModel utilitariosModel)
        {
            _comprasModel = comprasModel;
            _utilitariosModel = utilitariosModel;
        }

        [HttpPost("FinalizarCompra")]
        public IActionResult FinalizarCompra([FromBody] Request request)
        {
            try
            {
                _comprasModel.FinalizarCompra(request.Usu_Id, request.Total);
                return Ok(new { Message = "Producto agregado al carrito exitosamente." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("Listar")]
        public IActionResult Listar()
        {
            var lista = _comprasModel.ListarOrdenCompra();
            if (lista == null || !lista.Any())
                return NotFound();
            return Ok(lista);
        }

        [HttpGet("ListarDetalleOrdenPorId/{orden_Id}")]
        public IActionResult ListarDetalleOrdenPorId(int orden_Id)
        {
            var items = _comprasModel.ListarDetalleOrdenPorId(orden_Id);
            return Ok(items);
        }

        [HttpPut]
        [Route("CambiarCompletado/{orden_Id}")]
        public IActionResult CambiarCompletado(int orden_Id)
        {
            try
            {
                return Ok(_comprasModel.CambiarCompletadoOrdenCompra(orden_Id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("Count")]
        public IActionResult Count()
        {
            try
            {
                return Ok(_utilitariosModel.Count());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
    public class Request
    {
        public int Usu_Id { get; set; }
        public int Total { get; set; }

    }
}

