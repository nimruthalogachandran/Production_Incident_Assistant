using Production_Incident_Assistant.Core.Entities;
using Production_Incident_Assistant.Core.Interfaces;
using Production_Incident_Assistant.Core.DTOs;


namespace Production_Incident_Assistant.Core.Services
{
    public class IncidentService
    {
        private readonly IIncidentRepository _repository;

        public IncidentService(IIncidentRepository repository)
        {
            _repository = repository;
        }

        public async Task<IncidentResponseDto?> GetIncidentAsync(int id)
        {
            var incident = await _repository.GetByIdAsync(id);

            if (incident == null)
            {
                return null;
            }

            return new IncidentResponseDto
            {
                Id = incident.Id,
                Title = incident.Title,
                Description = incident.Description,
                Severity = incident.Severity.ToString(),
                Status = incident.Status.ToString(),
                CreatedAt = incident.CreatedAt,
                ResolvedAt = incident.ResolvedAt
            };
        }

        public async Task<List<IncidentResponseDto>?> GetIncidentByStatusAsync(IncidentStatus status)
        {
            var incidents = await _repository.GetByStatusAsync(status);

            List<IncidentResponseDto> response = new List<IncidentResponseDto>();

            foreach (var incident in incidents)
            {
                response.Add(new IncidentResponseDto
                {
                    Id = incident.Id,
                    Title = incident.Title,
                    Description = incident.Description,
                    Severity = incident.Severity.ToString(),
                    Status = incident.Status.ToString(),
                    CreatedAt = incident.CreatedAt,
                    ResolvedAt = incident.ResolvedAt
                });
            }
            return response;
        }
        

        public async Task<IEnumerable<Incident>> GetAllIncidentsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<IncidentResponseDto> CreateIncidentAsync(CreateIncidentRequestDto incident)
        {
            if (string.IsNullOrWhiteSpace(incident.Title))
            {
                throw new ArgumentException("Incident title is required.");
            }

            if (string.IsNullOrWhiteSpace(incident.Description))
            {
                throw new ArgumentException("Incident description is required.");
            }

            var newIncident = new Incident
            {
                Title = incident.Title,
                Description = incident.Description,
                Severity = incident.Severity,
                Status = IncidentStatus.Open,
                CreatedAt = DateTime.UtcNow
            };

            var createdIncident = await _repository.AddAsync(newIncident);

            return new IncidentResponseDto
            {
                Id = createdIncident.Id,
                Title = createdIncident.Title,
                Description = createdIncident.Description,
                Severity = createdIncident.Severity,
                Status = createdIncident.Status.ToString(),
                CreatedAt = createdIncident.CreatedAt,
                ResolvedAt = createdIncident.ResolvedAt
            };
        }

        public async Task<bool> UpdateIncidentAsync(int id, UpdateIncidentRequestDto incident)
        {
            var existingIncident = await _repository.GetByIdAsync(id);

            if (existingIncident == null)
            {
                return false;
            }

            existingIncident.Title = incident.Title;
            existingIncident.Description = incident.Description;
            existingIncident.Severity = incident.Severity;

            await _repository.UpdateAsync(existingIncident);

            return true;
        }
        public async Task<bool> UpdateIncidentStatusAsync(int id, UpdateIncidentStatusRequestDto request)
        {
            var incident = await _repository.GetByIdAsync(id);

            if (incident == null)
            {
                return false;
            }

            if (!Enum.TryParse<IncidentStatus>(
                  request.Status,
                  true,
                  out var newStatus))
            {
                throw new ArgumentException("Invalid incident status.");
            }

            incident.Status = newStatus;

            if (newStatus == IncidentStatus.Resolved)
            {
                incident.ResolvedAt = DateTime.UtcNow;
            }

            await _repository.UpdateAsync(incident);

            return true;
        }

        public async Task DeleteIncidentAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<IncidentDashboardDto> GetDashboardAsync()
        {
            var incidents = await _repository.GetAllAsync();

            return new IncidentDashboardDto
            {
                TotalIncidents = incidents.Count(),

                OpenIncidents = incidents.Count(i => i.Status == IncidentStatus.Open),

                InvestigatingIncidents = incidents.Count(i => i.Status == IncidentStatus.Investigating),

                ResolvedIncidents = incidents.Count(i => i.Status == IncidentStatus.Resolved),

                HighSeverityIncidents = incidents.Count(i => i.Severity == "High"),

                CriticalSeverityIncidents = incidents.Count(i => i.Severity == "Critical")
            };
        }
    }
}
