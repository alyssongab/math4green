using agendamento_recursos.Models;

namespace agendamento_recursos.Repository.Resource
{
    public interface IResourceRepository
    {
        Task<IEnumerable<Models.Resource>> GetAllAsync();
        Task<Models.Resource?> GetByIdAsync(int id);
    }
}