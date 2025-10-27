using Tidomi.Application.DTOs.Auth;

namespace Tidomi.Application.Interfaces;

public interface IAuthService
{
    Task<AuthResponseDto> RegisterAsync(RegisterDto dto);
    Task<AuthResponseDto> LoginAsync(LoginDto dto);
    Task<string> GenerateJwtToken(Guid userId, string email, string role);
}
