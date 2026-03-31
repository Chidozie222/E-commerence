using E_commerence.Models;

namespace E_commerence.Interfaces;

public interface IJwtService
{
    public Task<string> GenerateToken(User user, IList<string>  roles);
    
    public string GenerateRefreshToken();
}