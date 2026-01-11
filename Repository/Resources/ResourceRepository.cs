using agendamento_recursos.Data;
using agendamento_recursos.Models;
using Microsoft.EntityFrameworkCore;

namespace agendamento_recursos.Repository.Resource
{
    public class ResourceRepository : IResourceRepository
    {
        private readonly AppDbContext context;

        public ResourceRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Models.Resource>> GetAllAsync()
        {
            return await context.Resources.ToListAsync();
        }

        public async Task<Models.Resource?> GetByIdAsync(int id)
        {
            return await context.Resources.FindAsync(id);
        }
    }
}