using EcommerceApp.Business.DTOs;
using EcommerceApp.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EcommerceApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CarritoController : ControllerBase
    {
        private readonly ICarritoService _carritoService;

        public CarritoController(ICarritoService carritoService)
        {
            _carritoService = carritoService;
        }

        private int GetClienteId()
        {
            var clienteIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.Parse(clienteIdClaim);
        }

        [HttpGet]
        public async Task<IActionResult> GetCarritoActivo()
        {
            try
            {
                var clienteId = GetClienteId();
                var carrito = await _carritoService.GetCarritoActivoAsync(clienteId);
                return Ok(carrito);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("agregar-item")]
        public async Task<IActionResult> AgregarItem([FromBody] AgregarItemCarritoDto dto)
        {
            try
            {
                var clienteId = GetClienteId();
                var carrito = await _carritoService.AgregarItemAsync(clienteId, dto);
                return Ok(carrito);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("actualizar-item")]
        public async Task<IActionResult> ActualizarItem([FromBody] ActualizarItemCarritoDto dto)
        {
            try
            {
                var clienteId = GetClienteId();
                var carrito = await _carritoService.ActualizarItemAsync(clienteId, dto);
                return Ok(carrito);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("remover-item/{itemId}")]
        public async Task<IActionResult> RemoverItem(int itemId)
        {
            try
            {
                var clienteId = GetClienteId();
                await _carritoService.RemoverItemAsync(clienteId, itemId);
                return Ok(new { message = "Item removido del carrito" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("completar-compra")]
        public async Task<IActionResult> CompletarCompra()
        {
            try
            {
                var clienteId = GetClienteId();
                var carrito = await _carritoService.CompletarCompraAsync(clienteId);
                return Ok(new { message = "Compra completada exitosamente", data = carrito });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
