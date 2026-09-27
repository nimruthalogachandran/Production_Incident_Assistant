using Microsoft.EntityFrameworkCore;
using Production_Incident_Assistant.Core.Entities;

namespace Production_Incident_Assistant.Infrastructure.Data
{
    public class IncidentDbContext : DbContext
    {
        public IncidentDbContext(DbContextOptions<IncidentDbContext> options)
            : base(options)
        {
        }

        public DbSet<Incident> Incidents { get; set; }
    }
}