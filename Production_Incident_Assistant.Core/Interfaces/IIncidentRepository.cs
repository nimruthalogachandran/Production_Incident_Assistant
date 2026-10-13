using Production_Incident_Assistant.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Production_Incident_Assistant.Core.Interfaces
{
    public interface IIncidentRepository
    {
        Task<Incident?> GetByIdAsync(int id);

        Task<List<Incident>?> GetByStatusAsync(IncidentStatus status);

        Task<List<Incident>?> GetBySeverityAsync(IncidentSeverity severity);

        Task<IEnumerable<Incident>> GetAllAsync();

        Task<Incident> AddAsync(Incident incident);

        Task UpdateAsync(Incident incident);

        Task DeleteAsync(int id);
    }
}
