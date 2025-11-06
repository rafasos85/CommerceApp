using EcommerceApp.Business.DTOs;
using EcommerceApp.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ArticulosController : ControllerBase
    {
        private readonly IArticuloService _articuloService;

        public ArticulosController(IArticuloService articuloService)
        {
            _articuloService = articuloService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var articulos = await _articuloService.GetAllAsync();
                return Ok(articulos);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var articulo = await _articuloService.GetByIdAsync(id);
                return Ok(articulo);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("tienda/{tiendaId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByTiendaId(int tiendaId)
        {
            try
            {
                var articulos = await _articuloService.GetByTiendaIdAsync(tiendaId);
                return Ok(articulos);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ArticuloCreateDto dto)
        {
            try
            {
                var articulo = await _articuloService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = articulo.Id }, articulo);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ArticuloUpdateDto dto)
        {
            try
            {
                await _articuloService.UpdateAsync(id, dto);
                return Ok(new { message = "Artículo actualizado exitosamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _articuloService.DeleteAsync(id);
                return Ok(new { message = "Artículo eliminado exitosamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("asignar-tienda")]
        public async Task<IActionResult> AsignarATienda([FromBody] ArticuloTiendaCreateDto dto)
        {
            try
            {
                var articuloTienda = await _articuloService.AsignarArticuloATiendaAsync(dto);
                return Ok(articuloTienda);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
