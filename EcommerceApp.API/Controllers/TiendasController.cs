using EcommerceApp.Business.DTOs;
using EcommerceApp.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TiendasController : ControllerBase
    {
        private readonly ITiendaService _tiendaService;

        public TiendasController(ITiendaService tiendaService)
        {
            _tiendaService = tiendaService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var tiendas = await _tiendaService.GetAllAsync();
                return Ok(tiendas);
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
                var tienda = await _tiendaService.GetByIdAsync(id);
                return Ok(tienda);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TiendaCreateDto dto)
        {
            try
            {
                var tienda = await _tiendaService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = tienda.Id }, tienda);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TiendaUpdateDto dto)
        {
            try
            {
                await _tiendaService.UpdateAsync(id, dto);
                return Ok(new { message = "Tienda actualizada exitosamente" });
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
                await _tiendaService.DeleteAsync(id);
                return Ok(new { message = "Tienda eliminada exitosamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
