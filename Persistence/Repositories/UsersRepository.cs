using Application.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Persistence.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UsersRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

        }
        public async Task AddAsync(Users user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Users> GetByEmailAsync(string email)
        {
            return await _dbContext.Users
                .Include(u => u.Phones)
                .FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
