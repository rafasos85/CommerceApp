using EcommerceApp.Entities;

namespace EcommerceApp.Business.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(Cliente cliente);
        int? ValidateToken(string token);
    }
}
