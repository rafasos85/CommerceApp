using EcommerceApp.Entities;
using System.Threading.Tasks;

namespace EcommerceApp.Data.Interfaces
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        Task<Cliente> GetByEmailAsync(string email);
        Task<bool> EmailExistsAsync(string email);
    }
}
