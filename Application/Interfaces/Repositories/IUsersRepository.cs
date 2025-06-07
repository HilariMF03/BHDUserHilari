using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IUsersRepository
    {
        Task AddAsync(Users users);
        Task<Users> GetByEmailAsync(string email);
    }
}
