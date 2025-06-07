using Application.Dtos;

namespace Application.Interfaces.Services
{
    public interface IUsersService
    {
        Task<RegisterResponse> RegisterAsync(RegisterRequest request);
    }
}
