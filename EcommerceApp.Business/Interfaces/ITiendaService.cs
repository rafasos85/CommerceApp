using EcommerceApp.Business.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcommerceApp.Business.Interfaces
{
    public interface ITiendaService
    {
        Task<IEnumerable<TiendaDto>> GetAllAsync();
        Task<TiendaDto> GetByIdAsync(int id);
        Task<TiendaDto> CreateAsync(TiendaCreateDto dto);
        Task UpdateAsync(int id, TiendaUpdateDto dto);
        Task DeleteAsync(int id);
    }
}
