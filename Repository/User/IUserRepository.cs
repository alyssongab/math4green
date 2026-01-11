using agendamento_recursos.Models;

namespace agendamento_recursos.Repository.User
{
    public interface IUserRepository
    {
        Task<IEnumerable<Models.User>> GetAllSync();
        Task<Models.User?> GetByIdAsync(int id);
        Task<Models.User?> GetByEmailAsync(string email);
        Task<Models.User> CreateAsync(Models.User user);
    }

}