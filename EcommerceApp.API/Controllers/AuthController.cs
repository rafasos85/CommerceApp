using EcommerceApp.Business.DTOs;
using EcommerceApp.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public AuthController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] ClienteCreateDto dto)
        {
            try
            {
                var cliente = await _clienteService.CreateAsync(dto);
                return Ok(new { message = "Cliente registrado exitosamente", data = cliente });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            try
            {
                var response = await _clienteService.LoginAsync(dto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }
    }
}
