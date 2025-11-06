using EcommerceApp.Business.DTOs;
using EcommerceApp.Business.Interfaces;
using EcommerceApp.Data.Interfaces;
using EcommerceApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BCrypt.Net;

namespace EcommerceApp.Business.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IJwtService _jwtService;

        public ClienteService(IClienteRepository clienteRepository, IJwtService jwtService)
        {
            _clienteRepository = clienteRepository;
            _jwtService = jwtService;
        }

        public async Task<IEnumerable<ClienteDto>> GetAllAsync()
        {
            var clientes = await _clienteRepository.GetAllAsync();
            return clientes.Select(c => MapToDto(c));
        }

        public async Task<ClienteDto> GetByIdAsync(int id)
        {
            var cliente = await _clienteRepository.GetByIdAsync(id);
            if (cliente == null)
                throw new Exception("Cliente no encontrado");

            return MapToDto(cliente);
        }

        public async Task<ClienteDto> CreateAsync(ClienteCreateDto dto)
        {
            if (await _clienteRepository.EmailExistsAsync(dto.Email))
                throw new Exception("El email ya está registrado");

            var cliente = new Cliente
            {
                Nombre = dto.Nombre,
                Apellidos = dto.Apellidos,
                Direccion = dto.Direccion,
                Email = dto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                FechaRegistro = DateTime.Now,
                Activo = true
            };

            var created = await _clienteRepository.AddAsync(cliente);
            return MapToDto(created);
        }

        public async Task UpdateAsync(int id, ClienteUpdateDto dto)
        {
            var cliente = await _clienteRepository.GetByIdAsync(id);
            if (cliente == null)
                throw new Exception("Cliente no encontrado");

            cliente.Nombre = dto.Nombre;
            cliente.Apellidos = dto.Apellidos;
            cliente.Direccion = dto.Direccion;

            await _clienteRepository.UpdateAsync(cliente);
        }

        public async Task DeleteAsync(int id)
        {
            var cliente = await _clienteRepository.GetByIdAsync(id);
            if (cliente == null)
                throw new Exception("Cliente no encontrado");

            cliente.Activo = false;
            await _clienteRepository.UpdateAsync(cliente);
        }

        public async Task<LoginResponseDto> LoginAsync(LoginDto dto)
        {
            var cliente = await _clienteRepository.GetByEmailAsync(dto.Email);
            if (cliente == null || !cliente.Activo)
                throw new Exception("Credenciales inválidas");

            if (!BCrypt.Net.BCrypt.Verify(dto.Password, cliente.Password))
                throw new Exception("Credenciales inválidas");

            var token = _jwtService.GenerateToken(cliente);

            return new LoginResponseDto
            {
                Token = token,
                Cliente = MapToDto(cliente)
            };
        }

        private ClienteDto MapToDto(Cliente cliente)
        {
            return new ClienteDto
            {
                Id = cliente.Id,
                Nombre = cliente.Nombre,
                Apellidos = cliente.Apellidos,
                Direccion = cliente.Direccion,
                Email = cliente.Email
            };
        }
    }
}
