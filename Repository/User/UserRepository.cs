
using agendamento_recursos.Data;
using Microsoft.EntityFrameworkCore;

namespace agendamento_recursos.Repository.User
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext context;
        public UserRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<Models.User> CreateAsync(Models.User user)
        {
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            return user;
        }

        public async Task<IEnumerable<Models.User>> GetAllSync()
        {
            return await context.Users.ToListAsync();
        }

        public async Task<Models.User?> GetByEmailAsync(string email)
        {
            return await context.Users.FirstOrDefaultAsync(
                u => u.Email == email);
        }

        public async Task<Models.User?> GetByIdAsync(int id)
        {
            return await context.Users.FindAsync(id);
        }
    }
}
