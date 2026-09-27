using Microsoft.EntityFrameworkCore;
using Production_Incident_Assistant.Core.Entities;
using Production_Incident_Assistant.Core.Interfaces;
using Production_Incident_Assistant.Infrastructure.Data;

namespace Production_Incident_Assistant.Infrastructure.Repositories
{
    public class IncidentRepository : IIncidentRepository
    {
        private readonly IncidentDbContext _context;

        public IncidentRepository(IncidentDbContext context)
        {
            _context = context;
        }

        public async Task<Incident?> GetByIdAsync(int id)
        {
            return await _context.Incidents.FirstOrDefaultAsync(i => i.Id == id);
        }
        public async Task<List<Incident>?> GetByStatusAsync(IncidentStatus status)
        {
            return await _context.Incidents.Where(i => i.Status == status).ToListAsync();
        }

        public async Task<IEnumerable<Incident>> GetAllAsync()
        {
            return await _context.Incidents.ToListAsync();
        }

        public async Task<Incident> AddAsync(Incident incident)
        {
            _context.Incidents.Add(incident);
            await _context.SaveChangesAsync();

            return incident;
        }

        public async Task UpdateAsync(Incident incident)
        {
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var incident = await _context.Incidents.FindAsync(id);

            if (incident != null)
            {
                _context.Incidents.Remove(incident);
                await _context.SaveChangesAsync();
            }
        }
    }
}