using EcommerceApp.Business.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcommerceApp.Business.Interfaces
{
    public interface IClienteService
    {
        Task<IEnumerable<ClienteDto>> GetAllAsync();
        Task<ClienteDto> GetByIdAsync(int id);
        Task<ClienteDto> CreateAsync(ClienteCreateDto dto);
        Task UpdateAsync(int id, ClienteUpdateDto dto);
        Task DeleteAsync(int id);
        Task<LoginResponseDto> LoginAsync(LoginDto dto);
    }
}
